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

    private void OnEnable()
    {
        BattleDialogueProvider.betweenTurnDia += BattleDialogueProvider_betweenTurnDia;
    }
    private void OnDisable()
    {
        BattleDialogueProvider.betweenTurnDia -= BattleDialogueProvider_betweenTurnDia;
    }

    private void BattleDialogueProvider_betweenTurnDia()
    {
        this.tokenCount = 0;
        PlayerSlotChangedEvent?.Invoke(this.dmgType, this.tokenCount);
    }

    private void Awake()
    {
        this.tokenCount = this.transform.childCount;
    }

    public void slotCountArrange()
    {
        this.tokenCount = this.transform.childCount;
        PlayerSlotChangedEvent?.Invoke(this.dmgType, this.tokenCount);
    }

    //��ū�� ���� ������ onEndDrag�Ǿ��� ���
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
