using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float movespeed = 5f;
    void Update()
    {
        transform.Translate(Vector2.left * movespeed * Time.deltaTime);
    }
}
