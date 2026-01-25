using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private PlayerNodeManager pnManager;
    private ObstacleManager obManager;
    
    [SerializeField] private Button leftTouchZone;
    [SerializeField] private Button rightTouchZone;
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button homeButton;
    
    private int currentScore, bestScore;
    
    void Awake()
    {
        pnManager = FindFirstObjectByType<PlayerNodeManager>();
        obManager =  FindFirstObjectByType<ObstacleManager>();
    }
    
    void Start()
    {
        scoreText.text = currentScore.ToString();
        
        leftTouchZone.onClick.AddListener(onLeftClick);
        rightTouchZone.onClick.AddListener(onRightClick);
        
        retryButton.onClick.AddListener(Retry);
        homeButton.onClick.AddListener(() => Debug.Log("홈으로 이동"));
    }

    private void onLeftClick()
    {
        pnManager.MoveLeftNode();
    }

    private void onRightClick()
    {
        pnManager.MoveRightNode();
    }

    public void AddScoreOnce()
    {
        currentScore++;
        scoreText.text = currentScore.ToString();
    }

    public void ResetScore()
    {
        currentScore = 0;
        scoreText.text = "0";
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0;
        scoreText.gameObject.SetActive(false);
        if (currentScore > bestScore)
            bestScore = currentScore;
        bestScoreText.text = $"Best {bestScore.ToString()}";
        finalScoreText.text = $"Score {currentScore.ToString()}";
    }

    private void Retry()
    {
        ResetScore();
        gameOverUI.SetActive(false);
        Time.timeScale = 1;
        scoreText.gameObject.SetActive(true);
        obManager.Init();
    }
}
