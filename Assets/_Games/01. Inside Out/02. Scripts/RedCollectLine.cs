using UnityEngine;

public class RedCollectLine : MonoBehaviour
{
    private ObstacleManager obstacleManager;

    void Start()
    {
        obstacleManager = FindFirstObjectByType<ObstacleManager>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            obstacleManager.DispawnRedObstacle(other);
        }
    }
}
