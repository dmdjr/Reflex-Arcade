using UnityEngine;

public class ObstacleCollHandler : MonoBehaviour
{
    [SerializeField] private LineCircle.CircleType myType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        LineCircle incomingCircle = other.GetComponent<LineCircle>();

        if (incomingCircle != null)
        {
            if (incomingCircle.GetCircleType() != myType)
            {
                if (HitEffect.Instance != null)
                {
                    HitEffect.Instance.PlayHighlight(other.transform);
                }

                OrbitSyncManager.Instance.GameOver();

            }
            else
            {
                OrbitSyncManager.Instance.AddScore(1);
                incomingCircle.ReturnToPool();
            }
        }
    }
}
