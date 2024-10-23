using UnityEngine;

namespace Tutorial
{
    [CreateAssetMenu(fileName = "TutorialTextData", menuName = "Tutorial/TutorialTextData")]
    public class TutorialTextData : ScriptableObject
    {
        [TextArea]
        public string tutorialText;
    }
}