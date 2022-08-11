using UnityEngine;
using System;
using TMPro;

public class TurnManager : MonoBehaviour
{
    public static event Action<int> TurnStart;
    public static event Action BattleEndEvent;

    [SerializeField] private GameObject counter;

    [SerializeField] private int turnCount;
    [SerializeField] private int turnLimit;

    private void OnEnable()
    {
        counter.SetActive(false);
        SkillManager.BattleStart += SkillManager_BattleStart;
        BattleDialogueProvider.turnStartDiaEnd += BattleDialogueProvider_turnStartDiaEnd;
        TurnEndBtn.TurnEndEvent += TurnEndBtn_TurnEndEvent;
        BattleDialogueProvider.FinalDia += BattleDialogueProvider_FinalDia;
    }

    private void OnDisable()
    {
        SkillManager.BattleStart -= SkillManager_BattleStart;
        BattleDialogueProvider.turnStartDiaEnd -= BattleDialogueProvider_turnStartDiaEnd;
        TurnEndBtn.TurnEndEvent -= TurnEndBtn_TurnEndEvent;
        BattleDialogueProvider.FinalDia -= BattleDialogueProvider_FinalDia;
    }

    private void BattleDialogueProvider_FinalDia()
    {
        counter.SetActive(false);
    }

    private void TurnEndBtn_TurnEndEvent()
    {
        this.counter.GetComponentInChildren<TextMeshProUGUI>().SetText("남은 턴 : " + (this.turnLimit - this.turnCount - 1));
    }

    private void BattleDialogueProvider_turnStartDiaEnd()
    {
        turnCount++;
        if(turnCount == turnLimit)
        {
            BattleEndEvent?.Invoke();
        }
        else
        {
            TurnStart?.Invoke(turnCount);
        }
    }

    private void SkillManager_BattleStart(int count)
    {
        turnLimit = count;
        turnCount = 0;
        counter.SetActive(true);
        this.counter.GetComponentInChildren<TextMeshProUGUI>().SetText("남은 턴 : " + (this.turnLimit - this.turnCount));
        TurnStart?.Invoke(turnCount);
    }

    //turnEnd를 받아서 일 처리 중에 turncount와 limit이 같아진다면 battleend
}
