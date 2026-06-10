using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
    public GameObject piecePrefab;

    private void Start()
    {
        SpawnPieces();
    }

    void SpawnPieces()
    {
        SpawnBlackPieces();
        SpawnRedPieces();
    }

    void SpawnBlackPieces()
    {
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                if ((row + col) % 2 == 1)
                {
                    CreatePiece(row, col, false);
                }
            }
        }
    }

    void SpawnRedPieces()
    {
        for (int row = 5; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                if ((row + col) % 2 == 1)
                {
                    CreatePiece(row, col, true);
                }
            }
        }
    }

    void CreatePiece(int row, int col, bool isRed)
    {
        GameObject piece =
            Instantiate(
                piecePrefab,
                new Vector3(col, -row, -1),
                Quaternion.identity,
                transform
            );

        Piece pieceScript = piece.GetComponent<Piece>();

        pieceScript.row = row;
        pieceScript.column = col;
        pieceScript.isRed = isRed;

        SpriteRenderer renderer =
            piece.GetComponent<SpriteRenderer>();

        renderer.color =
            isRed ? Color.red : Color.black;
    }
}