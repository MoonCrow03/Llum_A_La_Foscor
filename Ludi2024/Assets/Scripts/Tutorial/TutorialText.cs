using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Tutorial;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    public TutorialTextData tutorialTextData;
    public TextMeshProUGUI text;
    public float textSpeed;

    private string fullText;
    private string currentText = "";
    private int index = 0;
    private int lastIndex = 0;
    private bool isTextFinished = false;
    
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
        return index >= fullText.Split(' ').Length;
    }

    IEnumerator ShowText()
    {
        string[] words = fullText.Split(' ');
        for (index = lastIndex; index < fullText.Split(' ').Length; index++)
        {
            currentText += words[index] + " ";
            text.text = currentText;
            text.ForceMeshUpdate();

            if (text.preferredHeight > text.rectTransform.rect.height) // Check if the text is overflowing
            {
                isTextFinished = true;
                lastIndex = index + 1; // Save the last position
                yield break;
            }

            yield return new WaitForSeconds(textSpeed);
        }
        isTextFinished = true;
    }
}
