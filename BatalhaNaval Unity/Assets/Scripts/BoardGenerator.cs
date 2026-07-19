using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    public GameObject tilePrefab;

    public Color lightColor =
        new Color(0.82f, 0.70f, 0.55f);

    public Color darkColor =
        new Color(0.36f, 0.22f, 0.10f);

    private const int boardSize = 8;
    private bool hasGeneratedBoard = false;

    private void Start()
    {
        if (hasGeneratedBoard)
            return;

        GenerateBoard();
        hasGeneratedBoard = true;
    }

    private void GenerateBoard()
    {
        if (tilePrefab == null)
        {
            CreateFallbackTilePrefab();
        }

        for (int row = 0; row < boardSize; row++)
        {
            for (int col = 0; col < boardSize; col++)
            {
                GameObject tile = Instantiate(
                    tilePrefab,
                    new Vector3(col, -row, 0),
                    Quaternion.identity,
                    transform
                );

                Tile tileScript =
                    tile.GetComponent<Tile>();

                if (tileScript == null)
                {
                    tileScript = tile.AddComponent<Tile>();
                }

                tileScript.row = row;
                tileScript.column = col;

                SpriteRenderer renderer =
                    tile.GetComponent<SpriteRenderer>();

                if (renderer == null)
                {
                    renderer = tile.AddComponent<SpriteRenderer>();
                }

                bool isDark =
                    (row + col) % 2 == 1;

                renderer.color =
                    isDark ? darkColor : lightColor;
            }
        }
    }

    private void CreateFallbackTilePrefab()
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

        GameObject fallbackTile = new GameObject("FallbackTile");
        fallbackTile.transform.localScale = new Vector3(1f, 1f, 1f);

        SpriteRenderer renderer =
            fallbackTile.AddComponent<SpriteRenderer>();

        renderer.sprite = sprite;

        fallbackTile.AddComponent<Tile>();
        fallbackTile.AddComponent<BoxCollider2D>();

        tilePrefab = fallbackTile;
    }
}