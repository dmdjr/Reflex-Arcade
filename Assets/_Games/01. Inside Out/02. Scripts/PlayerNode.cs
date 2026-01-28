using UnityEngine;

public class PlayerNode : MonoBehaviour
{
    private UIManager uiManager;
    
    void Start()
    {
        uiManager = FindFirstObjectByType<UIManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            if (HitEffect.Instance != null)
            {
                HitEffect.Instance.PlayHighlight(other.transform);
            }
            uiManager.GameOver();
        }
    }
}
