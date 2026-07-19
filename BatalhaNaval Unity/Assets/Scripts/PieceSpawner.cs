using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
    public GameObject piecePrefab;

    private bool hasSpawnedPieces = false;

    private void Start()
    {
        if (hasSpawnedPieces)
            return;

        SpawnPieces();
        hasSpawnedPieces = true;
    }

    private void SpawnPieces()
    {
        if (piecePrefab == null)
        {
            CreateFallbackPiecePrefab();
        }

        SpawnBlackPieces();
        SpawnRedPieces();
    }

    private void SpawnBlackPieces()
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

    private void SpawnRedPieces()
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

    private void CreatePiece(int row, int col, bool isRed)
    {
        GameObject piece =
            Instantiate(
                piecePrefab,
                new Vector3(col, -row, -1),
                Quaternion.identity,
                transform
            );

        Piece pieceScript = piece.GetComponent<Piece>();

        if (pieceScript == null)
        {
            pieceScript = piece.AddComponent<Piece>();
        }

        pieceScript.row = row;
        pieceScript.column = col;
        pieceScript.isRed = isRed;

        SpriteRenderer renderer =
            piece.GetComponent<SpriteRenderer>();

        if (renderer == null)
        {
            renderer = piece.AddComponent<SpriteRenderer>();
        }

        renderer.color =
            isRed ? Color.white : Color.black;
    }

    private void CreateFallbackPiecePrefab()
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, Color.white);
        texture.Apply();

        Sprite sprite = Sprite.Create(
            texture,
            new Rect(0, 0, 1, 1),
            new Vector2(0.5f, 0.5f),
            1f
        );

        GameObject fallbackPiece = new GameObject("FallbackPiece");
        SpriteRenderer renderer =
            fallbackPiece.AddComponent<SpriteRenderer>();

        renderer.sprite = sprite;
        renderer.color = Color.white;

        fallbackPiece.AddComponent<Piece>();
        piecePrefab = fallbackPiece;
    }
}