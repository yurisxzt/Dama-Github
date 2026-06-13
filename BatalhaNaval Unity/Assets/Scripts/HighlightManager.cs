using System.Collections.Generic;
using UnityEngine;

public class HighlightManager : MonoBehaviour
{
    public static HighlightManager Instance;

    private List<Tile> highlightedTiles =
        new List<Tile>();

    private void Awake()
    {
        Instance = this;
    }

    public void ClearHighlights()
    {
        foreach (Tile tile in highlightedTiles)
        {
            if (tile != null)
            {
                tile.HideHighlights();
            }
        }

        highlightedTiles.Clear();
    }

    public void HighlightMove(Tile tile)
    {
        if (tile == null)
            return;

        tile.ShowMoveHighlight();

        if (!highlightedTiles.Contains(tile))
        {
            highlightedTiles.Add(tile);
        }
    }

    public void HighlightCapture(Tile tile)
    {
        if (tile == null)
            return;

        tile.ShowCaptureHighlight();

        if (!highlightedTiles.Contains(tile))
        {
            highlightedTiles.Add(tile);
        }
    }
}