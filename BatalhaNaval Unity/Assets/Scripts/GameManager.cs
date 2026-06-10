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
        Debug.Log("Turno do Vermelho");
    }

    public void ChangeTurn()
    {
        redTurn = !redTurn;

        Debug.Log(
            redTurn
            ? "Turno do Vermelho"
            : "Turno do Preto"
        );
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
            if (piece.isRed)
                redCount++;
            else
                blackCount++;
        }

        if (redCount == 0)
        {
            Debug.Log("Preto venceu!");
        }

        if (blackCount == 0)
        {
            Debug.Log("Vermelho venceu!");
        }
    }
}