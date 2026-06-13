using UnityEngine;

public class Tile : MonoBehaviour
{
    public int row;
    public int column;

    private GameObject moveHighlight;
    private GameObject captureHighlight;

    private void Start()
    {
        Transform move =
            transform.Find("HighlightMove");

        Transform capture =
            transform.Find("HighlightCapture");

        if (move != null)
        {
            moveHighlight =
                move.gameObject;

            moveHighlight.SetActive(false);
        }

        if (capture != null)
        {
            captureHighlight =
                capture.gameObject;

            captureHighlight.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        MoveManager.Instance.TryMoveTo(this);
    }

    public void ShowMoveHighlight()
    {
        if (moveHighlight != null)
        {
            moveHighlight.SetActive(true);
        }
    }

    public void ShowCaptureHighlight()
    {
        if (captureHighlight != null)
        {
            captureHighlight.SetActive(true);
        }
    }

    public void HideHighlights()
    {
        if (moveHighlight != null)
        {
            moveHighlight.SetActive(false);
        }

        if (captureHighlight != null)
        {
            captureHighlight.SetActive(false);
        }
    }
}