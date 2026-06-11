using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool redTurn = true;

    public bool gameEnded = false;

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
        if (gameEnded)
            return;

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
            $"Brancas: {redCount} | Pretas: {blackCount}"
        );

        if (redCount <= 1)
        {
            gameEnded = true;

            UIManager.Instance.ShowWinner(
                "Pretas"
            );
        }

        if (blackCount <= 1)
        {
            gameEnded = true;

            UIManager.Instance.ShowWinner(
                "Brancas"
            );
        }
    }
}