using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class PackComplete : MonoBehaviour
{
    public static event Action CompletePressed;
    [SerializeField] private GameObject tooltip;

    private bool isComplete = false;
    private bool isSure = false;

    private void OnEnable()
    {
        Reward_Hallucination.HallusPickComplete += Reward_Hallucination_HallusPickComplete;
        Reward_Equip.EquipObtainClicked += disableBtn;
        Reward_Equip.EquipCancelClicked += enableBtn;
        Equipment_ResultInventory.EquipObtainCompleteFromInventory += enableBtn;
        Reward_Supply.SupplyCancelClicked += enableBtn;
        Reward_Supply.SupplyObtainClicked += disableBtn;
        Supply_ResultInventory.SupplyObtainCompleteFromInventory += enableBtn;
        Reward_Hallucination.HalluObtainClicked += disableBtn;
        Reward_Hallucination.HalluCancelClicked += enableBtn;
    }

    private void OnDisable()
    {
        Reward_Hallucination.HallusPickComplete -= Reward_Hallucination_HallusPickComplete;
        Reward_Equip.EquipObtainClicked -= disableBtn;
        Reward_Equip.EquipCancelClicked -= enableBtn;
        Equipment_ResultInventory.EquipObtainCompleteFromInventory -= enableBtn;
        Reward_Supply.SupplyCancelClicked -= enableBtn;
        Reward_Supply.SupplyObtainClicked -= disableBtn;
        Supply_ResultInventory.SupplyObtainCompleteFromInventory -= enableBtn;
        Reward_Hallucination.HalluObtainClicked -= disableBtn;
        Reward_Hallucination.HalluCancelClicked -= enableBtn;
    }

    private void disableBtn()
    {
        isSure = false;
        transform.GetComponentInChildren<TextMeshProUGUI>().SetText("정비\u000a완료");
        transform.GetComponent<Button>().interactable = false;
    }
    private void disableBtn(string n)
    {
        isSure = false;
        transform.GetComponentInChildren<TextMeshProUGUI>().SetText("정비\u000a완료");
        transform.GetComponent<Button>().interactable = false;
    }
    private void enableBtn()
    {
        if(isComplete)
        {
            transform.GetComponent<Button>().interactable = true;
        }
    }

    private void Reward_Hallucination_HallusPickComplete()
    {
        isComplete = true;
        transform.GetComponent<Button>().interactable = true;
        tooltip.SetActive(false);
    }



    public void onClick()
    {
        if(isSure)
        {
            CompletePressed?.Invoke();
        }
        else
        {
            isSure = true;
            transform.GetComponentInChildren<TextMeshProUGUI>().SetText("저택\u000a안으로");
        }
    }
}
