using System;
using GravitySplit;
using UnityEngine;

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
