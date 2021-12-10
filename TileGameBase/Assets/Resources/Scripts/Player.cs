using UnityEngine;

public class Player : MonoBehaviour
{
    private Board game;
    private Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<Board>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Tile t = IsMouseInteraction() ? SelectTile() : null;
        if (t == null) return;
    }

    private Tile SelectTile()
    {
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        return hit.collider != null ? 
                hit.collider.gameObject.GetComponent<Tile>() : null;
    }

    private bool IsMouseInteraction()
    {
        return Input.GetMouseButtonUp(0) || 
               Input.GetMouseButton(0) ||
               Input.GetMouseButtonDown(1);
    }
}
