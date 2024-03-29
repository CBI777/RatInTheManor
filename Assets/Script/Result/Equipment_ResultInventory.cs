using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Equipment_ResultInventory : MonoBehaviour
{
    [SerializeField] private SaveM_Result saveManager;

    [SerializeField] private GameObject[] bg = new GameObject[3];
    [SerializeField] private GameObject[] equips = new GameObject[3];
    [SerializeField] private GameObject[] equipcheck = new GameObject[3];
    private GameObject[] btns = new GameObject[3];

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

    public static event Action<int[]> equipmentChangedEvent;
    public static event Action EquipObtainCompleteFromInventory;

    private List<Equipment> equipment = new List<Equipment>();
    [SerializeField] private ListOfItems equipList;

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
        Reward_Hallucination.HalluObtainClicked -= disableAll;
        Reward_Hallucination.HalluCancelClicked -= enableAll;
        PackComplete.CompletePressed -= disableAll;
    }

    private void disableAll()
    {
        for (int i = 0; i < equipCount; i++)
        {
            if (btns[i].gameObject != null)
            {
                btns[i].GetComponent<Button>().interactable = false;
            }
        }
    }
    private void disableAll(string obj)
    {
        for (int i = 0; i < equipCount; i++)
        {
            if (btns[i].gameObject != null)
            {
                btns[i].GetComponent<Button>().interactable = false;
            }
        }
    }

    private void Reward_Equip_EquipObtainClicked(string obj)
    {
        tempEquip = obj;
        for (int i = 0; i < equipCount; i++)
        {
            if (btns[i].gameObject != null)
            {
                btns[i].GetComponent<Button>().interactable = true;
                btns[i].GetComponentInChildren<TextMeshProUGUI>().SetText("��  ȯ");
            }
        }
    }

    private void CurEquipChange(int a)
    {
        this.curEquip = a;
        for (int i = 0; i < equipCount; i++)
        {
            if (i == curEquip)
            {
                if (btns[i].gameObject != null)
                {
                    btns[i].GetComponent<Button>().interactable = false;
                }
                btns[i].GetComponentInChildren<TextMeshProUGUI>().SetText("�� �� ��");
                equipcheck[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/UI/SelectedEdge");
            }
            else
            {
                if (btns[i].gameObject != null)
                {
                    btns[i].GetComponent<Button>().interactable = true;
                }
                btns[i].GetComponentInChildren<TextMeshProUGUI>().SetText("��  ��");
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
                btns[i].SetActive(true);
                bg[i].SetActive(true);
                equips[i].GetComponent<TooltipTrigger>().enabled = true;
                equips[i].GetComponent<TooltipTrigger>().header = equipment[i].equipName;
                equips[i].GetComponent<TooltipTrigger>().content = equipment[i].description;
                equips[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Equipment/" + equipment[i].realName);

                if (i == curEquip)
                {
                    if (btns[i].gameObject != null)
                    {
                        btns[i].GetComponent<Button>().interactable = false;
                        btns[i].GetComponentInChildren<TextMeshProUGUI>().SetText("�� �� ��");
                    }
                    equipcheck[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/UI/SelectedEdge");
                }
                else
                {
                    if (btns[i].gameObject != null)
                    {
                        btns[i].GetComponent<Button>().interactable = true;
                        btns[i].GetComponentInChildren<TextMeshProUGUI>().SetText("��  ��");
                    }
                    equipcheck[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/UI/DeselectedEdge");
                }
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
            btns[i].GetComponentInChildren<TextMeshProUGUI>().SetText("��  ȯ");
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
            if (b[i] != -1)
            {
                obtainEquipment(equipList.items[b[i]]);
            }
        }
    }

    private void Awake()
    {
        this.btns[0] = GameObject.FindWithTag("EBtn1");
        this.btns[1] = GameObject.FindWithTag("EBtn2");
        this.btns[2] = GameObject.FindWithTag("EBtn3");
        this.curEquip = this.saveManager.saving.curEquip;
        initEquip(this.saveManager.saving.equip);
    }

    private void Start()
    {
        CurEquipChange(curEquip);
        EquipChanged();
        disableAll();
    }
}
