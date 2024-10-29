using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class SlotContainer2D : MonoBehaviour, IDropHandler
{
    public virtual void OnDrop(PointerEventData eventData)
    {
        //TODO: Arreglar esto, pillar la slot y comprobar si esta bloqueada. Si esta bloqueada la casilla solucion, return.
        
        GameObject l_droppedObject = eventData.pointerDrag;
        DragNDrop2D l_draggableObject = l_droppedObject.GetComponent<DragNDrop2D>();

        // Check if the dropped object is locked, if locked do nothing
        if (l_draggableObject == null || l_draggableObject.IsLocked())
        {
            Debug.Log("Item is locked, cannot swap or move.");
            return;
        }

        if (transform.childCount > 0)
        {
            // If the slotContainer2D already has a child, swap the objects.
            Transform l_currentChild = transform.GetChild(0); // Get the current object in the slotContainer2D
            SlotContainer2D oldSlotContainer2D = l_draggableObject.GetCurrentSlot(); // Get the old slotContainer2D of the dragged object

            // Move the object currently in the slotContainer2D back to the original slotContainer2D
            l_currentChild.SetParent(oldSlotContainer2D.transform);
            l_currentChild.localPosition = Vector3.zero;

            // Set the current slotContainer2D for the swapped object
            DragNDrop2D l_currentSlotObject = l_currentChild.GetComponent<DragNDrop2D>();
            l_currentSlotObject.SetCurrentSlot(oldSlotContainer2D);
        }

        // Place the dragged object in the new slotContainer2D
        l_draggableObject.SetParentAfterDrag(transform);
        l_draggableObject.SetCurrentSlot(this);
    }
}
