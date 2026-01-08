using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ColorTwin
{   
    public class FallingCircle : MonoBehaviour
    {
        public float radius = 125f;
  
        public float fallSpeed = 300f;

        private RectTransform rectTransform;
        public RectTransform target; // BaseCircle's RectTransform
        private float judgeDistance = 250f;

        public Sprite[] circleSprites; // Array of possible circle sprites
        public float appearanceRate = 0.3f; // chance to appear

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            StartCoroutine(CheckDistanceAndMove());
            SetupFallingCircle();
        }

        IEnumerator CheckDistanceAndMove()
        {
            while (true)
            {
                // 1. Falling movement
                rectTransform.anchoredPosition += Vector2.down * fallSpeed * Time.deltaTime;

                // 2. Distance calculation
                float yDistance = Mathf.Abs(rectTransform.localPosition.y - target.localPosition.y);

                // 3. Check distance and judge
                if (yDistance <= judgeDistance)
                {
                    CheckImageMatch();
                    ResetPosition();
                }

                yield return null;
            }
        }

        void ResetPosition()
        {
            rectTransform.localPosition = new Vector2(0f, 1325.5f);
            SetupFallingCircle();
        }

        void SetupFallingCircle()
        {
            // Randomly assign sprite based on appearance rate
            float randomValue = Random.Range(0f, 1f);
            if (randomValue <= appearanceRate && circleSprites.Length > 0)
            {
                int randomIndex = Random.Range(0, circleSprites.Length);
                GetComponent<Image>().sprite = circleSprites[randomIndex];
            }
            else
            {
                GetComponent<Image>().sprite = null;
            }
        }

        void CheckImageMatch()
        {
            if (gameObject.GetComponent<Image>().sprite == target.GetComponent<Image>().sprite)
            {
                Debug.Log("Color Match!");
                // Add score or other logic here
            }
            else
            {
                Debug.Log("Color Mismatch! or No Color!");
                // Add penalty or other logic here
            }
        }
    }
}    