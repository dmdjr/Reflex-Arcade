using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public void LoadGameScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
