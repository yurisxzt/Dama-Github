using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    public GameObject tilePrefab;

    public Color lightColor =
    new Color(0.82f, 0.70f, 0.55f);

public Color darkColor =
    new Color(0.36f, 0.22f, 0.10f);

    private const int boardSize = 8;

    private void Start()
    {
        GenerateBoard();
    }

    void GenerateBoard()
    {
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

                tileScript.row = row;
                tileScript.column = col;

                SpriteRenderer renderer =
                    tile.GetComponent<SpriteRenderer>();

                bool isDark =
                    (row + col) % 2 == 1;

                renderer.color =
                    isDark ? darkColor : lightColor;
            }
        }
    }
}