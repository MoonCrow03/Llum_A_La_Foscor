using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Wordle
{
    public class Tile : MonoBehaviour
    {
        [System.Serializable]
        public class TileStates
        {
            public Color FillColor;
            public Color OutlineColor;
        }
        
        public TileStates State { get; private set; }
        public char Letter { get; private set; }
        
        private TextMeshProUGUI text;
        private Image fillImage;
        private Outline outline;
        
        private void Awake()
        {
            text = GetComponentInChildren<TextMeshProUGUI>();
            fillImage = GetComponent<Image>();
            outline = GetComponent<Outline>();
        }
        
        public void SetLetter(char letter)
        {
            Letter = letter;
            text.text = letter.ToString();
        }
        
        public void SetTileState(TileStates tileStates)
        {
            this.State = tileStates;
            fillImage.color = tileStates.FillColor;
            outline.effectColor = tileStates.OutlineColor;
        }
    }
}
