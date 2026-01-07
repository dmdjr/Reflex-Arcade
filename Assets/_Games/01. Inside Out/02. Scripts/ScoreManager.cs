using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    private BoxCollider2D scoreCollider;

    void Awake()
    {
        scoreCollider = GetComponent<BoxCollider2D>();
        
        if (uiManager == null)
            Debug.LogError("UIManager가 할당되지 않았습니다.");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PassPoint"))
        {
            uiManager.AddScoreOnce();
        }
    }
}
