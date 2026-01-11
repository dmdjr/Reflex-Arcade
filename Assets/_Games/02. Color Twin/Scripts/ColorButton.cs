using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ColorTwin
{
    public class ColorButton : MonoBehaviour
    {
        public RectTransform baseCircle;

        public Button redButton;
        public Button greenButton;
        public Button blueButton;

        public Sprite redSprite;
        public Sprite greenSprite;
        public Sprite blueSprite;

        private void Start()
        {
            redButton.onClick.AddListener(() => OnColorButtonClicked(redSprite));
            greenButton.onClick.AddListener(() => OnColorButtonClicked(greenSprite));
            blueButton.onClick.AddListener(() => OnColorButtonClicked(blueSprite));
        }

        private void OnColorButtonClicked(Sprite sprite)
        {
            if (!ColorTwinGameManager.Instance.IsGameRunning) return;
            baseCircle.gameObject.GetComponent<Image>().sprite = sprite;
        }
    }
}