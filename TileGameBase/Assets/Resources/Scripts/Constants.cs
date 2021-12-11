using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
    private const string SPRITESHEET_NAME = "Sprites/<GAME>-Sheet";

    private static Sprite[] SPRITES = Resources.LoadAll<Sprite>(SPRITESHEET_NAME);

    public static Sprite TILE = SPRITES[0];

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

    public static float TILE_SIZE { get { return TILE.rect.height; } }

    public const float TIC_TIME = 1f;
}
