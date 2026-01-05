using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private PlayerNodesManager pnManager;
    
    [SerializeField] private Button leftTouchZone;
    [SerializeField] private Button rightTouchZone;
    [SerializeField] private Button pauseButton;
    [SerializeField] private TextMeshProUGUI score;
    
    void Awake()
    {
        pnManager = FindFirstObjectByType<PlayerNodesManager>();
    }
    
    void Start()
    {
        leftTouchZone.onClick.AddListener(onLeftClick);
        rightTouchZone.onClick.AddListener(onRightClick);
    }

    private void onLeftClick()
    {
        pnManager.MoveLeftNode();
    }

    private void onRightClick()
    {
        pnManager.MoveRightNode();
    }
}
