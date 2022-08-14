using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player_Result : MonoBehaviour
{
    //�⺻������ ������ �ִ� resist
    private int[] naturalResist = new int[4];

    //��ü resist
    private int[] totalResist = new int[4];
    //equip���κ��� �������� resist
    private int[] equipResist = new int[4];
    //quirk�κ��� �������� resist
    private int[] quirkResist = new int[4];

    public int[] getResist() { return naturalResist; }

    [SerializeField] private GameObject[] invenResist = new GameObject[4];

    //�κ��丮 â�� ���� ����. ���� ���� + ��� ����
    public static event Action<int[]> InventoryResistChangedEvent;

    private void OnEnable()
    {
        Equipment_ResultInventory.equipmentChangedEvent += EquipResistChange;
        Supply_Base.SupplyResistChangeEter += NaturalResistChange;
        Quirk_Base.QuirkResistChangeEvent += QuirkResistChange;
        Hallucination_Base.HalluResistChange += NaturalResistChange;
    }

    private void OnDisable()
    {
        Equipment_ResultInventory.equipmentChangedEvent -= EquipResistChange;
        Supply_Base.SupplyResistChangeEter -= NaturalResistChange;
        Quirk_Base.QuirkResistChangeEvent -= QuirkResistChange;
        Hallucination_Base.HalluResistChange -= NaturalResistChange;
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
    public void QuirkResistChange(int[] n)
    {
        for (int i = 0; i < n.Length; i++)
        {
            this.quirkResist[i] = n[i];
        }
        RecalcResist();
    }

    public void RecalcResist()
    {
        for (int i = 0; i < 4; i++)
        {
            totalResist[i] = (naturalResist[i] + equipResist[i] + quirkResist[i]);
            invenResist[i].GetComponent<TooltipTrigger>().content =
                "�⺻ ���� " + naturalResist[i] + "\u000a���� ���� ���� ��ȭ " + equipResist[i] + "\u000a����/�������� ���� ���� ��ȭ " + quirkResist[i];
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
