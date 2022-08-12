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

    //false면 획득, true면 취소
    private bool situ = false;
    //취득을 완료 했는가?
    private bool obtained = false;
    //빈 공간이 존재하는가?
    private bool trouble = false;

    [SerializeField] private ListOfItems equipList;
    private List<string> potentialEquipment = new List<string>();
    private Equipment presentEquip;

    public static event Action<string> EquipObtainClicked;
    public static event Action EquipCancelClicked;

    //빈 자리가 있을 때 얻었다면?
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
        this.btn.GetComponentInChildren<TextMeshProUGUI>().SetText("획득 완료");
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
        //만약 3개의 equip을 가지고 있지 않으면,
        if(!trouble)
        {
            this.btn.GetComponent<Button>().interactable = false;
            obtained = true;
            this.btn.GetComponentInChildren<TextMeshProUGUI>().SetText("획득 완료");
            EquipObtainComplete?.Invoke(presentEquip.realName);
            this.check.SetActive(true);
        }
        else
        {
            //획득을 클릭했다면
            if (!situ)
            {
                situ = true;
                //이걸로 일단 다른 애들을 다 잠그고,
                EquipObtainClicked?.Invoke(presentEquip.realName);
                this.btn.GetComponentInChildren<TextMeshProUGUI>().SetText("획득 보류");
            }
            //취소를 클릭했다면
            else
            {
                situ = false;
                EquipCancelClicked?.Invoke();
                this.btn.GetComponentInChildren<TextMeshProUGUI>().SetText("획  득");
            }
        }
        
    }
}
