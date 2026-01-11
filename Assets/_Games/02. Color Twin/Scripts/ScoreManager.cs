using TMPro;
using UnityEngine;
using GravitySplit;

namespace ColorTwin
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private GameObject gameOverUI;
        
        public int Score { get; private set; }

        void Start()
        {
            Score = 0;
            UpdateScoreUI();

            if (gameOverUI != null)
                gameOverUI.SetActive(false);
        }

        private void OnEnable()
        {
            FallingCircle.onSpriteMatch += HandleSpriteMatch;
            FallingCircle.onSpriteMismatch += HandleMisSpriteMatch;
        }

        private void OnDisable()
        {
            FallingCircle.onSpriteMatch -= HandleSpriteMatch;
            FallingCircle.onSpriteMismatch -= HandleMisSpriteMatch;
        }

        private void UpdateScoreUI()
        {
            scoreText.text = Score.ToString();
        }

        private void AddScore()
        {
            Score++;
        }

        private void SaveScore()
        {
            DataManager.Instance.CheckAndSaveBestScore(GameType.ColorTwin, Score);
        }

        private void ShowGameOverUI()
        {
            if (DataManager.Instance != null)
            {
                int bestScore = DataManager.Instance.GetBestScore(GameType.ColorTwin);
                gameOverUI.GetComponent<GameoverUI>().ShowResult(Score, bestScore);
            }

            if (gameOverUI != null)
                gameOverUI.SetActive(true);
        }

        private void HandleSpriteMatch()
        {
            AddScore();
            UpdateScoreUI();
        }

        private void HandleMisSpriteMatch()
        {
            SaveScore();
            ShowGameOverUI();
        }
    }
}