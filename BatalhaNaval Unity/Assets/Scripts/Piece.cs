using UnityEngine;

public class Piece : MonoBehaviour
{
    public bool isRed;
    public bool isKing = false;

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

        CheckPromotion();
    }

    private void CheckPromotion()
    {
        if (isKing)
            return;

        if (isRed && row == 0)
        {
            BecomeKing();
        }

        if (!isRed && row == 7)
        {
            BecomeKing();
        }
    }

    private void BecomeKing()
    {
        isKing = true;

        transform.localScale =
            new Vector3(
                1.2f,
                1.2f,
                1f
            );

        Debug.Log("Virou dama!");
    }
}