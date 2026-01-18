using UnityEngine;

public class StagePopupManager : MonoBehaviour
{
    public static StagePopupManager Instance;

    [SerializeField] private GameObject popupObject; // 켜고 끌 파란 상자 (Popup_UI)
    [SerializeField] RectTransform popupStartPos; // 위치를 옮길 파란 상자의 Transform

    [SerializeField] private Vector3 offset = new Vector3(0, 50, 0); // 자물쇠보다 살짝 위에 뜨게
    void Awake()
    {
        Instance = this;
        Hide();
    }

    public void Show(Vector3 targetPos)
    {
        popupObject.SetActive(true);

        popupStartPos.position = targetPos + offset;
    }

    public void Hide()
    {
        popupObject.SetActive(false);
    }

}
