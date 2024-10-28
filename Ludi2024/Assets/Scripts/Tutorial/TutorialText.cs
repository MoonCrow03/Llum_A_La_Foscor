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
        private float originalTextSpeed;
        public GameObject FinishedTextImage;
        public Scenes scene;
        public GameObject FasterTextImage;

        private string fullText;
        private string currentText = "";
        private int index = 0;
        private int lastIndex = 0;
        private bool isTextFinished = false;
        
        public static Action OnTutorialFinished;
        public static Action OnPageFinished;

        private void Awake()
        {
            originalTextSpeed = textSpeed;
            if (GameManager.TutorialsShown.ContainsKey(scene))
            {
                gameObject.SetActive(false);
                OnTutorialFinished?.Invoke();
            }
        }

        private void Start()
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
                    textSpeed = originalTextSpeed;
                    OnPageFinished?.Invoke();
                    isTextFinished = false;
                    currentText = "";
                    text.text = currentText;
                    StartCoroutine(ShowText());
                }
            }

            if (InputManager.Instance.SpaceBar.Hold)
            {
                textSpeed = 0;
            }
            else
            {
                textSpeed = originalTextSpeed;
            }
        }

        private bool IsAllTextDisplayed()
        {
            StopCoroutine(ShowText());
            return index >= fullText.Length;
        }

        private IEnumerator ShowText()
        {
            FinishedTextImage.SetActive(false);
            FasterTextImage.SetActive(true);
            text.text = ""; 

            for (index = lastIndex; index < fullText.Length; index++)
            {
                string simulatedText = currentText + fullText[index];

                text.text = simulatedText;
                text.ForceMeshUpdate(); 

                if (IsTextExceedingBounds())
                {
                    FinishedTextImage.SetActive(true);
                    FasterTextImage.SetActive(false);
                    isTextFinished = true;
                    lastIndex = index; 
                    yield break;
                }

                currentText = simulatedText;
                text.text = currentText;

                yield return new WaitForSeconds(textSpeed);
            }
            
            FasterTextImage.SetActive(false);
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
