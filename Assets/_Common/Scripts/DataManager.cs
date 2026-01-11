using System;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 점수 저장 및 불러오기
    public void CheckAndSaveBestScore(GameType type, int score)
    {
        int currentScore = GetBestScore(type);
        if (score > currentScore)
        {
            PlayerPrefs.SetInt("BestScore_" + type.ToString(), score);
            PlayerPrefs.Save();
        }
    }
    public int GetBestScore(GameType type)
    {
        return PlayerPrefs.GetInt("BestScore_" + type.ToString(), 0);
    }


    // 튜토리얼을 봤는지 확인
    public bool IsFirstSeen(GameType type)
    {
        // 예) "Tutorial_GravitySplit" 키가 0이면 처음 본 거, 1이면 이미 본 거
        return PlayerPrefs.GetInt("Tutorial_" + type.ToString(), 0) == 0;
    }

    // 튜토리얼 봤다고 도장 찍기
    public void SetTutorialSeen(GameType type)
    {
        PlayerPrefs.SetInt("Tutorial_" + type.ToString(), 1);
        PlayerPrefs.Save();
    }

}