using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Reward_Supply : MonoBehaviour
{
    [SerializeField] private GameObject supplyImage;
    [SerializeField] private GameObject check;
    [SerializeField] private GameObject btn;

    //false�� ȹ��, true�� ���
    private bool situ = false;
    //����� �Ϸ� �ߴ°�?
    private bool obtained = false;
    //�� ������ �����ϴ°�?
    private bool trouble = false;

    [SerializeField] private ListOfItems supplyList;
    private Supply_Base presentSupply;

    public static event Action<string> SupplyObtainClicked;
    public static event Action SupplyCancelClicked;

    //�� �ڸ��� ���� �� ����ٸ�?
    public static event Action<string> SupplyObtainComplete;

    private void OnEnable()
    {
        CurtainsUp.CurtainHasBeenLifted += enableBtn;
        Equipment_ResultInventory.EquipObtainCompleteFromInventory += enableBtn;
        Reward_Equip.EquipObtainClicked += Reward_Equip_EquipObtainClicked;
        Reward_Equip.EquipCancelClicked += enableBtn;
        Supply_ResultInventory.SupplyReady += initPotenEquip;
        Supply_ResultInventory.SupplyObtainCompleteFromInventory += Supply_ResultInventory_SupplyObtainCompleteFromInventory;
    }

    private void OnDisable()
    {
        CurtainsUp.CurtainHasBeenLifted -= enableBtn;
        Equipment_ResultInventory.EquipObtainCompleteFromInventory -= enableBtn;
        Reward_Equip.EquipObtainClicked -= Reward_Equip_EquipObtainClicked;
        Reward_Equip.EquipCancelClicked -= enableBtn;
        Supply_ResultInventory.SupplyReady -= initPotenEquip;
        Supply_ResultInventory.SupplyObtainCompleteFromInventory -= Supply_ResultInventory_SupplyObtainCompleteFromInventory;

    }

    private void Supply_ResultInventory_SupplyObtainCompleteFromInventory()
    {
        this.btn.GetComponent<Button>().interactable = false;
        obtained = true;
        this.btn.GetComponentInChildren<TextMeshProUGUI>().SetText("ȹ�� �Ϸ�");
        this.check.SetActive(true);
    }

    private void Reward_Equip_EquipObtainClicked(string obj)
    {
        this.btn.GetComponent<Button>().interactable = false;
    }

    private void disableBtn()
    {
        this.btn.GetComponent<Button>().interactable = false;
    }
    private void enableBtn()
    {
        if(!obtained)
        {
            this.btn.GetComponent<Button>().interactable = true;
        }
    }

    private void Supply_ResultInventory_SupplyObtainComplete()
    {
        this.btn.GetComponent<Button>().interactable = false;
        obtained = true;
        this.btn.GetComponentInChildren<TextMeshProUGUI>().SetText("ȹ�� �Ϸ�");
        this.check.SetActive(true);
    }

    private void initPotenEquip(int b)
    {
        string realName;
        if(b == 5)
        {
            trouble = true;
        }

        realName = supplyList.items[(UnityEngine.Random.Range(0, supplyList.items.Count))];

        presentSupply = (Supply_Base)Activator.CreateInstance(Type.GetType(realName));

        showSupply();
    }

    private void showSupply()
    {
        supplyImage.GetComponent<TooltipTrigger>().enabled = true;
        supplyImage.GetComponent<TooltipTrigger>().header = presentSupply.supplyName;
        supplyImage.GetComponent<TooltipTrigger>().content = presentSupply.description;
        supplyImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Supply/" + presentSupply.realName);
    }

    public void RewardSupply_onClick()
    {
        //���� 5���� supply ������ ���� ������,
        if (!trouble)
        {
            this.btn.GetComponent<Button>().interactable = false;
            obtained = true;
            this.btn.GetComponentInChildren<TextMeshProUGUI>().SetText("ȹ�� �Ϸ�");
            SupplyObtainComplete?.Invoke(presentSupply.realName);
            this.check.SetActive(true);
        }
        else
        {
            //ȹ���� Ŭ���ߴٸ�
            if (!situ)
            {
                situ = true;
                //�̰ɷ� �ϴ� �ٸ� �ֵ��� �� ��װ�,
                SupplyObtainClicked?.Invoke(presentSupply.realName);
                this.btn.GetComponentInChildren<TextMeshProUGUI>().SetText("ȹ�� ����");
            }
            //��Ҹ� Ŭ���ߴٸ�
            else
            {
                situ = false;
                SupplyCancelClicked?.Invoke();
                this.btn.GetComponentInChildren<TextMeshProUGUI>().SetText("ȹ  ��");
            }
        }
    }
}
