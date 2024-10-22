using UnityEngine;
using UnityEngine.UI;

namespace Wordle
{
    public class EnterTile : MonoBehaviour
    {
        private string enterChar = "Enter";

        public string EnterChar
        {
            get => enterChar;
            set
            {
                enterChar = value;
                GetComponentInChildren<TMPro.TextMeshProUGUI>().text = enterChar.ToString();
            }
        }
        
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnEnterClicked);
        }

        private void OnEnterClicked()
        {
            Board.Instance.OnEnterClicked();
        }
    }
}