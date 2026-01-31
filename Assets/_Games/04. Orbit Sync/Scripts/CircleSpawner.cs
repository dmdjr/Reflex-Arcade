using System.Collections.Generic;
using UnityEngine;

    public class CircleSpawner : MonoBehaviour
    {
        [SerializeField] private LineCircle redPrefab;
        [SerializeField] private LineCircle greenPrefab;
        [SerializeField] private LineCircle bluePrefab;

        [SerializeField] private float spawnYDistance = 8f;   // 생성 위치 (Y축 거리)
        [SerializeField] private float minSpawnInterval = 1f; // 최소 생성 주기
        [SerializeField] private float maxSpawnInterval = 2f; // 최대 생성 주기
        [SerializeField] private float circleSpeed = 3f;      // 원 이동 속도

        // 오브젝트 풀
        private Dictionary<LineCircle.CircleType, Queue<LineCircle>> circlePools;

        private float timer;
        private float nextSpawnTime;

        private void Awake()
        {
            InitializePool();
        }

        private void Start()
        {
            SetNextSpawnTime();
        }

        private void Update()
    {
            timer += Time.deltaTime;

            if (timer >= nextSpawnTime)
            {
                SpawnLogic();
                timer = 0f;
                SetNextSpawnTime();
            }
        }

        private void SpawnLogic()
        {
            bool isTop = Random.Range(0, 2) == 0;

            LineCircle.CircleType selectedType;

            if (isTop)
            {
                selectedType = (Random.Range(0, 2) == 0) ? LineCircle.CircleType.Red : LineCircle.CircleType.Green;
            }
            else
            {
                selectedType = (Random.Range(0, 2) == 0) ? LineCircle.CircleType.Red : LineCircle.CircleType.Blue;
            }

            SpawnCircle(selectedType, isTop);
        }

        private void SpawnCircle(LineCircle.CircleType type, bool isTop)
        {
            LineCircle circle = GetCircleFromPool(type);

            Vector3 spawnPos = isTop ? new Vector3(0, spawnYDistance, 0) : new Vector3(0, -spawnYDistance, 0);
            Vector3 moveDir = isTop ? Vector3.down : Vector3.up;

            circle.transform.position = spawnPos;
            circle.transform.rotation = Quaternion.identity;
            circle.gameObject.SetActive(true);

            circle.Init(this, circleSpeed, moveDir);
        }


        private void InitializePool()
        {
            circlePools = new Dictionary<LineCircle.CircleType, Queue<LineCircle>>
        {
            { LineCircle.CircleType.Red, new Queue<LineCircle>() },
            { LineCircle.CircleType.Green, new Queue<LineCircle>() },
            { LineCircle.CircleType.Blue, new Queue<LineCircle>() }
        };
        }

        private LineCircle GetCircleFromPool(LineCircle.CircleType type)
        {
            if (circlePools[type].Count > 0)
            {
                return circlePools[type].Dequeue();
            }
            else
            {
                LineCircle newCircle = Instantiate(GetPrefab(type), transform);
                return newCircle;
            }
        }

        public void ReturnCircle(LineCircle circle)
        {
            circle.gameObject.SetActive(false);
            circlePools[circle.GetCircleType()].Enqueue(circle);
        }

        private LineCircle GetPrefab(LineCircle.CircleType type)
        {
            switch (type)
            {
                case LineCircle.CircleType.Red: return redPrefab;
                case LineCircle.CircleType.Green: return greenPrefab;
                default: return bluePrefab;
            }
        }

        private void SetNextSpawnTime()
        {
            nextSpawnTime = Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }
