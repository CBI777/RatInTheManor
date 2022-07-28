using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EquipmentManager : MonoBehaviour
{
    private List<Equipment> equipment = new List<Equipment>();

    [SerializeField] private int turnInitEquip = 0;
    [SerializeField] private int curEquip = 0;
    [SerializeField] private int tempEquip = 0;
    [SerializeField] private int equipCount;

    //'현재' 장착중인 장비가 변화했을 경우
    public static event Action<int, int> CurEquipChanged;
    //장비 자체에 변화가 있을 경우
    public static event Action<int, int, Equipment[]> EquipChangedEvent;
    //player에게 알려주기 위함
    public static event Action<int[]> EquipResistChanged;

    private void OnEnable()
    {
        EquipmentChangeBtn.OnEquipChange += EquipmentChangeBtn_OnEquipChange;
        EquipmentChangeBtn.OnECBtnClick += EquipmentChangeBtn_OnECBtnClick;
        EquipmentTokenSlot.EquipSlotDeactivatedEvent += EquipmentTokenSlot_EquipSlotDeactivatedEvent;
        BattleResetManager.ResetBoardEvent += BattleResetManager_ResetBoardEvent;
        InventoryEquipmentChangeBtn.curEquipChanged += InventoryEquipmentChangeBtn_curEquipChanged;
    }

    private void OnDisable()
    {
        EquipmentChangeBtn.OnEquipChange -= EquipmentChangeBtn_OnEquipChange;
        EquipmentChangeBtn.OnECBtnClick -= EquipmentChangeBtn_OnECBtnClick;
        EquipmentTokenSlot.EquipSlotDeactivatedEvent -= EquipmentTokenSlot_EquipSlotDeactivatedEvent;
        BattleResetManager.ResetBoardEvent -= BattleResetManager_ResetBoardEvent;
        InventoryEquipmentChangeBtn.curEquipChanged -= InventoryEquipmentChangeBtn_curEquipChanged;
    }

    private void InventoryEquipmentChangeBtn_curEquipChanged(int obj)
    {
        setCurEquip(obj);
    }

    private void BattleResetManager_ResetBoardEvent()
    {
        CurEquipChanged?.Invoke(curEquip, equipCount);
        setCurEquip(turnInitEquip);
    }

    private void EquipmentTokenSlot_EquipSlotDeactivatedEvent()
    {
        CurEquipChanged?.Invoke(curEquip, equipCount);
        EquipResistChanged?.Invoke(this.equipment[curEquip].resChange);
    }

    private void EquipmentChangeBtn_OnECBtnClick(int obj)
    {
        tempEquip = obj;
        EquipResistChanged?.Invoke(this.equipment[tempEquip].resChange);
    }

    private void EquipmentChangeBtn_OnEquipChange(int obj)
    {
        setCurEquip(obj);
    }

   
    public void setCurEquip(int n)
    {
        this.curEquip = n;
        this.tempEquip = curEquip;
        CurEquipChanged?.Invoke(curEquip, equipCount);
        EquipResistChanged?.Invoke(this.equipment[curEquip].resChange);
    }

    public void obtainEquipment(string realName)
    {
        //TODO
        this.equipment.Add(Resources.Load<Equipment>("ScriptableObject/Equipment/" + realName));
        this.equipCount = this.equipment.Count;
        EquipChangedEvent?.Invoke(curEquip, equipCount, this.equipment.ToArray());
    }

    public void removeEquipment(int num)
    {
        //TODO and Action
    }

    private void Start()
    {
        this.equipment.Add(Resources.Load<Equipment>("ScriptableObject/Equipment/Equipment_Pistol"));
        this.equipment.Add(Resources.Load<Equipment>("ScriptableObject/Equipment/Equipment_Blindfold"));
        this.equipment.Add(Resources.Load<Equipment>("ScriptableObject/Equipment/Equipment_EyeballJar"));
        this.equipCount = this.equipment.Count;
        this.turnInitEquip = 0;
        setCurEquip(0);
        EquipChangedEvent?.Invoke(this.curEquip, this.equipCount, this.equipment.ToArray());
    }

}
