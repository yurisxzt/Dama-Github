using UnityEngine;

public class Piece : MonoBehaviour
{
    public bool isRed;
    public bool isKing = false;

    public int row;
    public int column;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private GameObject selectionRing;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        selectionRing = transform.Find("SelectionRing")?.gameObject;

        if (selectionRing != null)
            selectionRing.SetActive(false);
    }

    private void OnMouseDown()
    {
        SelectionManager.Instance.SelectPiece(this);
    }

    public void Select()
    {
        if (selectionRing != null)
            selectionRing.SetActive(true);
    }

    public void Deselect()
    {
        if (selectionRing != null)
            selectionRing.SetActive(false);
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

        Transform crown =
            transform.Find("Crown");

        if (crown != null)
        {
            crown.gameObject.SetActive(true);
        }

        Debug.Log("Virou dama!");
    }
}