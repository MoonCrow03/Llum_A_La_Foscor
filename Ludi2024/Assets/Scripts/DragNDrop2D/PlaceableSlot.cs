using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class PlaceableSlot : MonoBehaviour, IDropHandler
{
    public virtual void OnDrop(PointerEventData eventData)
    {
        GameObject l_droppedObject = eventData.pointerDrag;
        DragNDrop2D l_draggableObject = l_droppedObject.GetComponent<DragNDrop2D>();

        if (transform.childCount > 0)
        {
            // If the slot already has a child, swap the objects.
            Transform l_currentChild = transform.GetChild(0); // Get the current object in the slot
            PlaceableSlot oldSlot = l_draggableObject.GetCurrentSlot(); // Get the old slot of the dragged object

            // Move the object currently in the slot back to the original slot
            l_currentChild.SetParent(oldSlot.transform);
            l_currentChild.localPosition = Vector3.zero;

            // Set the current slot for the swapped object
            DragNDrop2D l_currentSlotObject = l_currentChild.GetComponent<DragNDrop2D>();
            l_currentSlotObject.SetCurrentSlot(oldSlot);
        }

        // Place the dragged object in the new slot
        l_draggableObject.SetParentAfterDrag(transform);
        l_draggableObject.SetCurrentSlot(this);
    }
}
