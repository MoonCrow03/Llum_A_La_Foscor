using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Tutorial;
using UnityEngine;
using UnityEngine.Serialization;

public class TutorialTextWorld : MonoBehaviour
{ 
        [SerializeField] private TutorialTextData m_TutorialTextData;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private float textSpeed;
        [SerializeField] private GameObject FinishedTextImage;
        [SerializeField] private GameObject FasterTextImage;
        
        private float originalTextSpeed;
        private string fullText;
        private string currentText = "";
        private int index = 0;
        private int lastIndex = 0;
        private bool isTextFinished = false;
        
        private void Awake()
        {
            originalTextSpeed = textSpeed;
        }
    
        private void Start()
        {
            if (m_TutorialTextData != null)
            {
                fullText = m_TutorialTextData.tutorialText;
            }
        }

        private void Update()
        {
            if ((InputManager.Instance.Enter.Tap || InputManager.Instance.LeftClick.Tap) && isTextFinished)
            {
                if (IsAllTextDisplayed())
                {
                    GameEvents.TriggerEnableTutorialWorldUI(false);
                    GameEvents.TriggerEnablePlayerMovement(true);
                    enabled = false;
                }
                else
                {
                    textSpeed = originalTextSpeed;
                    GameEvents.TriggerPageFinished();
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

        private void OnEnable()
        {
            GameEvents.OnStartTutorialWorld += StartTutorialWorld;
        }
        
        private void OnDisable()
        {
            GameEvents.OnStartTutorialWorld -= StartTutorialWorld;
        }

        private void StartTutorialWorld()
        {
            StartCoroutine(ShowText());
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

