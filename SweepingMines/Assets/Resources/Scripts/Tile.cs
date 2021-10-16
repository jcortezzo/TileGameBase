using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool isCovered = true;
    private bool isFlagged = false;

    [SerializeField] private TileType type = TileType.Empty;

    private SpriteRenderer sr;

    public float height { get { return sr.sprite.rect.height; } }
    public float width { get { return sr.sprite.rect.width; } }

    /// <summary>
    /// You can't chain constructors in C#???
    /// </summary>
    public Tile()
    {
        this.type = TileType.Empty;
    }

    public Tile(TileType type)
    {
        this.type = type;
    }

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        Resprite();
    }

    private void Resprite()
    {
        Sprite s;
        if (isCovered)
        {
            if (isFlagged)
            {
                s = Constants.FLAG_SPRITE;
            }
            else
            {
                s = Constants.COVERED_SPRITE;
            }
        }
        else
        {
            s = Constants.SPRITE_MAP[type];
        }
        sr.sprite = s;
    }

    /// <summary>
    /// Actively uncover with mouse, i.e. "left clicking"
    /// </summary>
    public void Uncover()
    {
        isCovered = false;
        if (type == TileType.Mine)
        {
            type = TileType.MineLose;
        }
        Resprite();
    }

    /// <summary>
    /// This shouldn't ever actually exist but why not
    /// </summary>
    public void Cover()
    {
        isCovered = true;
        Resprite();
    }

    /// <summary>
    /// Passive method
    /// </summary>
    public void RevealSelf()
    {
        isCovered = false;
        Resprite();
    }

    public void Flag()
    {
        isFlagged = true;
        Resprite();
    }

    public void Deflag()
    {
        isFlagged = false;
        Resprite();
    }

    public bool IsMine()
    {
        return type == TileType.Mine || type == TileType.MineLose;
    }

    public bool IsFlag()
    {
        return isFlagged;
    }

    public bool IsCovered()
    {
        return isCovered;
    }

    public bool IsNumber()
    {
        return Constants.NUMERIC_TILE_TYPES.Contains(type);
    }

    public TileType GetTileType() { return type; }
    public void SetTileType(TileType newType)
    {
        type = newType;
        Resprite();
    }

    public void Hide()
    {
        sr.enabled = false;
    }

    public void Show()
    {
        sr.enabled = true;
    }
}
