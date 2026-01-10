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
}