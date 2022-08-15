using System.Collections.Generic;
using UnityEngine;
using System;

public class EquipmentManager : MonoBehaviour
{
    private List<Equipment> equipment = new List<Equipment>();

    [SerializeField] private SaveM_Battle saveManager;

    [SerializeField] private ListOfItems equipList;

    private int turnInitEquip = 0;
    private int curEquip = 0;
    private int tempEquip = 0;
    private int equipCount;

    public int[] getEquipNum()
    {
        int[] equipNum = new int[3];

        for (int i = 0; i < 3; i++)
        {
            if (i >= equipCount)
            {
                equipNum[i] = -1;
            }
            else
            {
                equipNum[i] = equipment[i].index;
            }
        }

        return equipNum;
    }
    public int getCurEquip() { return curEquip; }

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
        TurnManager.TurnStart += TurnManager_TurnStart;
    }

    private void OnDisable()
    {
        EquipmentChangeBtn.OnEquipChange -= EquipmentChangeBtn_OnEquipChange;
        EquipmentChangeBtn.OnECBtnClick -= EquipmentChangeBtn_OnECBtnClick;
        EquipmentTokenSlot.EquipSlotDeactivatedEvent -= EquipmentTokenSlot_EquipSlotDeactivatedEvent;
        BattleResetManager.ResetBoardEvent -= BattleResetManager_ResetBoardEvent;
    }

    private void TurnManager_TurnStart(int obj)
    {
        turnInitEquip = curEquip;
    }

    private void BattleResetManager_ResetBoardEvent()
    {
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
            if (b[i] != -1)
            {
                obtainEquipment(equipList.items[b[i]]);
            }
        }
    }

    private void Awake()
    {
        int[] a = this.saveManager.saving.equip;
        this.curEquip = this.saveManager.saving.curEquip;

        initEquip(a);
        this.turnInitEquip = 0;
    }
    private void Start()
    {
        setCurEquip(curEquip);
        this.turnInitEquip = curEquip;
        EquipChangedEvent?.Invoke(curEquip, equipCount, this.equipment.ToArray());
    }

}
