using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* ==================================================================================
 * [Class]: StageLockView
 * [Role] : 개별 스테이지의 잠금 상태를 관리하고, 클릭 시 해금 조건을 팝업 매니저에게 '주문'하는 역할
 * * [Flow]
 * 1. CheckUnlock : Config(점수표)와 DataManager(내 점수)를 비교하여 해금 여부 판단
 * 2. UpdateUI    : 판단 결과에 따라 자물쇠를 켜거나, 플레이 버튼을 활성화
 * 3. OnLockClick : 잠겨있다면, 필요한 조건 리스트(prerequisiteGames)를 매니저에게 전달하며 팝업 요청
 * ================================================================================== */

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
            StagePopupManager.Instance.Show(lockIcon.transform.position,prerequisiteGames);
        }
    }
}
