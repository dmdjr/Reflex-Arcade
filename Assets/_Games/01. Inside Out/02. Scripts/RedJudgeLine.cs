using UnityEngine;

public class RedJudgeLine : MonoBehaviour
{
    private ObstacleManager obstacleManager;

    void Start()
    {
        obstacleManager = FindFirstObjectByType<ObstacleManager>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        obstacleManager.SpawnRedObstacle();
    }
}
