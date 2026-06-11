using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool redTurn = true;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UIManager.Instance.UpdateTurn(redTurn);
    }

    public void ChangeTurn()
    {
        redTurn = !redTurn;

        UIManager.Instance.UpdateTurn(redTurn);
    }

   public void CheckWinner()
{
    Piece[] pieces =
        FindObjectsByType<Piece>(
            FindObjectsSortMode.None
        );

    int redCount = 0;
    int blackCount = 0;

    foreach (Piece piece in pieces)
    {
        if (piece == null)
            continue;

        if (piece.isRed)
            redCount++;
        else
            blackCount++;
    }

    Debug.Log(
        $"Vermelhas: {redCount} | Pretas: {blackCount}"
    );

    if (redCount <= 1)
    {
        UIManager.Instance.ShowWinner("Preto");
    }

    if (blackCount <= 1)
    {
        UIManager.Instance.ShowWinner("Vermelho");
    }
}
}