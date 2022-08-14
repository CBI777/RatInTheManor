using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player_Result : MonoBehaviour
{
    //기본적으로 가지고 있는 resist
    private int[] naturalResist = new int[4];

    //전체 resist
    private int[] totalResist = new int[4];
    //equip으로부터 가져오는 resist
    private int[] equipResist = new int[4];
    //quirk로부터 가져오는 resist
    private int[] quirkResist = new int[4];

    public int[] getResist() { return naturalResist; }

    [SerializeField] private GameObject[] invenResist = new GameObject[4];

    //인벤토리 창에 나올 저항. 원래 저항 + 장비 저항
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
                "기본 저항 " + naturalResist[i] + "\u000a장비로 인한 저항 변화 " + equipResist[i] + "\u000a의지/나약으로 인한 저항 변화 " + quirkResist[i];
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
