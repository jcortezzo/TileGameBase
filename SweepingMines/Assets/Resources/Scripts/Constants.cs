using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
    private const string SPRITESHEET_NAME = "Sprites/Minesweeper-Sheet";

    private static Sprite[] SPRITES = Resources.LoadAll<Sprite>(SPRITESHEET_NAME);

    public static Sprite COVERED_SPRITE = SPRITES[0];

    public static Sprite FLAG_SPRITE = SPRITES[12];

    public static Dictionary<TileType, Sprite> SPRITE_MAP =
        new Dictionary<TileType, Sprite>()
        {
            { TileType.Empty, SPRITES[1] },
            { TileType.One, SPRITES[2] },
            { TileType.Two, SPRITES[3] },
            { TileType.Three, SPRITES[4] },
            { TileType.Four, SPRITES[5] },
            { TileType.Five, SPRITES[6] },
            { TileType.Six, SPRITES[7] },
            { TileType.Seven, SPRITES[8] },
            { TileType.Eight, SPRITES[9] },
            { TileType.Mine, SPRITES[10] },
            { TileType.MineLose, SPRITES[11] },
        };

    public static Dictionary<int, TileType> NUM_TO_TILE_TYPE = new Dictionary<int, TileType>
    {
        { 0, TileType.Empty },
        { 1, TileType.One },
        { 2, TileType.Two },
        { 3, TileType.Three },
        { 4, TileType.Four },
        { 5, TileType.Five },
        { 6, TileType.Six },
        { 7, TileType.Seven },
        { 8, TileType.Eight },
    };

    public static Dictionary<TileType, int> TILE_TYPE_TO_NUM =
            NUM_TO_TILE_TYPE.GroupBy(d => d.Value, d => d.Key)
                            .ToDictionary(d => d.Key, d => d.First());

    public static HashSet<TileType> NUMERIC_TILE_TYPES = new HashSet<TileType>()
    {
        TileType.One,
        TileType.Two,
        TileType.Three,
        TileType.Four,
        TileType.Five,
        TileType.Six,
        TileType.Seven,
        TileType.Eight,
    };

    public static List<Vector2Int> DIRECTION_VECTORS2INTS = new List<Vector2Int>
        {
            Vector2Int.up,
            Vector2Int.up + Vector2Int.right,
            Vector2Int.right,
            Vector2Int.right + Vector2Int.down,
            Vector2Int.down,
            Vector2Int.down + Vector2Int.left,
            Vector2Int.left,
            Vector2Int.left + Vector2Int.up,
        };

    public static float TILE_SIZE { get { return COVERED_SPRITE.rect.height; } }
}
