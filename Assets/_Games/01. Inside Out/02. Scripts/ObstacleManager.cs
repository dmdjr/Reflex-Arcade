using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private GameObject[] redObstaclePrefabs;
    [SerializeField] private GameObject[] greenObstaclePrefabs;
    
    // 사용할 Obstacle 오브젝트들 (비활성화된 Obstacle만 존재)
    private List<GameObject> redObstaclePools = new List<GameObject>();
    private List<GameObject> greenObstaclePools = new List<GameObject>();
    private GameObject obstaclePool;

    [SerializeField] private GameObject lines;
    private Collider2D redJudgeLine, greenJudgeLine, redCollectLine, greenCollectLine;
    
    [SerializeField] private GameObject SpawnPoint;
    private Transform way1, way2, way3, way4;

    void Awake()
    {
        redJudgeLine = lines.transform.GetChild(0).GetComponent<Collider2D>();
        greenJudgeLine = lines.transform.GetChild(1).GetComponent<Collider2D>();
        redCollectLine = lines.transform.GetChild(2).GetComponent<Collider2D>();
        greenCollectLine = lines.transform.GetChild(3).GetComponent<Collider2D>();
        
        way1 = SpawnPoint.transform.GetChild(0).GetComponent<Transform>();
        way2 = SpawnPoint.transform.GetChild(1).GetComponent<Transform>();
        way3 = SpawnPoint.transform.GetChild(2).GetComponent<Transform>();
        way4 = SpawnPoint.transform.GetChild(3).GetComponent<Transform>();
        
        obstaclePool = GameObject.Find("Obstacles");
    }
    
    void Start()
    {
        Init();
    }

    // 풀 초기화 및 Obstacle 재생성
    public void Init()
    {
        foreach (Transform child in obstaclePool.transform)
        {
            Destroy(child.gameObject);
        }
        
        redObstaclePools.Clear();
        greenObstaclePools.Clear();
        
        # region 오브젝트 풀 생성 및 할당
        // 오브젝트 풀링, Obstacle 오브젝트 생성 (이곳에서 Obstacle의 종류 및 개수 조절)
        // 0.5 - 3개
        // 1 - 2개
        // 2 - 2개
        // 3 - 1개
        for (int i = 0; i < 3; i++)
        {
            
            redObstaclePools.Add(Instantiate(redObstaclePrefabs[0], obstaclePool.transform));
            greenObstaclePools.Add(Instantiate(greenObstaclePrefabs[0], obstaclePool.transform));
            redObstaclePools.Add(Instantiate(redObstaclePrefabs[1], obstaclePool.transform));
            greenObstaclePools.Add(Instantiate(greenObstaclePrefabs[1], obstaclePool.transform));
        }

        for (int i = 0; i < 2; i++)
        {
            redObstaclePools.Add(Instantiate(redObstaclePrefabs[2], obstaclePool.transform));
            greenObstaclePools.Add(Instantiate(greenObstaclePrefabs[2], obstaclePool.transform));
        }
        
        redObstaclePools.Add(Instantiate(redObstaclePrefabs[3], obstaclePool.transform));
        greenObstaclePools.Add(Instantiate(greenObstaclePrefabs[3], obstaclePool.transform));
        
        foreach (GameObject obj in redObstaclePools)
            obj.SetActive(false);
        foreach (GameObject obj in greenObstaclePools)
            obj.SetActive(false);
        # endregion

        SpawnRedObstacle();
        SpawnGreenObstacle();
    }

    public void SpawnRedObstacle()
    {
        GameObject newObj = redObstaclePools[Random.Range(0, redObstaclePools.Count)];
        redObstaclePools.Remove(newObj);
        newObj.transform.position = GetRandomRedPosition();
        newObj.SetActive(true);
    }

    public void SpawnGreenObstacle()
    {
        GameObject newObj = greenObstaclePools[Random.Range(0, greenObstaclePools.Count)];
        greenObstaclePools.Remove(newObj);
        newObj.transform.position = GetRandomGreenPosition();
        newObj.SetActive(true);
    }

    public void DispawnRedObstacle(Collider2D collider)
    {
        GameObject oldObj = collider.gameObject;
        oldObj.SetActive(false);
        redObstaclePools.Add(oldObj);
    }

    public void DispawnGreenObstacle(Collider2D collider)
    {
        GameObject oldObj = collider.gameObject;
        oldObj.SetActive(false);
        greenObstaclePools.Add(oldObj);
    }

    private Vector2 GetRandomRedPosition()
    {
        int randomIndex = Random.Range(0, 2);
        if (randomIndex == 0)
            return way1.position;
        else
            return way2.position;
    }

    private Vector2 GetRandomGreenPosition()
    {
        int randomIndex = Random.Range(0, 2);
        if (randomIndex == 0)
            return way3.position;
        else
            return way4.position;
    }
}
