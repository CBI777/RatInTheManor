using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public enum DialogueProgress
{
    Bat, Skill, Status
};

public class BattleDialogueProvider : MonoBehaviour
{
    [SerializeField] DialogueProgress diaProg;

    [SerializeField] private Button nextBtn;
    [SerializeField] private Button proceedBtn;

    [SerializeField] private Battle_Script batScript;
    [SerializeField] private EnemySkillDialogue skillScript;
    //status script

    [SerializeField] private int bookmark = 0;
    [SerializeField] private int batBookmark = 0;
    [SerializeField] private int skillBookmark = 0;
    //bookmark = 각 dialogue 진행시의 책갈피
    //batbookmark = 시작 / 턴 사이에 나오는 enemy 전용 dialogue의 chapter를 찝어두는 책갈피
    //skillScript = 각 skill 발동시에 나오는 enemy의 skill dialogue의 chapter를 찝어두는 책갈피

    public static event Action PrevDialogueDone;
    public static event Action betweenTurnDia;
    public static event Action turnStartDiaEnd;

    //이걸로 몇 번째(int) 스킬의 dialogue가 진행될 것인지를 알려줌.
    public static event Action<int> skillDiaStart;

    private void OnEnable()
    {
        nextBtn.interactable = false;
        proceedBtn.interactable = false;

        SkillManager.enemyDecidedEvent += SkillManager_enemyDecidedEvent;
        TurnEndBtn.TurnEndEvent += TurnEndBtn_TurnEndEvent;
        SlotManager.TotalDmgPass += SlotManager_FinalDmgPass;
        BattleDialogueProvider.betweenTurnDia += BattleDialogueProvider_betweenTurnDia;
    }

    private void OnDisable()
    {
        SkillManager.enemyDecidedEvent -= SkillManager_enemyDecidedEvent;
        TurnEndBtn.TurnEndEvent -= TurnEndBtn_TurnEndEvent;
        SlotManager.TotalDmgPass -= SlotManager_FinalDmgPass;
        BattleDialogueProvider.betweenTurnDia -= BattleDialogueProvider_betweenTurnDia;
    }

    private void BattleDialogueProvider_betweenTurnDia()
    {
        scriptDisplay(batScript.script[batBookmark][bookmark]);
        bookmark++;

        if (batScript.script[batBookmark].Count() == 1)
        {
            proceedBtn.interactable = true;
            nextBtn.interactable = false;
        }
        else
        {
            nextBtn.interactable = true;
        }
    }

    private void SlotManager_FinalDmgPass(int obj)
    {
        scriptDisplay(skillScript.skillDia[skillBookmark][bookmark]);
        scriptDisplay(false, "이성에 " + obj + "의 피해를 입었다!");


        if ((skillScript.skillDia[skillBookmark].Count() - 1) == bookmark)
        {
            proceedBtn.interactable = true;
            nextBtn.interactable = false;
        }
        else
        {
            nextBtn.interactable = true;
        }
    }

    private void TurnEndBtn_TurnEndEvent()
    {
        //이게 들어왔다는 것은 방어 진행중이라는 것
        this.diaProg = DialogueProgress.Skill;
        skillDiaStart?.Invoke(bookmark);
    }

    private void scriptDisplay(scriptClass a)
    {
        if (a.isPorted)
        {
            DialogueManager.Instance.addDialogue(a.portName, a.isRight, a.line);
        }
        else
        {
            DialogueManager.Instance.addDialogue(a.isRight, a.line);
        }
    }
    private void scriptDisplay(SkillDialogue a)
    {
        DialogueManager.Instance.addDialogue(true, a.dialogue);
    }
    private void scriptDisplay(bool isRight, string portName, string line)
    {
        DialogueManager.Instance.addDialogue(portName, isRight, line);
    }
    private void scriptDisplay(bool isRight, string line)
    {
        DialogueManager.Instance.addDialogue(isRight, line);
    }

    private void SkillManager_enemyDecidedEvent(string obj)
    {
        //이게 들어왔다는 것은 턴 시작 전에 보여줄 것이라는 것
        this.diaProg = DialogueProgress.Bat;
        this.batScript = Resources.Load<Battle_Script>("ScriptableObject/BattleScript/" + obj);
        this.skillScript = Resources.Load<EnemySkillDialogue>("ScriptableObject/EnemySkillDialogue/" + obj);

        scriptDisplay(batScript.script[batBookmark][bookmark]);
        bookmark++;

        if (batScript.script[batBookmark].Count() == 1)
        {
            proceedBtn.interactable = true;
            nextBtn.interactable = false;
        }
        else
        {
            nextBtn.interactable = true;
        }
    }

    private void diaNextClicked()
    {
        if (this.diaProg == DialogueProgress.Bat)
        {
            scriptDisplay(batScript.script[batBookmark][bookmark]);
            bookmark++;
            if (batScript.script[batBookmark].Count() == bookmark)
            {
                proceedBtn.interactable = true;
                nextBtn.interactable = false;
            }
        }
        else if(this.diaProg == DialogueProgress.Skill)
        {
            bookmark++;
            skillDiaStart?.Invoke(bookmark);
        }
        else
        {
            /*status로 수정*/
        }
    }
    private void diaProceedClicked()
    {
        if (this.diaProg == DialogueProgress.Bat)
        {
            if (batBookmark == 0)
            {
                PrevDialogueDone?.Invoke();
            }
            else
            {
                DialogueManager.Instance.clearDialogue();
                turnStartDiaEnd?.Invoke();
            }
            //0번이 아니라면 이제 turn이 시작해야함.

            batBookmark++;
            bookmark = 0;
            proceedBtn.interactable = false;
        }
        else if (this.diaProg == DialogueProgress.Skill)
        {
            skillBookmark++;
            bookmark = 0;
            proceedBtn.interactable = false;

            this.diaProg = DialogueProgress.Bat;
            betweenTurnDia?.Invoke();
        }
    }
}
