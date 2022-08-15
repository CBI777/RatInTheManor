using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class EquipmentTokenSlot : MonoBehaviour, SlotInterface, IDropHandler
{
    public static event Action EquipSlotActivatedEvent;
    public static event Action EquipSlotDeactivatedEvent;

    [SerializeField] private bool slotEnabled;

    private void OnEnable()
    {
        EquipmentChangeBtn.OnEquipChange += EquipmentChangeBtn_OnEquipChange;
        TurnManager.TurnStart += TurnManager_TurnStart;
    }

    private void OnDisable()
    {
        EquipmentChangeBtn.OnEquipChange -= EquipmentChangeBtn_OnEquipChange;
        TurnManager.TurnStart -= TurnManager_TurnStart;
    }

    private void TurnManager_TurnStart(int obj)
    {
        slotEnabled = true;
    }

    private void EquipmentChangeBtn_OnEquipChange(int obj)
    {
        this.transform.GetComponentInChildren<TokenDragDrop>().Undragable();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && slotEnabled)
        {
            eventData.pointerDrag.GetComponent<TokenDragDrop>().returnParent = this.transform;
        }
    }

    public void tokenAdded()
    {
        slotEnabled = false;
        EquipSlotActivatedEvent?.Invoke();
    }
    public void tokenRemoved()
    {
        //��ū�� ����, ���� ���������� �ʾ���
        slotEnabled = true;
    }
    public void tokenAfterRemoved()
    {
        //��ū�� ����, �ٸ����ٰ� ��������.
        EquipSlotDeactivatedEvent?.Invoke();
    }
}
