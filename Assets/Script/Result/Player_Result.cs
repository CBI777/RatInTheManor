using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player_Result : MonoBehaviour
{
    //기본적으로 가지고 있는 resist
    [SerializeField] private int[] naturalResist = new int[4];

    //전체 resist
    [SerializeField] private int[] totalResist = new int[4];
    //equip으로부터 가져오는 resist
    [SerializeField] private int[] equipResist = new int[4];

    //인벤토리 창에 나올 저항. 원래 저항 + 장비 저항
    public static event Action<int[]> InventoryResistChangedEvent;

    private void OnEnable()
    {
        Equipment_ResultInventory.equipmentChangedEvent += EquipResistChange;
        Supply_Base.SupplyResistChangeEter += NaturalResistChange;
    }
    private void OnDisable()
    {
        Equipment_ResultInventory.equipmentChangedEvent -= EquipResistChange;
        Supply_Base.SupplyResistChangeEter -= NaturalResistChange;
    }

    public void NaturalResistChange(int[] n)
    {
        for (int i = 0; i < n.Length; i++)
        {
            this.naturalResist[i] += n[i];
        }
        RecalcResist();
    }
    public void EquipResistChange(int[] n)
    {
        for (int i = 0; i < n.Length; i++)
        {
            this.equipResist[i] = n[i];
        }
        RecalcResist();
    }

    public void RecalcResist()
    {
        for (int i = 0; i < 4; i++)
        {
            totalResist[i] = (naturalResist[i] + equipResist[i]);
        }

        InventoryResistChangedEvent?.Invoke(totalResist);
    }

    private void Awake()
    {
        this.naturalResist[0] = 1;
        this.naturalResist[1] = 1;
        this.naturalResist[2] = 0;
        this.naturalResist[3] = -1;
    }

    private void Start()
    {
        RecalcResist();
    }
}
