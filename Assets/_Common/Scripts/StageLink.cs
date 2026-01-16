using UnityEngine;

/* ======================================================================
/* 로비(Lobby)에서 스테이지 간 연결 화살표의 잠금/해제 시각 효과(View)를 담당하는 클래스
 * * [Method Summary]
 * 1. Start            : 씬 시작 시 화살표 상태를 초기화
 * 2. UpdateArrowState : Config(설정)의 요구 점수와 DataManager(내 점수)를 비교하여,
 * 조건 달성 여부에 따라 회색(잠김) 또는 파란색(해제) 화살표를 활성화
/* ====================================================================== */

public class StageLink : MonoBehaviour
{
    [SerializeField] private GameType targetGameType;
    [SerializeField] private StageUnLockConfig config;

    [SerializeField] private GameObject unclearArrow;
    [SerializeField] private GameObject clearArrow;

    void Start()
    {
        UpdateArrowState();
    }

    public void UpdateArrowState()
    {
        if (DataManager.Instance == null && config == null) return;

        int requiredScore = config.GetRequiedScore(targetGameType);
        int bestScore = DataManager.Instance.GetBestScore(targetGameType);

        bool isUnlocked = bestScore >= requiredScore;

        if (unclearArrow != null) unclearArrow.SetActive(!isUnlocked);
        if (clearArrow != null) clearArrow.SetActive(isUnlocked);
    }
}
