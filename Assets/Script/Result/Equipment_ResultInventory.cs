using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Equipment_ResultInventory : MonoBehaviour
{
    [SerializeField] private GameObject[] bg = new GameObject[3];
    [SerializeField] private GameObject[] equips = new GameObject[3];
    [SerializeField] private GameObject[] equipcheck = new GameObject[3];
    [SerializeField] private GameObject[] btns = new GameObject[3];

    public int[] getEquipNum()
    {
        int[] equipNum = new int[3];

        for (int i = 0; i < 3; i++)
        {
            if (i > equipCount)
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

    public static event Action<int[]> equipmentChangedEvent;
    public static event Action<int[]> curEquipPass;
    public static event Action EquipObtainCompleteFromInventory;

    private List<Equipment> equipment = new List<Equipment>();
    [SerializeField] private ListOfItems equipList;
    private List<string> potentialEquipment = new List<string>();

    private int curEquip = 0;
    private int equipCount;

    private string tempEquip = "";

    private void OnEnable()
    {
        CurtainsUp.CurtainHasBeenLifted += enableAll;
        Reward_Equip.EquipObtainComplete += obtainEquipment;
        Reward_Equip.EquipObtainClicked += Reward_Equip_EquipObtainClicked;
        Reward_Equip.EquipCancelClicked += enableAll;
        Reward_Supply.SupplyCancelClicked += enableAll;
        Reward_Supply.SupplyObtainClicked += disableAll;
        Supply_ResultInventory.SupplyObtainCompleteFromInventory += enableAll;
        Reward_Hallucination.HalluObtainClicked += disableAll;
        Reward_Hallucination.HalluCancelClicked += enableAll;
        PackComplete.CompletePressed += disableAll;
    }

    private void OnDisable()
    {
        CurtainsUp.CurtainHasBeenLifted -= enableAll;
        Reward_Equip.EquipObtainComplete -= obtainEquipment;
        Reward_Equip.EquipObtainClicked -= Reward_Equip_EquipObtainClicked;
        Reward_Equip.EquipCancelClicked -= enableAll;
        Reward_Supply.SupplyCancelClicked -= enableAll;
        Reward_Supply.SupplyObtainClicked -= disableAll;
        Supply_ResultInventory.SupplyObtainCompleteFromInventory -= enableAll;
        Reward_Hallucination.HalluObtainClicked += disableAll;
        Reward_Hallucination.HalluCancelClicked += enableAll;
        PackComplete.CompletePressed -= disableAll;
    }

    private void disableAll()
    {
        for (int i = 0; i < equipCount; i++)
        {
            btns[i].GetComponent<Button>().interactable = false;
        }
    }
    private void disableAll(string obj)
    {
        for (int i = 0; i < equipCount; i++)
        {
            btns[i].GetComponent<Button>().interactable = false;
        }
    }

    private void Reward_Equip_EquipObtainClicked(string obj)
    {
        tempEquip = obj;
        for (int i = 0; i < equipCount; i++)
        {
            btns[i].GetComponent<Button>().interactable = true;
            btns[i].GetComponentInChildren<TextMeshProUGUI>().SetText("±³  È¯");
        }
    }

    private void CurEquipChange(int a)
    {
        this.curEquip = a;
        for (int i = 0; i < equipCount; i++)
        {
            if (i == curEquip)
            {
                btns[i].GetComponent<Button>().interactable = false;
                btns[i].GetComponentInChildren<TextMeshProUGUI>().SetText("Àå Âø Áß");
                equipcheck[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/UI/SelectedEdge");
            }
            else
            {
                btns[i].GetComponent<Button>().interactable = true;
                btns[i].GetComponentInChildren<TextMeshProUGUI>().SetText("Àå  Âø");
                equipcheck[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/UI/DeselectedEdge");
            }
        }
        equipmentChangedEvent?.Invoke(equipment[curEquip].resChange);
    }

    private void EquipmentObtain(int a)
    {
        this.equipment.RemoveAt(a);
        this.equipment.Insert(a, Resources.Load<Equipment>("ScriptableObject/Equipment/" + tempEquip));
        
        this.equipCount = this.equipment.Count;

        EquipObtainCompleteFromInventory?.Invoke();
        EquipChanged();
        equipmentChangedEvent?.Invoke(equipment[curEquip].resChange);
    }

    private void EquipChanged()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i < equipCount)
            {
                bg[i].SetActive(true);
                equips[i].GetComponent<TooltipTrigger>().enabled = true;
                equips[i].GetComponent<TooltipTrigger>().header = equipment[i].equipName;
                equips[i].GetComponent<TooltipTrigger>().content = equipment[i].description;
                equips[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Equipment/" + equipment[i].realName);

                if (i == curEquip)
                {
                    btns[i].GetComponent<Button>().interactable = false;
                    btns[i].GetComponentInChildren<TextMeshProUGUI>().SetText("Àå Âø Áß");
                    equipcheck[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/UI/SelectedEdge");
                }
                else
                {
                    btns[i].GetComponent<Button>().interactable = true;
                    btns[i].GetComponentInChildren<TextMeshProUGUI>().SetText("Àå  Âø");
                    equipcheck[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/UI/DeselectedEdge");
                }
                btns[i].SetActive(true);
            }
            else
            {
                bg[i].SetActive(false);
                btns[i].SetActive(false);
            }
        }
    }

    private void enableAll()
    {
        CurEquipChange(curEquip);
    }

    private void Reward_EquipmentClicked()
    {
        for (int i = 0; i < equipCount; i++)
        {
            btns[i].GetComponentInChildren<TextMeshProUGUI>().SetText("±³  È¯");
        }
    }

    public void obtainEquipment(string realName)
    {
        this.equipment.Add(Resources.Load<Equipment>("ScriptableObject/Equipment/" + realName));
        this.equipCount = this.equipment.Count;
        EquipChanged();
    }

    private void initEquip(int[] b)
    {
        int temp = b.Length;
        for (int i = 0; i < temp; i++)
        {
            obtainEquipment(potentialEquipment[b[i]]);
        }
    }

    private void Start()
    {
        for (int i = 0; i < equipList.items.Count; i++)
        {
            this.potentialEquipment.Add(equipList.items[i]);
        }

        int[] a = { 3, 0};
        initEquip(a);

        Array.Sort(a);
        curEquipPass?.Invoke(a);
        curEquip = 0;
        CurEquipChange(curEquip);
        EquipChanged();
        disableAll();
    }
}
