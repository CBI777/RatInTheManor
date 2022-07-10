using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private int[] naturalResist = new int[4];
    [SerializeField] private int sanity;
    [SerializeField] private int madness;
    [SerializeField] private int obsession;

    [SerializeField] private int[] totalResist = new int[4];
    [SerializeField] private int[] equipResist = new int[4];
    [SerializeField] private int[] supplyResist = new int[4];

    public static event Action<int[]> ResistChangedEvent;

    private void OnEnable()
    {
        Supply_Base.SupplySanityChange += Supply_Base_SupplySanityChange;
        Supply_Base.SupplyMadnessChange += Supply_Base_SupplyMadnessChange;
        Supply_Base.SupplyObsessionChange += Supply_Base_SupplyObsessionChange;
        Supply_Base.SupplyResistChangeEter += NaturalResistChange;
        Supply_Base.SupplyResistChangeTemp += SupplyResistChange;
        EquipmentManager.EquipResistChanged += EquipResistChange;
    }

    private void OnDisable()
    {
        Supply_Base.SupplySanityChange -= Supply_Base_SupplySanityChange;
        Supply_Base.SupplyMadnessChange -= Supply_Base_SupplyMadnessChange;
        Supply_Base.SupplyObsessionChange -= Supply_Base_SupplyObsessionChange;
        Supply_Base.SupplyResistChangeEter -= NaturalResistChange;
        Supply_Base.SupplyResistChangeTemp -= SupplyResistChange;
        EquipmentManager.EquipResistChanged -= EquipResistChange;
    }

    private void Supply_Base_SupplyObsessionChange(int obj)
    {
        Debug.Log("¡˝¬¯¿Ã πŸ≤Ò §µ§°");
    }
    private void Supply_Base_SupplyMadnessChange(int obj)
    {
        Debug.Log("±§±‚∞° πŸ≤Ò §µ§°");
    }
    private void Supply_Base_SupplySanityChange(int obj)
    {
        Debug.Log("¿Ãº∫¿Ã πŸ≤Ò §µ§°");
    }


    public void SupplyResistChange(int[] n)
    {
        Debug.Log("¿˙«◊¿Ã ¿”Ω√¿˚¿∏∑Œ πŸ≤Ò §µ§°");
        for (int i = 0; i < n.Length; i++)
        {
            this.supplyResist[i] += n[i];
        }
        RecalcResist();
    }
    public void NaturalResistChange(int[] n)
    {
        Debug.Log("¿˙«◊¿Ã øµø¯»˜ πŸ≤Ò §µ§°");
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
            totalResist[i] = (naturalResist[i] + equipResist[i] + supplyResist[i]);
        }

        ResistChangedEvent?.Invoke(totalResist);
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
