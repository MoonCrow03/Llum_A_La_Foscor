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
            if (InputManager.Instance.Enter.Tap && isTextFinished)
            {
                if (IsAllTextDisplayed())
                {
                    gameObject.SetActive(false);
                    OnTutorialFinished?.Invoke();
                }
                else
                {
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
            return index >= fullText.Split(' ').Length;
        }

        IEnumerator ShowText()
        {
            string[] words = fullText.Split(' ');
            FinishedTextImage.SetActive(false);
            for (index = lastIndex; index < fullText.Split(' ').Length; index++)
            {
                currentText += words[index] + " ";
                text.text = currentText;
                text.ForceMeshUpdate();

                if (text.preferredHeight > text.rectTransform.rect.height) 
                {
                    FinishedTextImage.SetActive(true);
                    isTextFinished = true;
                    lastIndex = index + 1;
                    yield break;
                }

                yield return new WaitForSeconds(textSpeed);
            }
            FinishedTextImage.SetActive(true);
            isTextFinished = true;
        }
    }
}
