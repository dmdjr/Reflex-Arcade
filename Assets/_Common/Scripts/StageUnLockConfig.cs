using System;
using System.Collections.Generic;
using UnityEngine;

/* ======================================================================
/* 각 게임의 잠금 해제 조건(필요 점수)을 관리하는 스크립터블 오브젝트(데이터 컨테이너)
 * * [Method Summary]
 * 1. OnEnable        : 에디터에서 입력한 리스트(List) 데이터를 검색이 빠른 딕셔너리(Dictionary)로 변환
 * 2. GetRequiedScore : 특정 게임 타입(GameType)을 해금하기 위해 필요한 점수를 반환.
 * (데이터가 없을 경우 9999를 반환하여 잠금 상태 유지)
/* ====================================================================== */

[CreateAssetMenu(fileName = "StageUnLockConfig", menuName = "Scriptable Objects/Stage UnLock Config")]
public class StageUnLockConfig : ScriptableObject
{
    // 데이터 구조 정의 (게임 타입과 필요 점수)
    [System.Serializable]
    public struct UnlockRule
    {
        public GameType gameType;
        public int requiredScore;
    }

    // 에디터에서 입력할 리스트
    // 게임별 잠금 해제 조건 설정
    [SerializeField] private List<UnlockRule> rules;

    // 검색 속도를 위한 딕셔너리 (내부용)
    private Dictionary<GameType, int> ruleDict;

    private void OnEnable()
    {
        // 리스트에 적어둔 데이터들을 딕셔너리로 변환해서 정리
        ruleDict = new Dictionary<GameType, int>();
        foreach (var rule in rules)
        {
            ruleDict.Add(rule.gameType, rule.requiredScore);
        }
    }

    // 외부에서 점수를 물어보는 함수
    public int GetRequiedScore(GameType type)
    {
        // 초기화 안 되어있으면 다시 함
        if (ruleDict == null) OnEnable();

        if (ruleDict.TryGetValue(type, out int score))
        {
            return score;
        }
        // 만약 정보가 없다면 큰 수를 줘서 안 열리게 끔
        return 9999;
    }
}
