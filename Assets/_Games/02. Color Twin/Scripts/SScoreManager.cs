using ColorTwin;
using TMPro;
using UnityEngine;

public class SScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int Score { get; private set; }

    void Start()
    {
        Score = 0;
        scoreText.text = Score.ToString();
    }

    private void OnEnable()
    {
        FallingCircle.onSpriteMatch += HandleSpriteMatch;
    }

    private void OnDisable()
    {
        FallingCircle.onSpriteMatch -= HandleSpriteMatch;
    }

    private void UpdateScoreUI()
    {
        scoreText.text = Score.ToString();
    }

    private void AddScore()
    {
        Score++;
    }

    private void HandleSpriteMatch()
    {
        AddScore();
        UpdateScoreUI();
    }
}
