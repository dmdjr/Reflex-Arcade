using System;
using GravitySplit;
using UnityEngine;

/* ======================================================================
/* 게임 진입 시 출력되는 튜토리얼 팝업 UI를 제어하고, 게임 시작 콜백을 처리하는 클래스
 * * [Method Summary]
 * 1. Open              : 튜토리얼 UI를 활성화하고 시간을 정지(TimeScale 0)시키며, 게임 시작 시 실행할 함수(Action)를 등록
 * 2. OnTouchPlayButton : 플레이 버튼 클릭 시 튜토리얼 시청 여부를 저장하고, 시간을 다시 흐르게(TimeScale 1) 한 뒤 
 * 등록된 게임 시작 함수(Callback)를 실행
/* ====================================================================== */

public class TutorialUI : MonoBehaviour
{
    private GameType gameType;
    private Action onGameStart; // 게임 시작 시 호출할 콜백 

    // 각 게임 씬의 매니저에서 호출해서 TutorialUI를 열면 됨
    public void Open(GameType type, Action gameStartCallback)
    {
        gameType = type;
        onGameStart = gameStartCallback;
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    // 튜토리얼 화면에서 플레이 버튼 눌렀을 때
    public void OnTouchPlayButton()
    {
        // 튜토리얼 봤다고 도장 찍기
        if (DataManager.Instance != null)
        {
            DataManager.Instance.SetTutorialSeen(gameType);
        }
        Time.timeScale = 1f;
        onGameStart?.Invoke();
        gameObject.SetActive(false);
    }
}
