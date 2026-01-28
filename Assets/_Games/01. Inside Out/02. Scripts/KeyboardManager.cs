using UnityEngine;

public class KeyboardManager : MonoBehaviour
{
    private PlayerNodeManager pnManager;
    [SerializeField] private GameoverUI gameOverUI;

    void Start()
    {
        pnManager = FindFirstObjectByType<PlayerNodeManager>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            pnManager.MoveLeftNode();

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            pnManager.MoveRightNode();

        if (gameOverUI.gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                gameOverUI.OnClickRestart();
            
            if (Input.GetKeyDown(KeyCode.Escape))
                gameOverUI.OnClickHome();
        }
    }
}
