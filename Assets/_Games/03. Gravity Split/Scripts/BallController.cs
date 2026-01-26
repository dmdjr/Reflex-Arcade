using UnityEngine;

namespace GravitySplit
{
    public class BallController : MonoBehaviour
    {
        private Rigidbody2D rb;
        private bool isJumping;

        [SerializeField] private float jumpForce = 4f;
        [SerializeField] private bool isTopball;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        void Update()
        {

            if (GravityGameManager.Instance.IsGameRunning == false)
            {
                if (rb.simulated) rb.simulated = false; // 물리 시뮬레이션 중단
                return;
            }
            if (GravityGameManager.Instance.IsGameRunning == false) return;

            // 게임이 다시 시작되거나 실행 중이면 시뮬레이션 활성화
            if (!rb.simulated) rb.simulated = true;

#if UNITY_EDITOR
            // 에디터 테스트용
            if (Input.GetMouseButtonDown(0))
            {
                HandleJump(Input.mousePosition);
            }
#endif
            // 모바일 터치 입력 처리
            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch touch = Input.GetTouch(i);
                    if (touch.phase == TouchPhase.Began)
                    {
                        HandleJump(touch.position);
                    }
                }
            }
        }

        void HandleJump(Vector2 inputPos)
        {
            bool isTouchTop = inputPos.y > Screen.height / 2;

            if (isTopball && isTouchTop && !isJumping)
            {
                Jump(Vector2.up);
            }
            else if (!isTopball && !isTouchTop && !isJumping)
            {
                Jump(Vector2.down);
            }

        }
        void Jump(Vector2 dir)
        {
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(dir * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Line"))
            {
                isJumping = false;
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                GravityGameManager.Instance.GameOver();
                rb.linearVelocity = Vector2.zero;
                rb.simulated = false;
                HitEffect.Instance.PlayHighlight(collision.transform);
            }
        }
    }

}
