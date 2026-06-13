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

        HighlightManager.Instance.ClearHighlights();

        if (selectedPiece != null)
        {
            selectedPiece.Deselect();
        }

        selectedPiece = piece;

        selectedPiece.Select();

        ShowPossibleMoves(piece);
    }

    public Piece GetSelectedPiece()
    {
        return selectedPiece;
    }

    public void ClearSelection()
    {
        selectedPiece = null;

        HighlightManager.Instance.ClearHighlights();
    }

    private void ShowPossibleMoves(Piece piece)
    {
        Tile[] tiles =
            FindObjectsByType<Tile>(
                FindObjectsSortMode.None
            );

        foreach (Tile tile in tiles)
        {
            int rowDiff =
                tile.row - piece.row;

            int colDiff =
                tile.column - piece.column;

            Piece pieceOnTarget =
                BoardState.Instance.GetPieceAt(
                    tile.row,
                    tile.column
                );

            if (pieceOnTarget != null)
                continue;

            bool normalMove = false;
            bool captureMove = false;

            if (!piece.isKing)
            {
                if (piece.isRed)
                {
                    normalMove =
                        rowDiff == -1 &&
                        Mathf.Abs(colDiff) == 1;
                }
                else
                {
                    normalMove =
                        rowDiff == 1 &&
                        Mathf.Abs(colDiff) == 1;
                }
            }

            if (piece.isKing)
            {
                normalMove =
                    Mathf.Abs(rowDiff) == 1 &&
                    Mathf.Abs(colDiff) == 1;
            }

            if (
                Mathf.Abs(rowDiff) == 2 &&
                Mathf.Abs(colDiff) == 2
            )
            {
                int middleRow =
                    piece.row + rowDiff / 2;

                int middleColumn =
                    piece.column + colDiff / 2;

                Piece enemy =
                    BoardState.Instance.GetPieceAt(
                        middleRow,
                        middleColumn
                    );

                if (
                    enemy != null &&
                    enemy.isRed != piece.isRed
                )
                {
                    captureMove = true;
                }
            }

            if (captureMove)
            {
                HighlightManager.Instance
                    .HighlightCapture(tile);
            }
            else if (normalMove)
            {
                HighlightManager.Instance
                    .HighlightMove(tile);
            }
        }
    }
}