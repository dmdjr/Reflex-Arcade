using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    
    private Collider2D scoreCollider;

    void Awake()
    {
        scoreCollider = GetComponent<Collider2D>();
        
        if (uiManager == null)
            Debug.LogError("UIManager가 할당되지 않았습니다.");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            uiManager.AddScoreOnce();
        }
    }
}
