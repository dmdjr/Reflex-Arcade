using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace ColorTwin
{
    public class FallingCircle : MonoBehaviour
    {
        public static Action onSpriteMatch;
        public static Action onSpriteMismatch;

        public float fallSpeed = 500f;
        public float resetYPos = 1325.5f;
        public float appearanceRate = 0.3f; // chance to appear

        private RectTransform rectTransform;
        public RectTransform target; // BaseCircle's RectTransform
        private float judgeDistance = 250f;

        public Sprite[] circleSprites; // Array of possible circle sprites
       
        private Image image;

        public bool isMoving = false;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            image = GetComponent<Image>();
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
                if (isMoving)
                {
                    rectTransform.anchoredPosition += Vector2.down * fallSpeed * Time.deltaTime;
                }

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
            rectTransform.localPosition = new Vector2(0f, resetYPos);
            SetupFallingCircle();
        }

        void SetupFallingCircle()
        {
            // Randomly assign sprite based on appearance rate
            float randomValue = Random.Range(0f, 1f);

            if (randomValue <= appearanceRate && circleSprites.Length > 0)
            {
                int randomIndex = Random.Range(0, circleSprites.Length);
                image.sprite = circleSprites[randomIndex];
                image.color = new Color(1, 1, 1, 1); // Make visible
            }
            else
            {
                image.sprite = null;
                image.color = new Color(1, 1, 1, 0); // Make invisible
            }

            isMoving = false;
        }

        void CheckImageMatch()
        {
            if (image.sprite == target.GetComponent<Image>().sprite)
            {
                Debug.Log("Color Match!");
                onSpriteMatch?.Invoke();
            }
            else if (image.sprite != null)
            {
                Debug.Log("Color Mismatch!");
                onSpriteMismatch?.Invoke();
            }
            else
            {
                // No falling circle (not appeared), do nothing
            }
        }
    }
}