using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private PlayerNodeManager pnManager;
    
    [SerializeField] private Button leftTouchZone;
    [SerializeField] private Button rightTouchZone;
    // [SerializeField] private Button pauseButton;
    [SerializeField] private TextMeshProUGUI scoreText;

    private int currentScore;
    
    void Awake()
    {
        pnManager = FindFirstObjectByType<PlayerNodeManager>();
    }
    
    void Start()
    {
        scoreText.text = currentScore.ToString();
        
        leftTouchZone.onClick.AddListener(onLeftClick);
        rightTouchZone.onClick.AddListener(onRightClick);
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
}
