using UnityEngine;
using UnityEngine.SceneManagement;

/* ======================================================================
 * [Class]: LobbyManager 
 * [Role] : 로비 씬에서 게임 씬으로 이동 및 초기 설정(모든 스크립트 내의 로그를 Off)을 담당
 * ====================================================================== */
public class LobbyManager : MonoBehaviour
{

    void Awake()
    {
#if !UNITY_EDITOR
        Debug.unityLogger.logEnabled = false;
#endif
    }
    public void LoadGameScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
