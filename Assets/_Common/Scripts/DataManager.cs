using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    void Awake()
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

    // 최고 점수 가져오기
    public int GetBestScore(GameType type)
    {
        return PlayerPrefs.GetInt("BestScore_" + type.ToString(), 0);
    }

    // 최고 점수 저장하기 (현재 점수가 더 높을 때만)
    public void CheckAndSaveBestScore(GameType type, int currentScore)
    {
        int oldBest = GetBestScore(type);
        if (currentScore > oldBest)
        {
            PlayerPrefs.SetInt("BestScore_" + type.ToString(), currentScore);
            PlayerPrefs.Save();
        }
    }
}