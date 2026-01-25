using UnityEngine;

public class GreenCollectLine : MonoBehaviour
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
            obstacleManager.DispawnGreenObstacle(other);
        }
    }
}