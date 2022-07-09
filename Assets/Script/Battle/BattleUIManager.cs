using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BattleUIManager : MonoBehaviour
{
    [SerializeField] private RectTransform Play_ValueArea, Play_EnemySlotArea; //StatusArea

    [SerializeField] private int valueMove = 144;
    [SerializeField] private int enemyMove = 144;
    [SerializeField] private float moveSpeed = 0.5f;

    private void OnEnable()
    {
        EquipmentTokenSlot.EquipSlotActivatedEvent += EquipmentTokenSlot_EquipSlotActivatedEvent;
        EquipmentTokenSlot.EquipSlotDeactivatedEvent += EquipmentTokenSlot_EquipSlotDeactivatedEvent;
        EquipmentChangeBtn.OnEquipChange += EquipmentChangeBtn_OnEquipChange;
    }

    private void OnDisable()
    {
        EquipmentTokenSlot.EquipSlotActivatedEvent -= EquipmentTokenSlot_EquipSlotActivatedEvent;
        EquipmentTokenSlot.EquipSlotDeactivatedEvent -= EquipmentTokenSlot_EquipSlotDeactivatedEvent;
        EquipmentChangeBtn.OnEquipChange -= EquipmentChangeBtn_OnEquipChange;
    }

    private void EquipmentChangeBtn_OnEquipChange(int obj)
    {
        Play_EnemySlotArea.DOAnchorPos(Vector2.zero, moveSpeed);
        Play_ValueArea.DOAnchorPos(Vector2.zero, moveSpeed);
    }

    private void EquipmentTokenSlot_EquipSlotActivatedEvent()
    {
        Play_EnemySlotArea.DOAnchorPos(new Vector2(0, enemyMove), moveSpeed);
        Play_ValueArea.DOAnchorPos(new Vector2(0, valueMove), moveSpeed);
    }

    private void EquipmentTokenSlot_EquipSlotDeactivatedEvent()
    {
        Play_EnemySlotArea.DOAnchorPos(Vector2.zero, moveSpeed);
        Play_ValueArea.DOAnchorPos(Vector2.zero, moveSpeed);
    }
}
