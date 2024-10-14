using UnityEngine;
using UnityEngine.EventSystems;

namespace MakeWordsMinigame
{
    public class LetterFormDrag : DragNDrop2D
    {
        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
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
            Debug.Log("LetterFormDrag: OnEndDrag");
        }
    }
}
