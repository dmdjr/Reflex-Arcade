using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject baseRedObstaclePrefab;
    [SerializeField] private GameObject baseGreenObstaclePrefab;
    
    private GameObject redHalf, red1x, red2x, red3x, greenHalf, green1x, green2x, green3x;

    private GameObject[] redObstaclePools;
    private GameObject[] greenObstaclePools;

    void Awake()
    {
        if (baseRedObstaclePrefab == null || baseGreenObstaclePrefab == null)
            Debug.LogError("장애물 프리팹이 할당되지 않았습니다.");
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
