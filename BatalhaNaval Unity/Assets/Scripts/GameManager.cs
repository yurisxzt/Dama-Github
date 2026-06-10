using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool redTurn = true;

    private void Awake()
    {
        Instance = this;
    }

    public void ChangeTurn()
    {
        redTurn = !redTurn;

        Debug.Log(
            redTurn ?
            "Turno do Vermelho" :
            "Turno do Preto"
        );
    }
}