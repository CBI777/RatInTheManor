using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandTokenSlot : MonoBehaviour, IDropHandler
{
    //토큰이 슬롯 위에서 onEndDrag되었을 경우
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Debug.Log("On Drop" + eventData.pointerDrag.name);
            eventData.pointerDrag.GetComponent<TokenDragDrop>().returnParent = this.transform;
        }
    }
}
