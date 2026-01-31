using UnityEngine;
using UnityEngine.SceneManagement;

/* ======================================================================
 * [Class]: LobbyManager 
 * [Role] : 로비 씬 관리 (게임 진입, 배너 광고 등)
 * ====================================================================== */
public class LobbyManager : MonoBehaviour
{

    void Awake()
    {
#if !UNITY_EDITOR
        Debug.unityLogger.logEnabled = false;
#endif
    }


    void Start()
    {
        // 게임 시작하면 하단 배너광고 상시 띄우기
        AdManager.Instance.ShowBanner();
    }

    public void LoadGameScene(string sceneName)
    {
        // 게임 씬으로 넘어가면 하단 배너 광고 없애기
        AdManager.Instance.HideBanner();
        SceneManager.LoadScene(sceneName);
    }
    
}
