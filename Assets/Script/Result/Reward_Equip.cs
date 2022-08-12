using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Reward_Equip : MonoBehaviour
{
    [SerializeField] private GameObject equipImage;
    [SerializeField] private GameObject check;
    [SerializeField] private GameObject btn;

    //false�� ȹ��, true�� ���
    private bool situ = false;
    //����� �Ϸ� �ߴ°�?
    private bool obtained = false;
    //�� ������ �����ϴ°�?
    private bool trouble = false;

    [SerializeField] private ListOfItems equipList;
    private List<string> potentialEquipment = new List<string>();
    private Equipment presentEquip;

    public static event Action<string> EquipObtainClicked;
    public static event Action EquipCancelClicked;

    //�� �ڸ��� ���� �� ����ٸ�?
    public static event Action<string> EquipObtainComplete;

    private void OnEnable()
    {
        Equipment_ResultInventory.curEquipPass += initPotenEquip;
        Equipment_ResultInventory.EquipObtainCompleteFromInventory += Equipment_ResultInventory_EquipmentObtainComplete;
        CurtainsUp.CurtainHasBeenLifted += enableBtn;
        Reward_Supply.SupplyCancelClicked += enableBtn;
        Reward_Supply.SupplyObtainClicked += Reward_Supply_SupplyObtainClicked;
        Supply_ResultInventory.SupplyObtainCompleteFromInventory += enableBtn;
    }
    private void OnDisable()
    {
        Equipment_ResultInventory.curEquipPass -= initPotenEquip;
        Equipment_ResultInventory.EquipObtainCompleteFromInventory -= Equipment_ResultInventory_EquipmentObtainComplete;
        CurtainsUp.CurtainHasBeenLifted -= enableBtn;
        Reward_Supply.SupplyCancelClicked -= enableBtn;
        Reward_Supply.SupplyObtainClicked -= Reward_Supply_SupplyObtainClicked;
        Supply_ResultInventory.SupplyObtainCompleteFromInventory -= enableBtn;
    }

    private void Reward_Supply_SupplyObtainClicked(string obj)
    {
        disableBtn();
    }

    private void enableBtn()
    {
        if (!obtained)
        {
            this.btn.GetComponent<Button>().interactable = true;
        }
    }
    private void disableBtn()
    {
        this.btn.GetComponent<Button>().interactable = false;
    }

    private void Equipment_ResultInventory_EquipmentObtainComplete()
    {
        this.btn.GetComponent<Button>().interactable = false;
        obtained = true;
        this.btn.GetComponentInChildren<TextMeshProUGUI>().SetText("ȹ�� �Ϸ�");
        this.check.SetActive(true);
    }

    private void initPotenEquip(int[] b)
    {
        string realName;

        int temp = b.Length;
        if (temp == 3)
        {
            trouble = true;
        }
        for(int i = 0; i<equipList.items.Count; i++)
        {
            potentialEquipment.Add(equipList.items[i]);
        }
        for (int i = (temp - 1); i >= 0; i--)
        {
            potentialEquipment.RemoveAt(b[i]);
        }

        realName = potentialEquipment[(UnityEngine.Random.Range(0, potentialEquipment.Count))];

        presentEquip = Resources.Load<Equipment>("ScriptableObject/Equipment/" + realName);

        presentEquipment();
    }

    private void presentEquipment()
    {
        equipImage.GetComponent<TooltipTrigger>().enabled = true;
        equipImage.GetComponent<TooltipTrigger>().header = presentEquip.equipName;
        equipImage.GetComponent<TooltipTrigger>().content = presentEquip.description;
        equipImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Equipment/" + presentEquip.realName);
    }

    public void RewardEquip_onClick()
    {
        //���� 3���� equip�� ������ ���� ������,
        if(!trouble)
        {
            this.btn.GetComponent<Button>().interactable = false;
            obtained = true;
            this.btn.GetComponentInChildren<TextMeshProUGUI>().SetText("ȹ�� �Ϸ�");
            EquipObtainComplete?.Invoke(presentEquip.realName);
            this.check.SetActive(true);
        }
        else
        {
            //ȹ���� Ŭ���ߴٸ�
            if (!situ)
            {
                situ = true;
                //�̰ɷ� �ϴ� �ٸ� �ֵ��� �� ��װ�,
                EquipObtainClicked?.Invoke(presentEquip.realName);
                this.btn.GetComponentInChildren<TextMeshProUGUI>().SetText("ȹ�� ����");
            }
            //��Ҹ� Ŭ���ߴٸ�
            else
            {
                situ = false;
                EquipCancelClicked?.Invoke();
                this.btn.GetComponentInChildren<TextMeshProUGUI>().SetText("ȹ  ��");
            }
        }
        
    }
}
