using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GravityGameManager : MonoBehaviour
{
    public static GravityGameManager Instance;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI scoreText;

    private int currentScore = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
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
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
        // TODO: 점수 저장 등 데이터 처리 (DataManager 호출)
        // DataManager.Instance.CheckAndSaveBestScore(...);
    }
}
