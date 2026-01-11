using System.Collections;
using UnityEngine;

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
