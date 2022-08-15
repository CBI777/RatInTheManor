using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PackComplete : MonoBehaviour
{
    [SerializeField] private SaveM_Result saveManager;
    public static event Action CompletePressed;
    public static event Action SafeToGo;


    [SerializeField] private GameObject tooltip;

    private bool isComplete = false;
    private bool isSure = false;

    private void OnEnable()
    {
        Reward_Equip.EquipObtainClicked += disableBtn;
        Reward_Equip.EquipCancelClicked += enableBtn;
        Equipment_ResultInventory.EquipObtainCompleteFromInventory += enableBtn;
        Reward_Supply.SupplyCancelClicked += enableBtn;
        Reward_Supply.SupplyObtainClicked += disableBtn;
        Supply_ResultInventory.SupplyObtainCompleteFromInventory += enableBtn;
        Reward_Hallucination.HalluObtainClicked += disableBtn;
        Reward_Hallucination.HalluCancelClicked += enableBtn;
        SaveM_Result.finalSaveFinished += SaveM_Result_finalSaveFinished;
        SaveM_Result.middleSaveFinished += SaveM_Result_middleSaveFinished;
    }

    private void OnDisable()
    {
        Reward_Equip.EquipObtainClicked -= disableBtn;
        Reward_Equip.EquipCancelClicked -= enableBtn;
        Equipment_ResultInventory.EquipObtainCompleteFromInventory -= enableBtn;
        Reward_Supply.SupplyCancelClicked -= enableBtn;
        Reward_Supply.SupplyObtainClicked -= disableBtn;
        Supply_ResultInventory.SupplyObtainCompleteFromInventory -= enableBtn;
        Reward_Hallucination.HalluObtainClicked -= disableBtn;
        Reward_Hallucination.HalluCancelClicked -= enableBtn;
        SaveM_Result.finalSaveFinished -= SaveM_Result_finalSaveFinished;
        SaveM_Result.middleSaveFinished += SaveM_Result_middleSaveFinished;
    }


    private void SaveM_Result_middleSaveFinished()
    {
        isComplete = true;
        transform.GetComponent<Button>().interactable = true;
        tooltip.SetActive(false);
    }

    private void SaveM_Result_finalSaveFinished()
    {
        SafeToGo?.Invoke();
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

    private void Start()
    {
        if((!saveManager.saving.isBattle) && saveManager.saving.halluIsEarned)
        {
            SaveM_Result_middleSaveFinished();
        }
    }
}
