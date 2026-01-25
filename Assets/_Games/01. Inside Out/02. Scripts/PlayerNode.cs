using UnityEngine;

public class PlayerNode : MonoBehaviour
{
    private UIManager uiManager;
    
    private BoxCollider2D boxCollider2D;
    
    void Start()
    {
        uiManager = FindFirstObjectByType<UIManager>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            uiManager.GameOver();
        }
    }
}
