using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Minefield : MonoBehaviour
{
    [SerializeField] private int HEIGHT = 16;
    [SerializeField] private int WIDTH = 30;
    [SerializeField] private int NUM_MINES = 99;

    private Bidict<Tile, Vector2Int> board;
    private List<Tile> allTiles;
    private List<Tile> mines;

    [SerializeField] private GameObject TILE_PREFAB;

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate all mines and give them positions on the board
        allTiles = new List<Tile>();
        board = new Bidict<Tile, Vector2Int>();
        int n = NUM_MINES;
        for (int y = 0; y < WIDTH; y++)
        {
            for (int x = 0; x < HEIGHT; x++)
            {
                Vector2Int position = new Vector2Int(x, y);
                Tile t = Instantiate(TILE_PREFAB, 
                                     transform.position + (Vector3) ((Vector2) position),// * Constants.TILE_SIZE, // don't have to multiply because unit = 16!
                                     Quaternion.identity, 
                                     transform).GetComponent<Tile>();
                allTiles.Add(t);
                board[t] = position;
            }
        }

        // Place mines
        System.Random rnd = new System.Random();
        mines = allTiles.OrderBy(x => rnd.Next()).Take(NUM_MINES).ToList();
        mines.ForEach(t => t.SetTileType(TileType.Mine));

        // Adjust after placing mines
        allTiles.ForEach(t => ReevaluateTile(t));
    }

    /// <summary>
    /// Recursively uncovers tiles if they're blank, stopping at numbers and mines.
    /// </summary>
    /// <param name="t">Tile to uncover.</param>
    public void Uncover(Tile t)
    {
        ISet<Tile> closed = new HashSet<Tile>();
        Uncover(t, closed);
    }

    private void Uncover(Tile t, ISet<Tile> closed)
    {
        if (closed.Contains(t) || t.IsFlag()) return;

        closed.Add(t);
        t.Uncover();
        if (t.GetTileType() == TileType.Empty)
        {
            GetNeighbors(t).Where(neighbor => !neighbor.IsMine() && 
                                              !neighbor.IsFlag())
                           .ToList()
                           .ForEach(neighbor =>
            {
                Uncover(neighbor, closed);
            });
        }
    }

    public void Chord(Tile t)
    {
        if (t.IsCovered() || t.IsFlag() || !t.IsNumber()) return;
        List<Tile> neighbors = GetNeighbors(t);
        List<Tile> flags = neighbors.Where(neighbor => neighbor.IsFlag()).ToList();
        if (flags.Count == Constants.TILE_TYPE_TO_NUM[t.GetTileType()])
        {
            neighbors.Except(flags).ToList().ForEach(neighbor => { Uncover(neighbor); });
        }
    }

    public void ReevaluateTile(Tile t)
    {
        if (t.IsMine()) return;

        List<Tile> neighbors = GetNeighbors(t);
        int n = neighbors.Where(neighbor => neighbor.IsMine()).Count();
        t.SetTileType(Constants.NUM_TO_TILE_TYPE[n]);
    }

    private List<Tile> GetNeighbors(Tile t)
    {
        List<Tile> neighbors = new List<Tile>();
        Constants.DIRECTION_VECTORS2INTS.ForEach(v => {
            Vector2Int newCoords = board[t] + v;
            if (board.ContainsKey(newCoords))
            {
                neighbors.Add(board[newCoords]);
            }
        });
        return neighbors;
    }
}
