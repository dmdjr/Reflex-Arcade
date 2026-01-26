using System.Collections;
using UnityEngine;

/* ======================================================================
/* 모든 게임 매니저의 부모 클래스로, 게임 상태(실행 여부)와 시작 로직을 공통 관리
 * * [Method Summary]
 * 1. StartGame        : 게임 시작 코루틴을 호출하여 안전하게 게임을 시작
 * 2. StartGameRoutine : 0.1초 딜레이를 두어 UI 터치 입력이 인게임 동작으로 이어지는 것을 방지하고, 
 * IsGameRunning을 true로 변경하며 시간을 흐르게 함(TimeScale 1)
/* ====================================================================== */

public class BaseGameManager : MonoBehaviour
{
    public bool IsGameRunning { get; protected set; } = false;


    // 공통 기능: 게임 시작 (0.1초 딜레이 포함 -> 튜토리얼 플레이 버튼 터치 시 인게임에서 바로 반응하는 걸 막기 위함)
    public void StartGame()
    {
        StartCoroutine(StartGameRoutine());
    }

    private IEnumerator StartGameRoutine()
    {
        yield return new WaitForSeconds(0.1f);

        IsGameRunning = true;
        Time.timeScale = 1f;
    }

    
}
