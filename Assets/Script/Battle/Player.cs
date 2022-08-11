using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    //기본적으로 가지고 있는 resist
    [SerializeField] private int[] naturalResist = new int[4];

    //전체 resist
    [SerializeField] private int[] totalResist = new int[4];
    //equip으로부터 가져오는 resist
    [SerializeField] private int[] equipResist = new int[4];
    //supply로부터 가져오는 resist
    [SerializeField] private int[] supplyResist = new int[4];

    public static event Action<int[]> ResistChangedEvent;
    //인벤토리 창에 나올 저항. 원래 저항 + 장비 저항
    public static event Action<int[]> InventoryResistChangedEvent;

    private void OnEnable()
    {
        Supply_Base.SupplyResistChangeEter += NaturalResistChange;
        Supply_Base.SupplyResistChangeTemp += SupplyResistChange;
        EquipmentManager.EquipResistChanged += EquipResistChange;
        Quirk_Base.QuirkResistChangeEvent += NaturalResistChange;
    }

    private void OnDisable()
    {
        Supply_Base.SupplyResistChangeEter -= NaturalResistChange;
        Supply_Base.SupplyResistChangeTemp -= SupplyResistChange;
        EquipmentManager.EquipResistChanged -= EquipResistChange;
        Quirk_Base.QuirkResistChangeEvent -= NaturalResistChange;
    }

    public void SupplyResistChange(int[] n)
    {
        for (int i = 0; i < n.Length; i++)
        {
            this.supplyResist[i] += n[i];
        }
        RecalcResist();
    }
    public void NaturalResistChange(int[] n)
    {
        for (int i = 0; i < n.Length; i++)
        {
            this.naturalResist[i] += n[i];
        }
        RecalcResist();
        RecalcDisplayResist();
    }
    public void EquipResistChange(int[] n)
    {
        for (int i = 0; i < n.Length; i++)
        {
            this.equipResist[i] = n[i];
        }
        RecalcResist();
        RecalcDisplayResist();
    }

    public void RecalcResist()
    {
        for (int i = 0; i < 4; i++)
        {
            totalResist[i] = (naturalResist[i] + equipResist[i] + supplyResist[i]);
        }

        ResistChangedEvent?.Invoke(totalResist);
    }

    public void RecalcDisplayResist()
    {
        int[] temp = new int[4];
        for (int i = 0; i < 4; i++)
        {
            temp[i] = (naturalResist[i] + equipResist[i]);
        }

        InventoryResistChangedEvent?.Invoke(temp);
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
        RecalcDisplayResist();
    }
}
