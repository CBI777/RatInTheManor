using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class SupplyManager : MonoBehaviour
{
    [SerializeField] private GameObject img;
    [SerializeField] private GameObject prevBtn;
    [SerializeField] private GameObject nextBtn;

    private List<Supply_Base> supply = new List<Supply_Base>();
    [SerializeField] private int curSupply = 0;
    [SerializeField] private int supplyCount;

    private void OnEnable()
    {
        SupplyBtn.SupplyBtnPressed += SupplyBtn_SupplyBtnPressed;
    }

    private void OnDisable()
    {
        SupplyBtn.SupplyBtnPressed -= SupplyBtn_SupplyBtnPressed;
    }

    private void SupplyBtn_SupplyBtnPressed(bool obj)
    {
        if (obj) { setSupply((this.curSupply + 1)); }
        else { setSupply(this.curSupply - 1); }
    }

    private void setSupply(int n)
    {
        if(n < 0 || n > (supplyCount -1)) { return; }

        if(this.supply[curSupply].usage != 0) { this.supply[curSupply].onStopUse(); }
        //이미지 변경
        this.curSupply = n;
        Debug.Log("curSupply : " + curSupply);
        if(curSupply == 0) { this.prevBtn.SetActive(false); }
        else { if (!this.prevBtn.activeSelf) { this.prevBtn.SetActive(true); } }
        if (this.curSupply == (this.supplyCount - 1)) { this.nextBtn.SetActive(false); }
        else { if (!this.nextBtn.activeSelf) { this.nextBtn.SetActive(true); } }

        this.img.GetComponent<TooltipTrigger>().content = this.supply[curSupply].batDescription;
        this.img.GetComponent<TooltipTrigger>().header = this.supply[curSupply].supplyName;

        if (this.supply[curSupply].usage != 0) { this.supply[curSupply].onUse(); }
    }

    private void turnStart()
    {
        this.supplyCount = this.supply.Count;
        setSupply(0);
    }

    private void Awake()
    {
        this.supply.Add((Supply_Base)Activator.CreateInstance(Type.GetType("Supply_NoUse")));
        this.supply.Add((Supply_Base)Activator.CreateInstance(Type.GetType("Supply_Opium")));
        this.supply.Add((Supply_Base)Activator.CreateInstance(Type.GetType("Supply_Painkiller")));
        this.supply.Add((Supply_Base)Activator.CreateInstance(Type.GetType("Supply_Abhorrpainting")));

        turnStart();
    }
}
