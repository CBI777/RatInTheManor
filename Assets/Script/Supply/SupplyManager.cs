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

    //���� supply�� �ٲ������ �̾߱�����. �̰� UI�ʿ��� ���
    //ó�� int�� ��쿡�� �� ������ �ƴ����� �˷��ֱ� ����. 0�̸� prev�� ���ְ�, 1�̸� �׳� �ΰ�,
    //2���� next�� ���־��Ѵ�.
    public static event Action<int, string, string, string> CurSupplyChanged;
    //Supply ��ü�� ��ȭ�� ���� ���
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

        if(n < 0 || n > supplyCount) { return; } //����ó����

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
        //supply���� noUse�� �� �ֱ� ������ ������ �ִ� supply�� ������ �ű⼭ -1�� �� ���� �ȴ�.
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
