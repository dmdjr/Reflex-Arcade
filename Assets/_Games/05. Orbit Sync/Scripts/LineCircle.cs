using UnityEngine;

public class LineCircle : MonoBehaviour
{
    public enum CircleType { Red, Green, Blue }

    [SerializeField] private CircleType circleType;

    private float moveSpeed;
    private Vector3 moveDirection;
    private CircleSpawner spawner;

    // 초기화 함수 (Spawner가 호출)
    public void Init(CircleSpawner spawner, float speed, Vector3 direction)
    {
        this.spawner = spawner;
        this.moveSpeed = speed;
        this.moveDirection = direction;

        CancelInvoke();
        Invoke(nameof(ReturnToPool), 10f);
    }

    private void Update()
    {
        if (OrbitSyncManager.Instance != null && !OrbitSyncManager.Instance.IsGameRunning)
            return;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void ReturnToPool()
    {
        CancelInvoke();
        if (gameObject.activeSelf)
        {
            spawner.ReturnCircle(this);
        }
    }

    public CircleType GetCircleType() => circleType;
}
