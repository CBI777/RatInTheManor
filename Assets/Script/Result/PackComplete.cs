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
    [SerializeField] private Button btn;

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
        SaveM_Result.middleSaveFinished -= SaveM_Result_middleSaveFinished;
    }


    private void SaveM_Result_middleSaveFinished()
    {
        isComplete = true;
        btn.interactable = true;
        tooltip.GetComponent<TooltipTrigger>().enabled = false;
    }

    private void SaveM_Result_finalSaveFinished()
    {
        SafeToGo?.Invoke();
    }

    private void disableBtn()
    {
        isSure = false;
        transform.GetComponentInChildren<TextMeshProUGUI>().SetText("����\u000a�Ϸ�");
        btn.interactable = false;
    }
    private void disableBtn(string n)
    {
        isSure = false;
        transform.GetComponentInChildren<TextMeshProUGUI>().SetText("����\u000a�Ϸ�");
        btn.interactable = false;
    }
    private void enableBtn()
    {
        if(isComplete)
        {
            btn.interactable = true;
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
            transform.GetComponentInChildren<TextMeshProUGUI>().SetText("����\u000a������");
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
