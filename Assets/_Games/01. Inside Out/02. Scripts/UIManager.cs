using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private PlayerNodeManager pnManager;

    [SerializeField] private Button leftTouchZone;
    [SerializeField] private Button rightTouchZone;

    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private GameoverUI gameOverUI;

    [SerializeField] private TutorialUI tutorialUI;

    private int currentScore;

    void Awake()
    {
        pnManager = FindFirstObjectByType<PlayerNodeManager>();
    }

    void Start()
    {
        currentScore = 0;
        scoreText.text = currentScore.ToString();

        leftTouchZone.onClick.AddListener(() => pnManager.MoveLeftNode());
        rightTouchZone.onClick.AddListener(() => pnManager.MoveRightNode());


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
        bool isNewRecord = false;

        if (DataManager.Instance != null)
        {
            isNewRecord = DataManager.Instance.CheckAndSaveBestScore(GameType.InsideOut, currentScore);
            
            bestScore = DataManager.Instance.GetBestScore(GameType.InsideOut);
        }
        if (gameOverUI != null)
        {
            gameOverUI.gameObject.SetActive(true); // 패널 켜기
            gameOverUI.ShowResult(currentScore, bestScore, isNewRecord); // 점수 갱신
        }
        if (AdManager.Instance != null)
        {
            AdManager.Instance.ScheduleInterstitial();
        }
    }


}
