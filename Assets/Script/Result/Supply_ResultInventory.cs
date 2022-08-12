using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Supply_ResultInventory : MonoBehaviour
{
    private List<Supply_Base> supply = new List<Supply_Base>();
    [SerializeField] private int supplyCount;


    [SerializeField] private GameObject[] supplyImg = new GameObject[5];
    [SerializeField] private GameObject[] bg = new GameObject[5];
    [SerializeField] private GameObject[] btn = new GameObject[5];
    [SerializeField] private GameObject myText;

    private string tempSupply = "";

    public static event Action<int> SupplyReady;
    public static event Action SupplyObtainCompleteFromInventory;

    private void OnEnable()
    {
        CurtainsUp.CurtainHasBeenLifted += enableAll;
        Reward_Supply.SupplyCancelClicked += enableAll;
        Reward_Supply.SupplyObtainClicked += Reward_Supply_SupplyObtainClicked;
        Reward_Supply.SupplyObtainComplete += Reward_Supply_SupplyObtainComplete;
        Reward_Equip.EquipObtainClicked += Reward_Equip_EquipObtainClicked;
        Reward_Equip.EquipCancelClicked += enableAll;
        Equipment_ResultInventory.EquipObtainCompleteFromInventory += disableAll;
    }

    private void OnDisable()
    {
        CurtainsUp.CurtainHasBeenLifted -= enableAll;
        Reward_Supply.SupplyCancelClicked -= enableAll;
        Reward_Supply.SupplyObtainClicked -= Reward_Supply_SupplyObtainClicked;
        Reward_Supply.SupplyObtainComplete -= Reward_Supply_SupplyObtainComplete;
        Reward_Equip.EquipObtainClicked -= Reward_Equip_EquipObtainClicked;
        Reward_Equip.EquipCancelClicked -= enableAll;
        Equipment_ResultInventory.EquipObtainCompleteFromInventory -= disableAll;
    }


    private void Reward_Equip_EquipObtainClicked(string obj)
    {
        disableAll();
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

    private void Start()
    {
        //소모품은 반드시 NoUse 하나 이상을 가지고 있어야한다.
        this.supply.Add((Supply_Base)Activator.CreateInstance(Type.GetType("Supply_NoUse")));
        this.supply.Add((Supply_Base)Activator.CreateInstance(Type.GetType("Supply_Opium")));
        this.supply.Add((Supply_Base)Activator.CreateInstance(Type.GetType("Supply_Painkiller")));
        this.supply.Add((Supply_Base)Activator.CreateInstance(Type.GetType("Supply_Abhorrpainting")));
        this.supply.Add((Supply_Base)Activator.CreateInstance(Type.GetType("Supply_Opium")));
        this.supply.Add((Supply_Base)Activator.CreateInstance(Type.GetType("Supply_Opium")));
        SupplyChanged();
        disableAll();

        SupplyReady?.Invoke(supplyCount);
    }
}
