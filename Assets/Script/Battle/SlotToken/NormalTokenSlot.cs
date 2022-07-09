using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class NormalTokenSlot : MonoBehaviour, SlotInterface, IDropHandler
{
    [SerializeField] private DmgType dmgType;
    private int tokenCount = 0;

    public static event Action<DmgType, int> PlayerSlotChangedEvent;

    private void Awake()
    {
        this.tokenCount = this.transform.childCount;
    }

    public void slotCountArrange()
    {
        this.tokenCount = this.transform.childCount;
        PlayerSlotChangedEvent?.Invoke(this.dmgType, this.tokenCount);
    }

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
        slotCountArrange();
    }
    public void tokenRemoved()
    {
        slotCountArrange();
    }
    public void tokenAfterRemoved()
    {
        return;
    }
}
