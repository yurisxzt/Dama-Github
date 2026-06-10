using UnityEngine;

public class MoveManager : MonoBehaviour
{
    public static MoveManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void TryMoveTo(Tile tile)
    {
        Piece piece =
            SelectionManager.Instance.GetSelectedPiece();

        if (piece == null)
            return;

        Piece pieceOnTarget =
            BoardState.Instance.GetPieceAt(
                tile.row,
                tile.column
            );

        if (pieceOnTarget != null)
            return;

        bool validMove = false;
        Piece capturedPiece = null;

        int rowDifference =
            tile.row - piece.row;

        int columnDifference =
            tile.column - piece.column;

        // =====================
        // MOVIMENTO NORMAL
        // =====================

        if (!piece.isKing)
        {
            if (piece.isRed)
            {
                if (
                    rowDifference == -1 &&
                    Mathf.Abs(columnDifference) == 1
                )
                {
                    validMove = true;
                }
            }
            else
            {
                if (
                    rowDifference == 1 &&
                    Mathf.Abs(columnDifference) == 1
                )
                {
                    validMove = true;
                }
            }
        }

        // =====================
        // MOVIMENTO DA DAMA
        // =====================

        if (piece.isKing)
        {
            if (
                Mathf.Abs(rowDifference) == 1 &&
                Mathf.Abs(columnDifference) == 1
            )
            {
                validMove = true;
            }
        }

        // =====================
        // CAPTURA NORMAL
        // =====================

        if (!piece.isKing)
        {
            if (
                Mathf.Abs(rowDifference) == 2 &&
                Mathf.Abs(columnDifference) == 2
            )
            {
                int middleRow =
                    piece.row + rowDifference / 2;

                int middleColumn =
                    piece.column + columnDifference / 2;

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
                    validMove = true;
                    capturedPiece = enemy;
                }
            }
        }

        // =====================
        // CAPTURA DA DAMA
        // =====================

        if (piece.isKing)
        {
            if (
                Mathf.Abs(rowDifference) == 2 &&
                Mathf.Abs(columnDifference) == 2
            )
            {
                int middleRow =
                    piece.row + rowDifference / 2;

                int middleColumn =
                    piece.column + columnDifference / 2;

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
                    validMove = true;
                    capturedPiece = enemy;
                }
            }
        }

        if (!validMove)
            return;

        if (capturedPiece != null)
        {
            Destroy(capturedPiece.gameObject);

            GameManager.Instance.CheckWinner();
        }

        piece.MoveTo(
            tile.row,
            tile.column
        );

        piece.Deselect();

        SelectionManager.Instance.ClearSelection();

        GameManager.Instance.ChangeTurn();
    }
}