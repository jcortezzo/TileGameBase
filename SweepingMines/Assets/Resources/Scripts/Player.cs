using UnityEngine;

public class Player : MonoBehaviour
{
    private Minefield minefield;
    private Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        minefield = FindObjectOfType<Minefield>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Tile t = IsMouseInteraction() ? SelectTile() : null;
        if (t == null) return;

        if (Input.GetMouseButton(1))
        {
            if (Input.GetMouseButtonUp(0))
            {
                minefield.Chord(t);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            minefield.Uncover(t);
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (!t.IsCovered()) return;
            if (!t.IsFlag()) t.Flag(); else t.Deflag();
        }
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
