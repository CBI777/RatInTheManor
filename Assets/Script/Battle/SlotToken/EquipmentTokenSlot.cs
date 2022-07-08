using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentTokenSlot : MonoBehaviour, IDropHandler
{
    //[SerializeField] private bool slotEnabled;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null/*&& slotEnabled*/)
        {
            Debug.Log("On Drop" + eventData.pointerDrag.name);
            eventData.pointerDrag.GetComponent<TokenDragDrop>().returnParent = this.transform;
        }
    }
}
