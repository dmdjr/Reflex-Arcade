using UnityEngine;
using UnityEngine.SceneManagement;

/* ======================================================================
 * [Class]: LobbyManager 
 * [Role] : 로비 씬에서 게임 씬으로 이동 및 초기 설정(모든 스크립트 내의 로그를 Off)을 담당
 * ====================================================================== */
public class LobbyManager : MonoBehaviour
{
    void Start()
    {
        // 게임 시작하면 하단 배너광고 상시 띄우기
        AdManager.Instance.ShowBanner();
    }
    void Awake()
    {
#if !UNITY_EDITOR
        Debug.unityLogger.logEnabled = false;
#endif
    }
    public void LoadGameScene(string sceneName)
    {
        // 게임 씬으로 넘어가면 하단 배너 광고 없애기
        AdManager.Instance.HideBanner();
        SceneManager.LoadScene(sceneName);
    }

}
