using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Reward_Supply : MonoBehaviour
{
    [SerializeField] private GameObject supplyImage;
    [SerializeField] private GameObject check;
    [SerializeField] private GameObject btn;

    //false면 획득, true면 취소
    private bool situ = false;
    //취득을 완료 했는가?
    private bool obtained = false;
    //빈 공간이 존재하는가?
    private bool trouble = false;

    [SerializeField] private ListOfItems supplyList;
    private Supply_Base presentSupply;

    public string getEarnSupply()
    {
        return this.presentSupply.realName;
    }
    public bool getObtained()
    {
        return obtained;
    }

    public static event Action<string> SupplyObtainClicked;
    public static event Action SupplyCancelClicked;

    //빈 자리가 있을 때 얻었다면?
    public static event Action<string> SupplyObtainComplete;

    private void OnEnable()
    {
        CurtainsUp.CurtainHasBeenLifted += enableBtn;
        Equipment_ResultInventory.EquipObtainCompleteFromInventory += enableBtn;
        Reward_Equip.EquipObtainClicked += disableBtn;
        Reward_Equip.EquipCancelClicked += enableBtn;
        Supply_ResultInventory.SupplyReady += initPotenEquip;
        Supply_ResultInventory.SupplyObtainCompleteFromInventory += Supply_ResultInventory_SupplyObtainCompleteFromInventory;
        Supply_ResultInventory.SupplyUsedInventory += Supply_ResultInventory_SupplyUsedInventory;
        Reward_Hallucination.HalluObtainClicked += disableBtn;
        Reward_Hallucination.HalluCancelClicked += enableBtn;
        PackComplete.CompletePressed += disableBtn;
    }

    private void OnDisable()
    {
        CurtainsUp.CurtainHasBeenLifted -= enableBtn;
        Equipment_ResultInventory.EquipObtainCompleteFromInventory -= enableBtn;
        Reward_Equip.EquipObtainClicked -= disableBtn;
        Reward_Equip.EquipCancelClicked -= enableBtn;
        Supply_ResultInventory.SupplyReady -= initPotenEquip;
        Supply_ResultInventory.SupplyObtainCompleteFromInventory -= Supply_ResultInventory_SupplyObtainCompleteFromInventory;
        Supply_ResultInventory.SupplyUsedInventory -= Supply_ResultInventory_SupplyUsedInventory;
        Reward_Hallucination.HalluObtainClicked -= disableBtn;
        Reward_Hallucination.HalluCancelClicked -= enableBtn;
        PackComplete.CompletePressed -= disableBtn;
    }

    private void Supply_ResultInventory_SupplyUsedInventory()
    {
        this.trouble = false;
    }

    private void Supply_ResultInventory_SupplyObtainCompleteFromInventory()
    {
        this.btn.GetComponent<Button>().interactable = false;
        obtained = true;
        this.btn.GetComponentInChildren<TextMeshProUGUI>().SetText("획득 완료");
        this.check.SetActive(true);
    }

    private void disableBtn()
    {
        this.btn.GetComponent<Button>().interactable = false;
    }
    private void disableBtn(string obj)
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
        //만약 5개의 supply 가지고 있지 않으면,
        if (!trouble)
        {
            this.btn.GetComponent<Button>().interactable = false;
            obtained = true;
            this.btn.GetComponentInChildren<TextMeshProUGUI>().SetText("획득 완료");
            SupplyObtainComplete?.Invoke(presentSupply.realName);
            this.check.SetActive(true);
        }
        else
        {
            //획득을 클릭했다면
            if (!situ)
            {
                situ = true;
                //이걸로 일단 다른 애들을 다 잠그고,
                SupplyObtainClicked?.Invoke(presentSupply.realName);
                this.btn.GetComponent<Image>().color = new Color32(165, 255, 235, 255);
                this.btn.GetComponentInChildren<TextMeshProUGUI>().SetText("획득 보류");
            }
            //취소를 클릭했다면
            else
            {
                situ = false;
                SupplyCancelClicked?.Invoke();
                this.btn.GetComponent<Image>().color = Color.white;
                this.btn.GetComponentInChildren<TextMeshProUGUI>().SetText("획  득");
            }
        }
    }
}
