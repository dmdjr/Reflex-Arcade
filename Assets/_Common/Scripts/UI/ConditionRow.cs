using TMPro;
using UnityEngine;
using UnityEngine.UI;

/* ==================================================================================
 * [Class]: ConditionRow
 * [Role] : 팝업 내의 개별 조건 항목(아이콘 + 텍스트)을 표시하는 뷰 클래스
 * [Note] : StagePopupManager에 의해 생성(Instantiate)되고 데이터가 설정(SetData)됨
 * ================================================================================== */
public class ConditionRow : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI conditionText;

    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color clearColor;

    public void SetData(Sprite icon, int score, string gameName, bool isCleared)
    {
        iconImage.sprite = icon;
        conditionText.text = $"{gameName} : {score}";

        if (isCleared)
        {
            conditionText.color = clearColor;
        }
        else
        {
            conditionText.color = defaultColor;
        }
    }
}
