using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GravityGameManager : MonoBehaviour
{
    public static GravityGameManager Instance;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameoverUI scoreUI;
    [SerializeField] private TextMeshProUGUI scoreText;

    private int currentScore = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Time.timeScale = 1f;
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        if (scoreText != null)
        {
            scoreText.text = currentScore.ToString();
        }
        Debug.Log("점수 획득! 현재 점수: " + currentScore);

    }
    public void GameOver()
    {
        Time.timeScale = 0f;
        int bestScore = 0;

        if (DataManager.Instance != null)
        {
            DataManager.Instance.CheckAndSaveBestScore(GameType.GravitySplit, currentScore);
        }


        if (DataManager.Instance != null)
        {
            bestScore = DataManager.Instance.GetBestScore(GameType.GravitySplit);
        }
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
            scoreUI.ShowResult(currentScore, bestScore);
        }
    }
}
