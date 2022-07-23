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
        SupplyManager.SupplyChangedEvent += SupplyManager_SupplyChangedEvent;
    }

    private void OnDisable()
    {
        SupplyManager.CurSupplyChanged -= SupplyManager_CurSupplyChanged;
        SupplyManager.SupplyChangedEvent -= SupplyManager_SupplyChangedEvent;
    }

    private void SupplyManager_SupplyChangedEvent(int count, Supply_Base[] arg2)
    {
        if(count == 0)
        {
            this.nextBtn.SetActive(false);
        }
        this.prevBtn.SetActive(false);
        this.img.GetComponent<TooltipTrigger>().content = arg2[0].batDescription;
        this.img.GetComponent<TooltipTrigger>().header = arg2[0].supplyName;
    }

    private void SupplyManager_CurSupplyChanged(int supplyLocation, string realName, string supplyName, string batDescription)
    {
        if (supplyLocation == 0) { this.prevBtn.SetActive(false); }
        else { if (!this.prevBtn.activeSelf) { this.prevBtn.SetActive(true); } }
        if (supplyLocation == 2) { this.nextBtn.SetActive(false); }
        else { if (!this.nextBtn.activeSelf) { this.nextBtn.SetActive(true); } }

        this.img.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Supply/" + realName);
        this.img.GetComponent<TooltipTrigger>().content = batDescription;
        this.img.GetComponent<TooltipTrigger>().header = supplyName;
    }
}
