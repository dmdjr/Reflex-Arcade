using System;
using UnityEngine;

/* ======================================================================
 * [Class]: MirrorDodgeInputHandler (Singleton)
 * [Role] : 화면 터치를 감지하고, 상단/하단 영역 탭 이벤트를 발행
 *          BallController가 이 이벤트를 구독하여 반응
 * * [Event Summary]
 * 1. OnTopZoneTapped    : 화면 상단 탭 시 발행 → 위쪽 공이 구독
 * 2. OnBottomZoneTapped : 화면 하단 탭 시 발행 → 아래쪽 공이 구독
 * ====================================================================== */
namespace MirrorDodge
{
    public class MirrorDodgeInputHandler : Singleton<MirrorDodgeInputHandler>
    {
        // 탭 이벤트 — BallController가 구독하여 자신의 Flip() 호출 여부를 결정
        public event Action OnTopZoneTapped;
        public event Action OnBottomZoneTapped;

        private void Update()
        {
#if UNITY_EDITOR
            // 에디터 전용: 마우스 동시 클릭이 불가능하므로 키보드로 대체
            // ← 왼쪽 화살표 → 위쪽 공 (위쪽 공이 화면 왼편에 위치)
            // → 오른쪽 화살표 → 아래쪽 공 (아래쪽 공이 화면 오른편에 위치)
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                OnTopZoneTapped?.Invoke();

            if (Input.GetKeyDown(KeyCode.RightArrow))
                OnBottomZoneTapped?.Invoke();
#else
            foreach (Touch touch in Input.touches)
            {
                // Began 페이즈만 처리 → 탭 당 1회만 이벤트 발행 (꾹 눌러도 반복 안 됨)
                if (touch.phase == TouchPhase.Began)
                    FireZoneEvent(touch.position);
            }
#endif
        }

        // 터치 위치가 화면 세로 중앙 기준 위/아래 어느 쪽인지 판별 후 이벤트 발행
        private void FireZoneEvent(Vector2 screenPos)
        {
            if (screenPos.y > Screen.height / 2f)
                OnTopZoneTapped?.Invoke();
            else
                OnBottomZoneTapped?.Invoke();
        }
    }
}
