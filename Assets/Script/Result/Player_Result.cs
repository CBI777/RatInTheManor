using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player_Result : MonoBehaviour
{
    //�⺻������ ������ �ִ� resist
    [SerializeField] private int[] naturalResist = new int[4];

    //��ü resist
    [SerializeField] private int[] totalResist = new int[4];
    //equip���κ��� �������� resist
    [SerializeField] private int[] equipResist = new int[4];

    //�κ��丮 â�� ���� ����. ���� ���� + ��� ����
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
