using UnityEngine;
namespace GravitySplit
{
    public class GetScore : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                GravityGameManager.Instance.AddScore(1);
            }
        }
    }
}