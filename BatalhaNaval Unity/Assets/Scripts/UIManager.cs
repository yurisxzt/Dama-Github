using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TMP_Text turnText;
    public TMP_Text winnerText;

    public Button restartButton;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        restartButton.gameObject.SetActive(false);

        restartButton.onClick.AddListener(
            RestartGame
        );
    }

    public void UpdateTurn(bool redTurn)
    {
        turnText.text =
            redTurn
            ? "Turno das Brancas"
            : "Turno das Pretas";
    }

    public void ShowWinner(string winner)
    {
        winnerText.text =
            winner + " venceram!";

        restartButton.gameObject.SetActive(true);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}