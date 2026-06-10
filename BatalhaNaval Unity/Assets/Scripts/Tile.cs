using UnityEngine;

public class Tile : MonoBehaviour
{
    public int row;
    public int column;

    private void OnMouseDown()
    {
        MoveManager.Instance.TryMoveTo(this);
    }
}