using UnityEngine;
using System;
using TMPro;

public class TurnManager : MonoBehaviour
{
    public static event Action<int> TurnStart;
    public static event Action BattleEndEvent;
    public static event Action BatEndReached;

    [SerializeField] private SaveM_Battle saveManager;
    private Enemy_Base enemy;

    [SerializeField] private GameObject counter;

    [SerializeField] private int turnCount;
    [SerializeField] private int turnLimit;

    public int getTurnCount() { return turnCount; }

    private void OnEnable()
    {
        BattleDialogueProvider.startFromDialogue += TurnEndBtn_TurnEndEvent;
        SkillManager.BattleStart += SkillManager_BattleStart;
        BattleDialogueProvider.turnStartDiaEnd += BattleDialogueProvider_turnStartDiaEnd;
        TurnEndBtn.TurnEndEvent += TurnEndBtn_TurnEndEvent;
        BattleDialogueProvider.FinalDia += BattleDialogueProvider_FinalDia;
        SaveM_Battle.finalSaveFinished += SaveM_Battle_finalSaveFinished;
    }

    private void OnDisable()
    {
        BattleDialogueProvider.startFromDialogue -= TurnEndBtn_TurnEndEvent;
        SkillManager.BattleStart -= SkillManager_BattleStart;
        BattleDialogueProvider.turnStartDiaEnd -= BattleDialogueProvider_turnStartDiaEnd;
        TurnEndBtn.TurnEndEvent -= TurnEndBtn_TurnEndEvent;
        BattleDialogueProvider.FinalDia -= BattleDialogueProvider_FinalDia;
        SaveM_Battle.finalSaveFinished -= SaveM_Battle_finalSaveFinished;
    }

    private void SaveM_Battle_finalSaveFinished()
    {
        BattleEndEvent?.Invoke();
    }

    private void BattleDialogueProvider_FinalDia()
    {
        counter.SetActive(false);
    }

    private void TurnEndBtn_TurnEndEvent()
    {
        counter.SetActive(true);
        this.counter.GetComponentInChildren<TextMeshProUGUI>().SetText("남은 턴 : " + (this.turnLimit - this.turnCount - 1));
    }

    private void BattleDialogueProvider_turnStartDiaEnd()
    {
        turnCount++;
        if(turnCount == turnLimit)
        {
            BatEndReached?.Invoke();
        }
        else
        {
            TurnStart?.Invoke(turnCount);
        }
    }

    private void SkillManager_BattleStart(int count)
    {
        turnLimit = count;
        this.turnCount = 0;
        counter.SetActive(true);
        this.counter.GetComponentInChildren<TextMeshProUGUI>().SetText("남은 턴 : " + (this.turnLimit - this.turnCount));
        TurnStart?.Invoke(turnCount);
    }

    private void Awake()
    {
        this.turnCount = -1;
        if(this.saveManager.saving.isBattle)
        {
            this.turnCount = this.saveManager.saving.turn;
            this.enemy = Resources.Load<Enemy_Base>("ScriptableObject/Enemy/" + saveManager.saving.selEnemy);
            this.turnLimit = this.enemy.turnCount;
        }

        counter.SetActive(false);
    }
}
