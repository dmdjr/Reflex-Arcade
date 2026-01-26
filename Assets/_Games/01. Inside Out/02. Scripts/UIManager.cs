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

    [SerializeField] private GameoverUI gameOverUI;

    [SerializeField] private TutorialUI tutorialUI;

    private int currentScore = 0;

    void Awake()
    {
        pnManager = FindFirstObjectByType<PlayerNodeManager>();
        obManager = FindFirstObjectByType<ObstacleManager>();
    }

    void Start()
    {
        currentScore = 0;
        scoreText.text = currentScore.ToString();

        leftTouchZone.onClick.AddListener(onLeftClick);
        rightTouchZone.onClick.AddListener(onRightClick);


        Time.timeScale = 0f;

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
    void Update()
    {
        if (gameOverUI != null && gameOverUI.gameObject.activeSelf && Time.timeScale > 0f)
        {
            Time.timeScale = 0f;
        }
    }
    private void RealGameStart()
    {
        Time.timeScale = 1f;
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

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayScoreSound();
        }
    }



    public void GameOver()
    {
        Time.timeScale = 0;
        scoreText.gameObject.SetActive(false);
        int bestScore = 0;
        if (DataManager.Instance != null)
        {
            // 현재 점수 저장 시도
            DataManager.Instance.CheckAndSaveBestScore(GameType.InsideOut, currentScore);
            // 최고 점수 가져오기
            bestScore = DataManager.Instance.GetBestScore(GameType.InsideOut);
        }
        if (gameOverUI != null)
        {
            gameOverUI.gameObject.SetActive(true); // 패널 켜기
            gameOverUI.ShowResult(currentScore, bestScore); // 점수 갱신
        }
        if (AdManager.Instance != null)
        {
            AdManager.Instance.ScheduleInterstitial();
        }
    }


}
