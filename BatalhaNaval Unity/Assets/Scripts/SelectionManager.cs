using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance;

    private Piece selectedPiece;

    private void Awake()
    {
        Instance = this;
    }

    public void SelectPiece(Piece piece)
    {
        if (GameManager.Instance.gameEnded)
            return;

        if (piece.isRed != GameManager.Instance.redTurn)
            return;

        if (selectedPiece != null)
        {
            selectedPiece.Deselect();
        }

        selectedPiece = piece;

        selectedPiece.Select();
    }

    public Piece GetSelectedPiece()
    {
        return selectedPiece;
    }

    public void ClearSelection()
    {
        selectedPiece = null;
    }
}