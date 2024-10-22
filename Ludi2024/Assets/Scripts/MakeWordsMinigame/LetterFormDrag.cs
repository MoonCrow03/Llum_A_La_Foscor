using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MakeWordsMinigame
{
    public class LetterFormDrag : DragNDrop2D
    {
        public TextMeshProUGUI text;
        
        
        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.SetCanvas();
            base.OnBeginDrag(eventData);
            text.raycastTarget = false;
            Debug.Log("LetterFormDrag: OnBeginDrag");
        }
        
        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            Debug.Log("LetterFormDrag: OnDrag");
        }
        
        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            text.raycastTarget = true;
            Debug.Log("LetterFormDrag: OnEndDrag");
        }
    }
}
