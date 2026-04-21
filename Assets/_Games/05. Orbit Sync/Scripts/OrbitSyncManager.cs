using TMPro;
using UnityEngine;

public class OrbitSyncManager : BaseGameManager
{
    public static OrbitSyncManager Instance;

        [SerializeField] private GameObject gameOverUI;
        [SerializeField] private GameoverUI scoreUI;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TutorialUI tutorialUI;

        private int currentScore = 0;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            if (DataManager.Instance != null && DataManager.Instance.IsFirstSeen(GameType.OrbitSync))
            {
                tutorialUI.Open(GameType.OrbitSync, StartGame);
            }
            else
            {
                if (tutorialUI != null)
                {
                    tutorialUI.gameObject.SetActive(false);
                }
                StartGame();
            }
        }

        public void AddScore(int amount)
    {
            if (!IsGameRunning) return;
            currentScore += amount;
            if (scoreText != null)
            {
                scoreText.text = currentScore.ToString();
            }
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayScoreSound();
            }
        }

        public void GameOver()
        {
            if (!IsGameRunning) return; // 이미 게임오버 상태면 중복 실행 방지

            IsGameRunning = false;
            Time.timeScale = 0f; // 게임 정지
            
            int bestScore = 0;
            bool isNewRecord = false;

            if (DataManager.Instance != null)
            {
                isNewRecord = DataManager.Instance.CheckAndSaveBestScore(GameType.OrbitSync, currentScore);
                bestScore = DataManager.Instance.GetBestScore(GameType.OrbitSync);
            }

            // 게임오버 UI 표시
            if (gameOverUI != null)
            {
                gameOverUI.SetActive(true);
                scoreUI.ShowResult(currentScore, bestScore, isNewRecord);
            }

            // 전면 광고 재생
            if (AdManager.Instance != null)
            {
                AdManager.Instance.ScheduleInterstitial();
            }
        }
}
