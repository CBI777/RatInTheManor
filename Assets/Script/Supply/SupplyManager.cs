using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class SupplyManager : MonoBehaviour
{
    private List<Supply_Base> supply = new List<Supply_Base>();
    [SerializeField] private int curSupply = 0;
    [SerializeField] private int supplyCount;

    //현재 supply가 바뀌었음을 이야기해줌. 이건 UI쪽에서 사용
    //처음 int의 경우에는 양 끝인지 아닌지를 알려주기 위함. 0이면 prev를 없애고, 1이면 그냥 두고,
    //2면은 next를 없애야한다.
    public static event Action<int, string, string, string> CurSupplyChanged;
    //Supply 자체에 변화가 있을 경우
    public static event Action<int, Supply_Base[]> SupplyChangedEvent;

    private void OnEnable()
    {
        SupplyBtn.SupplyBtnPressed += SupplyBtn_SupplyBtnPressed;
        BattleResetManager.ResetBoardEvent += BattleResetManager_ResetBoardEvent;
    }

    private void OnDisable()
    {
        SupplyBtn.SupplyBtnPressed -= SupplyBtn_SupplyBtnPressed;
        BattleResetManager.ResetBoardEvent -= BattleResetManager_ResetBoardEvent;
    }

    private void BattleResetManager_ResetBoardEvent()
    {
        setSupply(0);
    }

    private void SupplyBtn_SupplyBtnPressed(bool obj)
    {
        if (obj) { setSupply((this.curSupply + 1)); }
        else { setSupply(this.curSupply - 1); }
    }

    private void setSupply(int n)
    {
        int temp = 0;

        if(n < 0 || n > supplyCount) { return; } //오류처리용

        if(this.supply[curSupply].usage != 0) { this.supply[curSupply].onStopUse(); }
        this.curSupply = n;
        if (this.supply[curSupply].usage != 0) { this.supply[curSupply].onUse(); }

        if(curSupply == 0) { temp = 0; }
        else if (curSupply == this.supplyCount) { temp = 2; }
        else { temp = 1; }
        CurSupplyChanged?.Invoke(temp, this.supply[curSupply].realName, this.supply[curSupply].supplyName, this.supply[curSupply].batDescription);
    }

    private void turnStart()
    {
        this.supplyCount = (this.supply.Count - 1);
        SupplyChangedEvent?.Invoke(supplyCount, this.supply.ToArray());
        setSupply(0);
    }

    private void Start()
    {
        //소모품은 반드시 NoUse 하나 이상을 가지고 있어야한다.
        this.supply.Add((Supply_Base)Activator.CreateInstance(Type.GetType("Supply_NoUse")));
        this.supply.Add((Supply_Base)Activator.CreateInstance(Type.GetType("Supply_Opium")));
        this.supply.Add((Supply_Base)Activator.CreateInstance(Type.GetType("Supply_Painkiller")));
        this.supply.Add((Supply_Base)Activator.CreateInstance(Type.GetType("Supply_Abhorrpainting")));
        turnStart();
    }
}
