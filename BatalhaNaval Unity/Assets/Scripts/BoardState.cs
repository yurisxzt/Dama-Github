using UnityEngine;

public class BoardState : MonoBehaviour
{
    public static BoardState Instance;

    private void Awake()
    {
        Instance = this;
    }

    public Piece GetPieceAt(int row, int column)
    {
        Piece[] pieces = FindObjectsByType<Piece>(
            FindObjectsSortMode.None
        );

        foreach (Piece piece in pieces)
        {
            if (
                piece.row == row &&
                piece.column == column
            )
            {
                return piece;
            }
        }

        return null;
    }
}