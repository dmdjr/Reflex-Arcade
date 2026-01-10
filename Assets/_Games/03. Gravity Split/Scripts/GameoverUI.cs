using TMPro;
using UnityEngine;

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
}
