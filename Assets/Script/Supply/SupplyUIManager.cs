using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SupplyUIManager : MonoBehaviour
{
    [SerializeField] private GameObject img;
    [SerializeField] private GameObject prevBtn;
    [SerializeField] private GameObject nextBtn;

    private void OnEnable()
    {
        SupplyManager.CurSupplyChanged += SupplyManager_CurSupplyChanged;
        TurnEndBtn.TurnEndEvent += TurnEndBtn_TurnEndEvent;
    }

    private void OnDisable()
    {
        SupplyManager.CurSupplyChanged -= SupplyManager_CurSupplyChanged;
        TurnEndBtn.TurnEndEvent -= TurnEndBtn_TurnEndEvent;
    }

    private void TurnEndBtn_TurnEndEvent()
    {
        this.nextBtn.SetActive(false);
        this.prevBtn.SetActive(false);
    }

    private void SupplyManager_CurSupplyChanged(int supplyLocation, string realName, string supplyName, string batDescription)
    {
        if (supplyLocation > 0) { this.prevBtn.SetActive(true); }
        else { this.prevBtn.SetActive(false); }

        if(supplyLocation > -1 && supplyLocation < 2) { this.nextBtn.SetActive(true); }
        else { this.nextBtn.SetActive(false); }

        this.img.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Supply/" + realName);
        if (supplyLocation == -1)
        {
            this.img.GetComponent<TooltipTrigger>().header = "소지중인 소모품 없음";
            this.img.GetComponent<TooltipTrigger>().content = "소지품 사용 불가";
        }
        else
        {
            this.img.GetComponent<TooltipTrigger>().content = batDescription;
            this.img.GetComponent<TooltipTrigger>().header = supplyName;
        }
    }
}
