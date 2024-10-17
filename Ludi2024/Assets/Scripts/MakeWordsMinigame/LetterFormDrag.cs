using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MakeWordsMinigame
{
    public class LetterFormDrag : DragNDrop2D
    {
        public Image image;
        public TextMeshProUGUI text;
        
        
        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.SetCanvas();
            base.OnBeginDrag(eventData);
            image.raycastTarget = false;
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
            image.raycastTarget = true;
            text.raycastTarget = true;
            Debug.Log("LetterFormDrag: OnEndDrag");
        }
    }
}
