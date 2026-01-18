using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

/* ======================================================================
/* 스테이지 진입 버튼의 잠금(Lock) 및 해제(Unlock) 상태를 제어하는 클래스
 * * [Method Summary]
 * 1. Start       : 게임 시작 시 현재 데이터를 기반으로 버튼 상태 초기화
 * 2. CheckUnlock : 필요 조건(prerequisiteGames)들이 모두 목표 점수(Config)를 넘었는지 검사
 * 3. UpdateUI    : 검사 결과에 따라 잠금 아이콘을 끄고 플레이 버튼을 활성화
/* ====================================================================== */

public class StageLockView : MonoBehaviour
{
    [SerializeField] private StageUnLockConfig config;

    // [확장성 핵심] 이 스테이지를 열기 위해 클리어해야 하는 게임들 리스트
    // 예: GravitySplit을 열려면 -> InsideOut, ColorTwin 두 개를 리스트에 등록
    [SerializeField] private List<GameType> prerequisiteGames;

    [SerializeField] private GameObject lockIcon;
    [SerializeField] private Button playButton;

    void Start()
    {
        UpdateUI();
    }

    private bool CheckUnlock()
    {
        if (DataManager.Instance == null || config == null) return false;

        foreach (var targetGame in prerequisiteGames)
        {
            // 롤북에서 통과 기준 점수 가져오기
            int requiredScore = config.GetRequiedScore(targetGame);
            // 내 최고 점수 가져오기
            int myBestScore = DataManager.Instance.GetBestScore(targetGame);

            if (myBestScore < requiredScore)
            {
                return false; // 하나라도 조건 미달이면 잠김
            }
        }
        return true; // 모두 조건 달성 시 해제
    }

    public void UpdateUI()
    {
        bool isUnlocked = CheckUnlock();

        if (lockIcon != null) lockIcon.SetActive(!isUnlocked);

        if (!isUnlocked)
        {
            Button lockBtn = lockIcon.GetComponent<Button>();
            if (lockBtn != null)
            {
                lockBtn.onClick.RemoveAllListeners();
                lockBtn.onClick.AddListener(OnLockClick);
            }
        }

        if (playButton != null) playButton.gameObject.SetActive(isUnlocked);
    }
    private void OnLockClick()
    {
        if (StagePopupManager.Instance != null && lockIcon != null)
        {
            StagePopupManager.Instance.Show(lockIcon.transform.position);
        }
    }
}
