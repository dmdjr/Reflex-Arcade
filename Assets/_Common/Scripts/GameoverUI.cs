using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;

    public void ShowResult(int currentScore, int bestScore)
    {
        if (currentScoreText != null)
        {
            currentScoreText.text = "Score " + currentScore.ToString();
        }
        if (bestScoreText != null)
        {
            bestScoreText.text = "Best " + bestScore.ToString();
        }
    }

    public void OnClickRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Lobby");
    }
}