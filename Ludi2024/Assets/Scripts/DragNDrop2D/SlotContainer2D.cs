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
            // Get the current child object in the slot
            Transform l_currentChild = transform.GetChild(0);
            DragNDrop2D l_currentSlotObject = l_currentChild.GetComponent<DragNDrop2D>();

            // Check if the object in the slot is locked
            if (l_currentSlotObject != null && l_currentSlotObject.IsLocked())
            {
                Debug.Log("The slot contains a locked object, cannot swap.");
                // Return the dragged object to its previous slot
                l_draggableObject.SetParentAfterDrag(l_draggableObject.GetCurrentSlot().transform);
                return;
            }

            // If the current object is not locked, proceed with the swap
            SlotContainer2D oldSlotContainer2D = l_draggableObject.GetCurrentSlot();

            // Move the current object in the slot back to its original slot
            l_currentChild.SetParent(oldSlotContainer2D.transform);
            l_currentChild.localPosition = Vector3.zero;

            // Set the current slot for the swapped object
            l_currentSlotObject.SetCurrentSlot(oldSlotContainer2D);
        }

        // Place the dragged object in the new slotContainer2D
        l_draggableObject.SetParentAfterDrag(transform);
        l_draggableObject.SetCurrentSlot(this);
    }
}
