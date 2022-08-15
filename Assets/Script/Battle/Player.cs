using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    //기본적으로 가지고 있는 resist
    private int[] naturalResist = new int[4];

    //전체 resist
    private int[] totalResist = new int[4];
    //equip으로부터 가져오는 resist
    private int[] equipResist = new int[4];
    //supply로부터 가져오는 resist
    private int[] supplyResist = new int[4];
    //quirk로부터 가져오는 resist
    private int[] quirkResist = new int[4];

    [SerializeField] private SaveM_Battle saveManager;
    [SerializeField] private GameObject[] invenResist = new GameObject[4];

    public int[] getResist() { return naturalResist; }

    public static event Action<int[]> ResistChangedEvent;
    //인벤토리 창에 나올 저항. 원래 저항 + 장비 저항
    public static event Action<int[]> InventoryResistChangedEvent;

    private void OnEnable()
    {
        Supply_Base.SupplyResistChangeEter += NaturalResistChange;
        Supply_Base.SupplyResistChangeTemp += SupplyResistChange;
        EquipmentManager.EquipResistChanged += EquipResistChange;
        Quirk_Base.QuirkResistChangeEvent += QuirkResistChange;
    }

    private void OnDisable()
    {
        Supply_Base.SupplyResistChangeEter -= NaturalResistChange;
        Supply_Base.SupplyResistChangeTemp -= SupplyResistChange;
        EquipmentManager.EquipResistChanged -= EquipResistChange;
        Quirk_Base.QuirkResistChangeEvent -= QuirkResistChange;
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
    public void QuirkResistChange(int[] n)
    {
        for (int i = 0; i < n.Length; i++)
        {
            this.quirkResist[i] += n[i];
        }
        RecalcResist();
        RecalcDisplayResist();
    }

    public void RecalcResist()
    {
        for (int i = 0; i < 4; i++)
        {
            totalResist[i] = (naturalResist[i] + equipResist[i] + supplyResist[i] + quirkResist[i]);
        }

        ResistChangedEvent?.Invoke(totalResist);
    }

    public void RecalcDisplayResist()
    {
        int[] temp = new int[4];
        for (int i = 0; i < 4; i++)
        {
            temp[i] = (naturalResist[i] + equipResist[i] + quirkResist[i]);
            invenResist[i].GetComponent<TooltipTrigger>().content =
                "기본 저항 " + naturalResist[i] + "\u000a장비로 인한 저항 변화 " + equipResist[i] + "\u000a의지/나약으로 인한 저항 변화 " + quirkResist[i];
        }

        InventoryResistChangedEvent?.Invoke(temp);
    }

    private void Awake()
    {
        int[] temp = this.saveManager.saving.resist;
        for(int i = 0; i < temp.Length; i++)
        {
            this.naturalResist[i] = temp[i];
        }
    }

    private void Start()
    {
        RecalcResist();
        RecalcDisplayResist();
    }
}
