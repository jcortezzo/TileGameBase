using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private int HEIGHT = 16;
    [SerializeField] private int WIDTH = 30;

    private Bidict<Tile, Vector2Int> board;
    private List<Tile> allTiles;

    [SerializeField] private GameObject TILE_PREFAB;

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate all mines and give them positions on the board
        allTiles = new List<Tile>();
        board = new Bidict<Tile, Vector2Int>();
        
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

        // Adjust after placing mines
        allTiles.ForEach(t => ReevaluateTile(t));
    }

    public void ReevaluateTile(Tile t)
    {
        // *****
        // Insert logic for what happens when you re-evaluate a Tile
        // *****
        t.SetTileType(TileType.Empty);
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
