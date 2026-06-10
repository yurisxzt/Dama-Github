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
}