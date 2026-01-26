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

    [SerializeField] private TutorialUI tutorialUI;

    private int currentScore, bestScore;

    void Awake()
    {
        pnManager = FindFirstObjectByType<PlayerNodeManager>();
        obManager = FindFirstObjectByType<ObstacleManager>();
    }

    void Start()
    {
        scoreText.text = currentScore.ToString();

        leftTouchZone.onClick.AddListener(onLeftClick);
        rightTouchZone.onClick.AddListener(onRightClick);

        retryButton.onClick.AddListener(Retry);
        homeButton.onClick.AddListener(() => Debug.Log("홈으로 이동"));

        Time.timeScale = 0f;

        // 팀원 게임에 맞는 GameType으로 변경 필요 (예: GameType.NodeGame)
        GameType myGameType = GameType.InsideOut;
        if (DataManager.Instance != null && DataManager.Instance.IsFirstSeen(myGameType))
        {
            // 처음이라면 튜토리얼 열기 (닫히면 RealGameStart 실행)
            tutorialUI.Open(myGameType, RealGameStart);
        }
        else
        {
            // 처음이 아니라면 UI 끄고 바로 시작
            tutorialUI.gameObject.SetActive(false);
            RealGameStart();
        }
    }
    private void RealGameStart()
    {
        Time.timeScale = 1f; // 멈췄던 시간을 다시 흐르게 함
        // 혹시 게임 시작 시 obManager 등을 초기화해야 한다면 여기서 호출
        // 예: obManager.Init(); 
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
