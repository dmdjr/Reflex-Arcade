using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float speed;

    void Update()
    {
        transform.Translate(Time.deltaTime * speed * Vector2.down);
    }
}
