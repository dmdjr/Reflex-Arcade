using System.Collections.Generic;
using UnityEngine;

public class ObstacleSetter : MonoBehaviour
{
    [SerializeField] private GameObject baseRedObstaclePrefab;
    [SerializeField] private GameObject baseGreenObstaclePrefab;

    public float[] multipliers = { 0.5f, 1f, 2f, 3f };
    [SerializeField] private List<GameObject> redObstacles;
    [SerializeField] private List<GameObject> greenObstacles;
    
    void Awake()
    {
        if (baseRedObstaclePrefab == null || baseGreenObstaclePrefab == null)
            Debug.LogError("장애물 프리팹이 할당되지 않았습니다.");
    }
    
    void Start()
    {
        foreach (float multiplier in multipliers)
        {
            GameObject obstacle = Instantiate(baseRedObstaclePrefab);
            
            Transform passPointTr = obstacle.transform.GetChild(0);
            passPointTr.localPosition = new Vector3(1, passPointTr.localPosition.y * multiplier, 1);
            
            SpriteRenderer sr = obstacle.GetComponent<SpriteRenderer>();
            sr.size = new Vector2(1f, multiplier);

            redObstacles.Add(obstacle);
        }
    }

    void Update()
    {
        
    }
}
