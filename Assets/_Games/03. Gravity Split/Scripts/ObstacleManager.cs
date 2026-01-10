using System.Collections.Generic;
using UnityEngine;

/* ======================================================================
/* 위(Red), 아래(Green) 장애물들의 생성과 무한 배치를 관리하는 클래스
 * * [Method Summary]
 * 1. SpawnObstacles : 게임 시작 시 지정된 개수만큼 장애물을 초기 생성하고 리스트에 등록
 * 2. CheckPosition  : (Update) 활성화된 모든 장애물의 위치를 매 프레임 검사
 * 3. Reposition     : 화면 왼쪽 밖으로 나간 장애물을 찾아 오른쪽 끝으로 랜덤 재배치
/* ====================================================================== */

public class ObstacleManager : MonoBehaviour
{
    public GameObject redPrefab;
    public GameObject greenPrefab;

    private List<GameObject> redObstacles = new List<GameObject>();
    private List<GameObject> greenObstacles = new List<GameObject>();


    private float[] redYPosCandidates = new float[] { 0.33f, 1.1f };
    private float[] greenYPosCandidates = new float[] { -0.33f, -1.1f };

    void Start()
    {
        // 빨간색 생성 (위쪽)
        SpawnObstacles(redPrefab, redObstacles, redYPosCandidates, 3f);

        // 초록색 생성 (아래쪽) - 시작 X 위치를 빨강이랑 살짝 다르게 3.5f로 줘서 엇박자 느낌 냄
        SpawnObstacles(greenPrefab, greenObstacles, greenYPosCandidates, 3.5f);
    }

    void SpawnObstacles(GameObject prefab, List<GameObject> list, float[] yCandidates, float startX)
    {
        float currentX = startX;

        for (int i = 0; i < 5; i++)
        {
            GameObject obj = Instantiate(prefab);
            float randomY = yCandidates[Random.Range(0, yCandidates.Length)];
            obj.transform.position = new Vector3(currentX, randomY, 0);
            list.Add(obj);
            float randomGap = Random.Range(2f, 4f);
            currentX += randomGap;
        }
    }
    void Update()
    {
        // 빨간색 검사
        CheckPosition(redObstacles, redYPosCandidates);

        // 초록색 검사
        CheckPosition(greenObstacles, greenYPosCandidates);
    }
    void CheckPosition(List<GameObject> targetList, float[] yCandidates)
    {
        foreach (var obj in targetList)
        {
            // 화면 왼쪽 밖으로 나갔는지 확인
            if (obj.transform.position.x < -3f)
            {
                Reposition(obj, targetList, yCandidates);
            }
        }
    }
    void Reposition(GameObject targetObj, List<GameObject> groupList, float[] yCandidates)
    {
        float maxX = -Mathf.Infinity;
        // "내 그룹(빨강이면 빨강들)" 중에서 가장 뒤에 있는 놈을 찾음
        foreach (var obj in groupList)
        {
            if (obj.transform.position.x > maxX)
            {
                maxX = obj.transform.position.x;
            }
        }

        float nextGap = Random.Range(2f, 4f);

        // 내 그룹의 Y 후보군 중에서 랜덤 선택
        float randomY = yCandidates[Random.Range(0, yCandidates.Length)];

        targetObj.transform.position = new Vector3(maxX + nextGap, randomY, 0);
    }

}

