using UnityEngine;

public class AppManager : MonoBehaviour
{
    public static AppManager Instance;

    private float backBtnTime = 0f;

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.time - backBtnTime < 2f)
            {
                Application.Quit();
            }
            else
            {
                backBtnTime = Time.time;
            }
        }
    }

   
}
