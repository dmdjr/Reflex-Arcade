using TMPro;
using UnityEngine;

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

        private bool SaveScore()
        {
            if (DataManager.Instance != null)
            {
                return DataManager.Instance.CheckAndSaveBestScore(GameType.ColorTwin, Score);
            }
            return false;
        }

        private void ShowGameOverUI(bool isNewRecord)
        {
            if (gameOverUI != null)
                gameOverUI.SetActive(true);

            if (DataManager.Instance != null)
            {
                int bestScore = DataManager.Instance.GetBestScore(GameType.ColorTwin);
                
                if (gameOverUI != null)
                {
                    gameOverUI.GetComponent<GameoverUI>().ShowResult(Score, bestScore, isNewRecord);
                }
            }


        }

        private void HandleSpriteMatch()
        {
            AddScore();
            UpdateScoreUI();
        }

        private void HandleMisSpriteMatch(FallingCircle targetCircle)
        {
            if (targetCircle == null) return;
            bool isNewRecord = SaveScore();
            ShowGameOverUI(isNewRecord);
        }
    }
}