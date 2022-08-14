using System.Collections.Generic;
using UnityEngine;
using System;

public class SupplyManager : MonoBehaviour
{
    private List<Supply_Base> supply = new List<Supply_Base>();
    [SerializeField] private int curSupply = 0;
    [SerializeField] private int supplyCount;

    [SerializeField] private SaveM_Battle saveManager;

    public string[] getSupplyString()
    {
        string[] supplyString = new string[6];

        for(int i = 0; i < 6; i++)
        {
            if(i > supplyCount)
            {
                supplyString[i] = "NA";
            }
            else
            {
                supplyString[i] = this.supply[i].realName;
            }
        }

        return supplyString;
    }

    //현재 supply가 바뀌었음을 이야기해줌. 이건 UI쪽에서 사용
    //처음 int의 경우에는 양 끝인지 아닌지를 알려주기 위함. 0이면 prev를 없애고, 1이면 그냥 두고,
    //2면은 next를 없애야한다.
    public static event Action<int, string, string, string> CurSupplyChanged;
    //Supply 자체에 변화가 있을 경우
    public static event Action<int, Supply_Base[]> SupplyChangedEvent;
    public static event Action<string> supplyUsed;

    private void OnEnable()
    {
        SupplyBtn.SupplyBtnPressed += SupplyBtn_SupplyBtnPressed;
        BattleResetManager.ResetBoardEvent += BattleResetManager_ResetBoardEvent;
        BattleDialogueProvider.betweenTurnDia += BattleDialogueProvider_betweenTurnDia;
        TurnEndBtn.TurnEndEvent += TurnEndBtn_TurnEndEvent;
    }

    private void OnDisable()
    {
        SupplyBtn.SupplyBtnPressed -= SupplyBtn_SupplyBtnPressed;
        BattleResetManager.ResetBoardEvent -= BattleResetManager_ResetBoardEvent;
        BattleDialogueProvider.betweenTurnDia -= BattleDialogueProvider_betweenTurnDia;
        TurnEndBtn.TurnEndEvent -= TurnEndBtn_TurnEndEvent;
    }

    private void TurnEndBtn_TurnEndEvent()
    {
        if(curSupply != 0)
        {
            if(supply[curSupply].usage != 0)
            {
                //supply[curSupply].onLose();
                supplyUsed?.Invoke(supply[curSupply].supplyName);
                supply.RemoveAt(curSupply);
            }
        }
    }
    private void BattleDialogueProvider_betweenTurnDia()
    {
        turnStart();
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

        if(this.supplyCount == 0) {temp = -1; }
        else if(curSupply == 0) { temp = 0; }
        else if (curSupply == this.supplyCount) { temp = 2; }
        else { temp = 1; }
        CurSupplyChanged?.Invoke(temp, this.supply[curSupply].realName, this.supply[curSupply].supplyName, this.supply[curSupply].batDescription);
    }

    private void turnStart()
    {
        //supply에는 noUse가 들어가 있기 때문에 실제로 있는 supply의 갯수는 거기서 -1을 한 것이 된다.
        this.supplyCount = (this.supply.Count - 1);
        curSupply = 0;
        SupplyChangedEvent?.Invoke(supplyCount, this.supply.ToArray());
        setSupply(0);
    }

    private void Awake()
    {
        string[] temp = this.saveManager.saving.supply;

        for(int i = 0; i < temp.Length; i++)
        {
            if (temp[i] != "NA")
            {
                this.supply.Add((Supply_Base)Activator.CreateInstance(Type.GetType(temp[i])));
            }
        }
    }

    private void Start()
    {
        turnStart();
    }
}
