using UnityEngine;

public class Tile : Ticable
{
    [SerializeField] private TileType type = TileType.Empty;

    private SpriteRenderer sr;

    public float Height { get { return sr.sprite.rect.height; } }
    public float Width { get { return sr.sprite.rect.width; } }

    public Ticable Entity { get; set; }

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
        // *****
        // Insert logic to determine sprite
        s = sr.sprite;
        // *****
        sr.sprite = s;
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

    public override bool Tic()
    {
        base.Tic();
        Entity?.Tic();
        return true;
    }
}
