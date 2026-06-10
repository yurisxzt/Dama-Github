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

        bool validMove = false;

        Piece capturedPiece = null;

        int rowDifference =
            tile.row - piece.row;

        int columnDifference =
            tile.column - piece.column;

        // MOVIMENTO NORMAL

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

        // CAPTURA

        if (piece.isRed)
        {
            if (
                rowDifference == -2 &&
                Mathf.Abs(columnDifference) == 2
            )
            {
                int middleRow =
                    piece.row - 1;

                int middleColumn =
                    piece.column +
                    columnDifference / 2;

                Piece enemy =
                    BoardState.Instance
                    .GetPieceAt(
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
        else
        {
            if (
                rowDifference == 2 &&
                Mathf.Abs(columnDifference) == 2
            )
            {
                int middleRow =
                    piece.row + 1;

                int middleColumn =
                    piece.column +
                    columnDifference / 2;

                Piece enemy =
                    BoardState.Instance
                    .GetPieceAt(
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
        }

        piece.MoveTo(
            tile.row,
            tile.column
        );

        piece.Deselect();

        SelectionManager.Instance
            .ClearSelection();

        GameManager.Instance
            .ChangeTurn();
    }
}