using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Tutorial
{
    public class TutorialText : MonoBehaviour
    {
        public TutorialTextData tutorialTextData;
        public TextMeshProUGUI text;
        public float textSpeed;
        public GameObject FinishedTextImage;

        private string fullText;
        private string currentText = "";
        private int index = 0;
        private int lastIndex = 0;
        private bool isTextFinished = false;
        
        public static Action OnTutorialFinished;
        public static Action OnPageFinished;
    
        void Start()
        {
            if (tutorialTextData != null)
            {
                fullText = tutorialTextData.tutorialText;
                StartCoroutine(ShowText());
            }
        }

        private void Update()
        {
            if ((InputManager.Instance.Enter.Tap || InputManager.Instance.LeftClick.Tap) && isTextFinished)
            {
                if (IsAllTextDisplayed())
                {
                    OnTutorialFinished?.Invoke();
                }
                else
                {
                    OnPageFinished?.Invoke();
                    isTextFinished = false;
                    currentText = "";
                    text.text = currentText;
                    StartCoroutine(ShowText());
                }
            }
        }

        private bool IsAllTextDisplayed()
        {
            StopCoroutine(ShowText());
            return index >= fullText.Length;
        }

        IEnumerator ShowText()
        {
            FinishedTextImage.SetActive(false);
            text.text = ""; 

            for (index = lastIndex; index < fullText.Length; index++)
            {
                string simulatedText = currentText + fullText[index];

                text.text = simulatedText;
                text.ForceMeshUpdate(); 

                if (IsTextExceedingBounds())
                {
                    FinishedTextImage.SetActive(true);
                    isTextFinished = true;
                    lastIndex = index; 
                    yield break;
                }

                currentText = simulatedText;
                text.text = currentText;

                yield return new WaitForSeconds(textSpeed);
            }

            FinishedTextImage.SetActive(true);
            isTextFinished = true;
        }

        private bool IsTextExceedingBounds()
        {
            float preferredHeight = text.preferredHeight;

            return preferredHeight > text.rectTransform.rect.height;
        }
    }
}
