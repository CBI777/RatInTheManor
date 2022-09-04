using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Supply_ResultInventory : MonoBehaviour
{
    private List<Supply_Base> supply = new List<Supply_Base>();
    [SerializeField] private int supplyCount;

    [SerializeField] private SaveM_Result saveManager;
    [SerializeField] private GameObject[] supplyImg = new GameObject[5];
    [SerializeField] private GameObject[] bg = new GameObject[5];
    private GameObject[] btn = new GameObject[5];
    [SerializeField] private GameObject myText;

    private string tempSupply = "";

    public string[] getSupplyString()
    {
        string[] supplyString = new string[6];

        for (int i = 0; i < 6; i++)
        {
            if (i > supplyCount)
            {
                supplyString[i] = "NA";
            }
            else
            {
                supplyString[i] = this.supply[i].realName;
            }
        }

        return supplyString;
    }

    public static event Action SupplyObtainCompleteFromInventory;
    public static event Action SupplyUsedInventory;

    private void OnEnable()
    {
        CurtainsUp.CurtainHasBeenLifted += enableAll;
        Reward_Supply.SupplyCancelClicked += enableAll;
        Reward_Supply.SupplyObtainClicked += Reward_Supply_SupplyObtainClicked;
        Reward_Supply.SupplyObtainComplete += Reward_Supply_SupplyObtainComplete;
        Reward_Equip.EquipObtainClicked += disableAll;
        Reward_Equip.EquipCancelClicked += enableAll;
        Equipment_ResultInventory.EquipObtainCompleteFromInventory += disableAll;
        Reward_Hallucination.HalluObtainClicked += disableAll;
        Reward_Hallucination.HalluCancelClicked += enableAll;
        PackComplete.CompletePressed += disableAll;
    }

    private void OnDisable()
    {
        CurtainsUp.CurtainHasBeenLifted -= enableAll;
        Reward_Supply.SupplyCancelClicked -= enableAll;
        Reward_Supply.SupplyObtainClicked -= Reward_Supply_SupplyObtainClicked;
        Reward_Supply.SupplyObtainComplete -= Reward_Supply_SupplyObtainComplete;
        Reward_Equip.EquipObtainClicked -= disableAll;
        Reward_Equip.EquipCancelClicked -= enableAll;
        Equipment_ResultInventory.EquipObtainCompleteFromInventory -= disableAll;
        Reward_Hallucination.HalluObtainClicked -= disableAll;
        Reward_Hallucination.HalluCancelClicked -= enableAll;
        PackComplete.CompletePressed -= disableAll;
    }

    private void Reward_Supply_SupplyObtainComplete(string obj)
    {
        this.supply.Add((Supply_Base)Activator.CreateInstance(Type.GetType(obj)));
        SupplyChanged();
    }

    private void Reward_Supply_SupplyObtainClicked(string obj)
    {
        tempSupply = obj;
        for (int i = 1; i <= supplyCount; i++)
        {
            btn[i - 1].GetComponent<Button>().interactable = true;
            btn[i - 1].GetComponentInChildren<TextMeshProUGUI>().SetText("교  환");
        }
    }

    private void SupplyChanged()
    {
        this.supplyCount = (this.supply.Count - 1);
        if (supplyCount == 0) { myText.SetActive(true); }
        else { myText.SetActive(false); }

        int j = 0;
        for (int i = 1; i < 6; i++, j++)
        {
            if (i <= supplyCount)
            {
                bg[j].SetActive(true);
                btn[j].SetActive(true);
                supplyImg[j].GetComponent<TooltipTrigger>().enabled = true;
                supplyImg[j].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Supply/" + supply[i].realName);
                supplyImg[j].GetComponent<TooltipTrigger>().header = supply[i].supplyName;
                supplyImg[j].GetComponent<TooltipTrigger>().content = supply[i].description;
                if (supply[i].usage != 1)
                {
                    btn[j].GetComponent<Button>().interactable = true;
                    btn[j].GetComponentInChildren<TextMeshProUGUI>().SetText("사용하기");
                }
                else
                {
                    btn[j].GetComponent<Button>().interactable = false;
                    btn[j].GetComponentInChildren<TextMeshProUGUI>().SetText("사용 불가");
                }
            }
            else
            {
                bg[j].SetActive(false);
                btn[j].SetActive(false);
            }
        }
    }

    private void disableAll()
    {
        for (int i = 1; i <= supplyCount; i++)
        {
            btn[i-1].GetComponent<Button>().interactable = false;
        }
    }
    private void disableAll(string n)
    {
        for (int i = 1; i <= supplyCount; i++)
        {
            btn[i - 1].GetComponent<Button>().interactable = false;
        }
    }
    
    private void enableAll()
    {
        int j = 0;
        for (int i = 1; i <= supplyCount; i++, j++)
        {
            if(supply[i].usage != 1)
            {
                btn[j].GetComponent<Button>().interactable = true;
                btn[j].GetComponentInChildren<TextMeshProUGUI>().SetText("사용하기");
            }
            else
            {
                btn[j].GetComponent<Button>().interactable = false;
                btn[j].GetComponentInChildren<TextMeshProUGUI>().SetText("사용 불가");
            }
        }
    }

    private void UseSupply(int a)
    {
        if(supplyCount == 5) { SupplyUsedInventory?.Invoke();  }
        if (supply[a].usage != 1)//사실 usage가 1인데 발동이 될 가능성은 전무하지만 일단 오류처리
        {
            supply[a].onUse();
            //supply[a].onLose(); onLose가 사용되는게 있을까 싶긴 하다만
            supply.RemoveAt(a);
            SupplyChanged();
        }
    }

    private void SupplyObtain(int a)
    {
        this.supply.RemoveAt(a);
        //onLose?
        this.supply.Insert(a, (Supply_Base)Activator.CreateInstance(Type.GetType(tempSupply)));
        SupplyChanged();
        SupplyObtainCompleteFromInventory?.Invoke();
    }

    private void Awake()
    {
        this.btn[0] = GameObject.FindWithTag("SBtn1");
        this.btn[1] = GameObject.FindWithTag("SBtn2");
        this.btn[2] = GameObject.FindWithTag("SBtn3");
        this.btn[3] = GameObject.FindWithTag("SBtn4");
        this.btn[4] = GameObject.FindWithTag("SBtn5");

        string[] temp = this.saveManager.saving.supply;

        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i] != "NA")
            {
                this.supply.Add((Supply_Base)Activator.CreateInstance(Type.GetType(temp[i])));
            }
        }
    }

    private void Start()
    {
        SupplyChanged();
        disableAll();
    }
}
