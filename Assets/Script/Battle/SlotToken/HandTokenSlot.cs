using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandTokenSlot : MonoBehaviour, SlotInterface, IDropHandler
{
    //토큰이 슬롯 위에서 onEndDrag되었을 경우
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<TokenDragDrop>().returnParent = this.transform;
        }
    }

    public void tokenAdded()
    {
        return;
    }
    public void tokenRemoved()
    {
        return;
    }
    public void tokenAfterRemoved()
    {
        return;
    }
}
