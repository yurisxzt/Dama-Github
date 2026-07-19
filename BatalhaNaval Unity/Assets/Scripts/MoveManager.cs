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

        if (piece == null || GameManager.Instance.gameEnded)
            return;

        if (!IsValidMove(piece, tile.row, tile.column, out Piece capturedPiece))
            return;

        int fromRow = piece.row;
        int fromColumn = piece.column;

        if (capturedPiece != null)
        {
            Destroy(capturedPiece.gameObject);
        }

        piece.MoveTo(tile.row, tile.column);
        piece.Deselect();

        SelectionManager.Instance.ClearSelection();

        GameManager.Instance.ChangeTurn();
        GameManager.Instance.CheckWinner();

        NetworkManager.Instance.SendMove(
            fromRow,
            fromColumn,
            tile.row,
            tile.column,
            piece.isRed
        );
    }

    public void ApplyRemoteMove(int fromRow, int fromColumn, int toRow, int toColumn)
    {
        Piece piece = BoardState.Instance.GetPieceAt(fromRow, fromColumn);

        if (piece == null || GameManager.Instance.gameEnded)
            return;

        if (!IsValidMove(piece, toRow, toColumn, out Piece capturedPiece))
            return;

        if (capturedPiece != null)
        {
            Destroy(capturedPiece.gameObject);
        }

        piece.MoveTo(toRow, toColumn);
        piece.Deselect();

        SelectionManager.Instance.ClearSelection();

        GameManager.Instance.ChangeTurn();
        GameManager.Instance.CheckWinner();
    }

    private bool IsValidMove(Piece piece, int targetRow, int targetColumn, out Piece capturedPiece)
    {
        capturedPiece = null;

        Piece pieceOnTarget =
            BoardState.Instance.GetPieceAt(
                targetRow,
                targetColumn
            );

        if (pieceOnTarget != null)
            return false;

        int rowDifference = targetRow - piece.row;
        int columnDifference = targetColumn - piece.column;

        bool validMove = false;

        if (!piece.isKing)
        {
            if (piece.isRed)
            {
                validMove =
                    rowDifference == -1 &&
                    Mathf.Abs(columnDifference) == 1;
            }
            else
            {
                validMove =
                    rowDifference == 1 &&
                    Mathf.Abs(columnDifference) == 1;
            }
        }

        if (piece.isKing)
        {
            validMove =
                Mathf.Abs(rowDifference) == 1 &&
                Mathf.Abs(columnDifference) == 1;
        }

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

        return validMove;
    }
}