using UnityEngine;

namespace GravitySplit
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private float movespeed = 5f;
        void Update()
        {
            if (GravityGameManager.Instance != null && !GravityGameManager.Instance.IsGameRunning)
                return;
            transform.Translate(Vector2.left * movespeed * Time.deltaTime);
        }
    }
}