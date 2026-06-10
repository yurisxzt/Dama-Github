using UnityEngine;

public class Piece : MonoBehaviour
{
    public bool isRed;
    public int row;
    public int column;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void OnMouseDown()
    {
        SelectionManager.Instance.SelectPiece(this);
    }

    public void Select()
    {
        spriteRenderer.color = Color.yellow;
    }

    public void Deselect()
    {
        spriteRenderer.color = originalColor;
    }

    public void MoveTo(int newRow, int newColumn)
    {
        row = newRow;
        column = newColumn;

        transform.position =
            new Vector3(
                newColumn,
                -newRow,
                -1
            );
    }
}