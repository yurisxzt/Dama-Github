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

        int whiteCount = 0;
        int blackCount = 0;

        foreach (Piece piece in pieces)
        {
            if (piece == null)
                continue;

            if (piece.isRed)
                whiteCount++;
            else
                blackCount++;
        }

        Debug.Log(
            $"Brancas: {whiteCount} | Pretas: {blackCount}"
        );

        if (whiteCount == 0)
        {
            gameEnded = true;

            UIManager.Instance.ShowWinner(
                "Pretas"
            );
        }

        if (blackCount == 0)
        {
            gameEnded = true;

            UIManager.Instance.ShowWinner(
                "Brancas"
            );
        }
    }
}