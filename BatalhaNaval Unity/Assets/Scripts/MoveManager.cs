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

        if (piece.isRed)
        {
            if (tile.row == piece.row - 1)
            {
                if (
                    tile.column == piece.column - 1 ||
                    tile.column == piece.column + 1
                )
                {
                    validMove = true;
                }
            }
        }
        else
        {
            if (tile.row == piece.row + 1)
            {
                if (
                    tile.column == piece.column - 1 ||
                    tile.column == piece.column + 1
                )
                {
                    validMove = true;
                }
            }
        }

        if (!validMove)
            return;

        piece.row = tile.row;
        piece.column = tile.column;

        piece.transform.position =
            new Vector3(
                tile.column,
                -tile.row,
                -1
            );
    }
}
