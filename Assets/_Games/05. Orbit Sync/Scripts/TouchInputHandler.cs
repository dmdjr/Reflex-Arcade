using UnityEngine;

public class TouchInputHandler : MonoBehaviour
{
    public bool IsLeftPressed => CheckInput(true);
    public bool IsRightPressed => CheckInput(false);

    private bool CheckInput(bool isLeftTarget)
    {
#if UNITY_EDITOR
        if (isLeftTarget && Input.GetKey(KeyCode.LeftArrow)) return true;
        if (!isLeftTarget && Input.GetKey(KeyCode.RightArrow)) return true;
#endif

        if (Input.GetMouseButton(0))
        {
            if (isLeftTarget && Input.mousePosition.x < Screen.width / 2) return true;
            if (!isLeftTarget && Input.mousePosition.x >= Screen.width / 2) return true;
        }

        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                {
                    if (isLeftTarget && touch.position.x < Screen.width / 2) return true;
                    if (!isLeftTarget && touch.position.x >= Screen.width / 2) return true;
                }
            }
        }
        return false;
    }
}