using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TMP_Text turnText;
    public TMP_Text winnerText;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateTurn(bool redTurn)
    {
        turnText.text =
            redTurn
            ? "Turno do Vermelho"
            : "Turno do Preto";
    }

    public void ShowWinner(string winner)
    {
        winnerText.text =
            winner + " venceu!";
    }
}