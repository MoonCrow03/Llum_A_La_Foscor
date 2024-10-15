using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "AlphabetData", menuName = "WordSearch/AlphabetData", order = 1)]
public class AlphabetData : ScriptableObject
{
    [System.Serializable]
    public class LetterData
    {
        public string m_Letter;
        public Sprite m_Image;
    }

    public List<LetterData> m_AlphabetPlain = new List<LetterData>();
    public List<LetterData> m_AlphabetNormal = new List<LetterData>();
    public List<LetterData> m_AlphabetHighlighted = new List<LetterData>();
    public List<LetterData> m_AlphabetCorrect = new List<LetterData>();
}
