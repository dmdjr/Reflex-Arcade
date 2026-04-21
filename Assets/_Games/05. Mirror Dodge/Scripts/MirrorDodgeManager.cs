using UnityEngine;

/* ======================================================================
 * [Class]: MirrorDodgeManager
 * [Role] : 게임 상태(시작/종료)를 관리하는 핵심 매니저
 *          BaseGameManager 상속 → IsGameRunning, StartGame() 공통 제공
 * * [Method Summary]
 * 1. GameOver : 게임 종료 처리 (중복 호출 방지 포함)
 * ※ 추후 추가 예정: 점수 관리, 튜토리얼, 광고 연동
 * ====================================================================== */
namespace MirrorDodge
{

    public class MirrorDodgeManager : BaseGameManager
    {
        public static MirrorDodgeManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            // 튜토리얼 연동 전까지 임시로 바로 시작
            // BaseGameManager.StartGame() → 0.1초 딜레이 후 IsGameRunning = true
            StartGame();
        }

        public void GameOver()
        {
            // 두 공이 동시에 장애물에 닿았을 때 GameOver가 2번 호출되는 것을 방지
            if (!IsGameRunning) return;

            IsGameRunning = false;
            Time.timeScale = 0f;

            Debug.Log("Game Over");
            // 추후 추가: 점수 저장, GameoverUI 표시, 광고 호출
        }
    }
}
