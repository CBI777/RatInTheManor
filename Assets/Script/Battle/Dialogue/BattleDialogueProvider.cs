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
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource nextSource;
    [SerializeField] private AudioClip nextClip;

    private Battle_Script batScript;
    private EnemySkillDialogue skillScript;
    [SerializeField] private Battle_Script statusScriptBase;
    List<scriptClass> realStatusScript = new List<scriptClass>();

    private int bookmark = 0;
    private int batBookmark = 0;
    private int skillBookmark = 0;

    private int batBookmarkLimit = 0;
    private string deathSFX;

    private bool isMad = false;
    private scriptClass supplyDia = new scriptClass(false, false, "", "");
    private bool supplyUsed = false;
    //bookmark = �� dialogue ������� å����
    //batbookmark = ���� / �� ���̿� ������ enemy ���� dialogue�� chapter�� ���δ� å����
    //skillScript = �� skill �ߵ��ÿ� ������ enemy�� skill dialogue�� chapter�� ���δ� å����

    public static event Action PrevDialogueDone;
    public static event Action betweenTurnDia;
    public static event Action turnStartDiaEnd;

    public static event Action FinalDia;
    //�̰ɷ� �� ��°(int) ��ų�� dialogue�� ����� �������� �˷���.
    public static event Action<int> skillDiaStart;

    private void OnEnable()
    {
        nextBtn.interactable = false;
        proceedBtn.interactable = false;

        SkillManager.enemyDecidedEvent += SkillManager_enemyDecidedEvent;
        TurnEndBtn.TurnEndEvent += TurnEndBtn_TurnEndEvent;
        SlotManager.TotalDmgPass += SlotManager_FinalDmgPass;
        StatusManager.TurnResultToss += StatusManager_TurnResultToss;
        SupplyManager.supplyUsed += SupplyManager_supplyUsed;
    }

    private void OnDisable()
    {
        SkillManager.enemyDecidedEvent -= SkillManager_enemyDecidedEvent;
        TurnEndBtn.TurnEndEvent -= TurnEndBtn_TurnEndEvent;
        SlotManager.TotalDmgPass -= SlotManager_FinalDmgPass;
        StatusManager.TurnResultToss -= StatusManager_TurnResultToss;
        SupplyManager.supplyUsed -= SupplyManager_supplyUsed;
    }


    private void SupplyManager_supplyUsed(string obj)
    {
        this.supplyUsed = true;
        supplyDia.line = obj + " ��/�� ����ߴ�...";
    }

    private void StatusManager_TurnResultToss(int over, int san, int mad, bool obs)
    {
        DialogueManager.Instance.clearDialogue();
        this.diaProg = DialogueProgress.Status;
        realStatusScript.Clear();
        int count = 0;

        //���Ⱑ �����ٸ�
        if (mad >= 100)
        {
            isMad = true;
            count = statusScriptBase.script[5].Count();
            for (int i = 0; i < count; i++)
            {
                realStatusScript.Add(statusScriptBase.script[5][i]);
            }
        }
        else
        {
            //�̼��� �������� ��
            if (over == 0)
            {
                if (san >= 80)
                {
                    realStatusScript.Add(statusScriptBase.script[4][0]);
                }
                else if (san >= 50)
                {
                    realStatusScript.Add(statusScriptBase.script[4][1]);
                }
                else if (san >= 30)
                {
                    realStatusScript.Add(statusScriptBase.script[4][2]);
                }
                else
                {
                    realStatusScript.Add(statusScriptBase.script[4][3]);
                }
                if(supplyUsed)
                {
                    realStatusScript.Add(supplyDia);
                }
                realStatusScript.Add(statusScriptBase.script[6][0]);
            }
            //�̼��� ������ ��
            else
            {
                //�������� ������ ��
                if (over == 1)
                {
                    count = statusScriptBase.script[0].Count();
                    for (int i = 0; i < count; i++)
                    {
                        realStatusScript.Add(statusScriptBase.script[0][i]);
                    }
                }
                //�����ؼ� ������ ��
                else if (over == 2)
                {
                    count = statusScriptBase.script[1].Count();
                    for (int i = 0; i < count; i++)
                    {
                        realStatusScript.Add(statusScriptBase.script[1][i]);
                    }
                }

                int check = mad / 20;
                realStatusScript.Add(statusScriptBase.script[3][check]);

                if (obs)
                {
                    count = statusScriptBase.script[2].Count();
                    for (int i = 0; i < count; i++)
                    {
                        realStatusScript.Add(statusScriptBase.script[2][i]);
                    }
                }
                if(supplyUsed) { realStatusScript.Add(supplyDia); }
                
                realStatusScript.Add(statusScriptBase.script[6][0]);
            }
        }

        supplyUsed = false;
        scriptDisplay(realStatusScript[bookmark++]);
        if (realStatusScript.Count == 1)
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
        audioSource.PlayOneShot(Resources.Load<AudioClip>("SFX/Battle/" + skillScript.skillDia[skillBookmark][bookmark].sfxName));
        scriptDisplay(skillScript.skillDia[skillBookmark][bookmark]);
        scriptDisplay(false, "�̼��� " + obj + "�� ���ظ� �Ծ���!");

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
        //�̰� ���Դٴ� ���� ��� �������̶�� ��
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

    private void SkillManager_enemyDecidedEvent(string obj, string sfx)
    {
        //�̰� ���Դٴ� ���� �� ���� ���� ������ ���̶�� ��
        this.deathSFX = sfx;
        this.batScript = Resources.Load<Battle_Script>("ScriptableObject/BattleScript/" + obj);
        this.skillScript = Resources.Load<EnemySkillDialogue>("ScriptableObject/EnemySkillDialogue/" + obj);
        batBookmarkLimit = batScript.script.Count;
        batDialogueStart();
    }

    private void batDialogueStart()
    {
        this.diaProg = DialogueProgress.Bat;
        if((this.batBookmark + 1) == batBookmarkLimit)
        {
            audioSource.PlayOneShot(Resources.Load<AudioClip>("SFX/Battle/" + this.deathSFX));
            FinalDia?.Invoke();
        }
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
        nextSource.PlayOneShot(nextClip);
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
            scriptDisplay(realStatusScript[bookmark++]);
            if (realStatusScript.Count == bookmark)
            {
                proceedBtn.interactable = true;
                nextBtn.interactable = false;
            }
        }
    }
    private void diaProceedClicked()
    {
        nextSource.PlayOneShot(nextClip);
        if (this.diaProg == DialogueProgress.Bat)
        {
            if (batBookmark == 0)
            {
                PrevDialogueDone?.Invoke();
            }
            else
            {
                turnStartDiaEnd?.Invoke();
            }
            //0���� �ƴ϶�� ���� turn�� �����ؾ���.

            batBookmark++;
            bookmark = 0;
            proceedBtn.interactable = false;
        }
        else if (this.diaProg == DialogueProgress.Skill)
        {
            skillBookmark++;
            bookmark = 0;
            proceedBtn.interactable = false;

            betweenTurnDia?.Invoke();
        }
        else
        {
            if(isMad) { /*���ӿ���*/}
            bookmark = 0;
            proceedBtn.interactable = false;
            batDialogueStart();
        }
    }
}
