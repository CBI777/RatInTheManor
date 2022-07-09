using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    private List<Equipment> equipment = new List<Equipment>();

    [SerializeField] private int[] naturalResist = new int[4];
    [SerializeField] private int curEquip = 0;
    [SerializeField] private int tempEquip = 0;
    [SerializeField] private int equipCount;

    [SerializeField] private int[] totalResist = new int[4];

    public static event Action<int[]> ResistChangedEvent;
    public static event Action<int, int> CurEquipChanged;
    public static event Action<int, int, Equipment[]> EquipChangedEvent;

    private void OnEnable()
    {
        EquipmentChangeBtn.OnEquipChange += EquipmentChangeBtn_OnEquipChange;
        EquipmentChangeBtn.OnECBtnClick += EquipmentChangeBtn_OnECBtnClick;
        EquipmentTokenSlot.EquipSlotDeactivatedEvent += EquipmentTokenSlot_EquipSlotDeactivatedEvent;
    }

    private void OnDisable()
    {
        EquipmentChangeBtn.OnEquipChange -= EquipmentChangeBtn_OnEquipChange;
        EquipmentTokenSlot.EquipSlotDeactivatedEvent -= EquipmentTokenSlot_EquipSlotDeactivatedEvent;
    }

    private void EquipmentChangeBtn_OnECBtnClick(int obj)
    {
        tempEquip = obj;
        ResistChange(tempEquip);
    }

    private void EquipmentTokenSlot_EquipSlotDeactivatedEvent()
    {
        CurEquipChanged?.Invoke(curEquip, equipCount);
        ResistChange(curEquip);
    }

    private void EquipmentChangeBtn_OnEquipChange(int obj)
    {
        setCurEquip(obj);
    }

    public void ResistChange(int eq)
    {
        for(int i = 0; i < 4; i++)
        {
            totalResist[i] = (naturalResist[i] + equipment[eq].resChange[i]);
        }

        ResistChangedEvent?.Invoke(totalResist);
    }

    public void setCurEquip(int n)
    {
        this.curEquip = n;
        this.tempEquip = curEquip;
        CurEquipChanged?.Invoke(curEquip, equipCount);
        ResistChange(curEquip);
    }

    public void obtainEquipment(Equipment eq)
    {
        //TODO

        this.equipCount = this.equipment.Count;
        EquipChangedEvent?.Invoke(curEquip, equipCount, this.equipment.ToArray());
    }

    public void removeEquipment(int num)
    {
        //TODO and Action
    }

    private void Awake()
    {
        this.naturalResist[0] = 1;
        this.naturalResist[1] = 1;
        this.naturalResist[2] = 0;
        this.naturalResist[3] = -1;

        Equipment equip1 = new Equipment();
        equip1.equipName = "장비1";
        equip1.resChange[0] = 1;
        equip1.resChange[1] = -1;
        equip1.resChange[2] = 0;
        equip1.resChange[3] = 0;
        this.equipment.Add(equip1);

        Equipment equip2 = new Equipment();
        equip2.equipName = "장비2";
        equip2.resChange[0] = 0;
        equip2.resChange[1] = 1;
        equip2.resChange[2] = 0;
        equip2.resChange[3] = -1;
        this.equipment.Add(equip2);
        
        Equipment equip3 = new Equipment();
        equip3.equipName = "장비3";
        equip3.resChange[0] = 2;
        equip3.resChange[1] = -1;
        equip3.resChange[2] = -1;
        equip3.resChange[3] = 0;
        this.equipment.Add(equip3);

        this.equipCount = this.equipment.Count;
        this.curEquip = 1;
        this.tempEquip = curEquip;
        EquipChangedEvent?.Invoke(this.curEquip, this.equipCount, this.equipment.ToArray());
    }

    private void Start()
    {
        ResistChange(curEquip);
    }
}
