using System.Collections.Generic;
using UnityEngine;
using System;

public class EquipmentManager : MonoBehaviour
{
    private List<Equipment> equipment = new List<Equipment>();

    [SerializeField] private ListOfItems equipList;


    private int turnInitEquip = 0;
    private int curEquip = 0;
    private int tempEquip = 0;
    private int equipCount;

    //'����' �������� ��� ��ȭ���� ���
    public static event Action<int, int> CurEquipChanged;
    //��� ��ü�� ��ȭ�� ���� ���
    public static event Action<int, int, Equipment[]> EquipChangedEvent;
    //player���� �˷��ֱ� ����
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
        this.equipment.Add(Resources.Load<Equipment>("ScriptableObject/Equipment/" + realName));
        this.equipCount = this.equipment.Count;
    }

    private void initEquip(int[] b)
    {
        int temp = b.Length;
        for(int i = 0; i<temp; i++)
        {
            obtainEquipment(equipList.items[b[i]]);
        }
    }

    private void Start()
    {
        int[] a = { 3, 0, 1 };

        initEquip(a);
        this.turnInitEquip = 0;

        setCurEquip(0);
        EquipChangedEvent?.Invoke(curEquip, equipCount, this.equipment.ToArray());
    }

}
