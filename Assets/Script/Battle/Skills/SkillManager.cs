using UnityEngine;
using System;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private SaveM_Battle saveManager;
    private bool startFromMe;

    //저장을 위해서
    public static event Action enemySetEvent;
    //적이 결정되면 조우 다이얼로그를 위해서 한 번 이벤트를 쏴준다.
    public static event Action<string, string, string> enemyDecidedEvent;

    //1. skill을 받아서 skill 들어옴 ㅅㄱ하면서 뿌려줌
    //2. 이 skill을 SlotManager랑 skillBtn이 받아먹음
    //이번 턴의 EnemySkill + Skill 갯수
    public static event Action<ListWrapper<EnemySkill>, int> SkillAddedEvent;
    //이 int는 남은 턴을 위한거임.
    public static event Action<int> BattleStart;

    [SerializeField] private Enemy_Base enemy;
    [SerializeField] private GameObject enemyPic;

    public string getSelEnemy()
    {
        return enemy.realName;
    }

    private void OnEnable()
    {
        SaveM_Battle.firstSaveFinished += SaveM_Battle_firstSaveFinished;
        StageManager.StageSpread += StageManager_StageSpread;
        TurnManager.TurnStart += TurnManager_TurnStart;
        BattleDialogueProvider.PrevDialogueDone += BattleDialogueProvider_PrevDialogueDone;
        BattleDialogueProvider.FinalDia += BattleDialogueProvider_FinalDia;
        BattleDialogueProvider.startFromDialogue += showEnemyPic;
    }

    private void OnDisable()
    {
        SaveM_Battle.firstSaveFinished -= SaveM_Battle_firstSaveFinished;
        StageManager.StageSpread -= StageManager_StageSpread;
        TurnManager.TurnStart -= TurnManager_TurnStart;
        BattleDialogueProvider.PrevDialogueDone -= BattleDialogueProvider_PrevDialogueDone;
        BattleDialogueProvider.FinalDia -= BattleDialogueProvider_FinalDia;
        BattleDialogueProvider.startFromDialogue -= showEnemyPic;
    }

    private void showEnemyPic()
    {
        this.enemyPic.SetActive(true);
        this.enemyPic.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Enemy/" + enemy.realName);
    }

    private void SaveM_Battle_firstSaveFinished()
    {
        enemyDecidedEvent?.Invoke(this.enemy.realName, this.enemy.deathSFX, this.enemy.bgm);
    }

    private void BattleDialogueProvider_FinalDia()
    {
        enemyPic.SetActive(false);
    }

    private void BattleDialogueProvider_PrevDialogueDone()
    {
        BattleStart?.Invoke(this.enemy.turnCount);
    }

    private void StageManager_StageSpread(int obj)
    {
        int temp = UnityEngine.Random.Range(1, 101);
        ListWrapper<EnemyVariation> varis = (Resources.Load<StageVariations>("ScriptableObject/StageVariation/Stage" + obj)).variationList;
        int i = 0;

        while (true)
        {
            if (temp < varis[i].num)
            {
                this.enemy = Resources.Load<Enemy_Base>("ScriptableObject/Enemy/" + varis[i].realName);
                break;
            }
            i++;
        }
        showEnemyPic();
        enemySetEvent?.Invoke();
    }
        

    private void TurnManager_TurnStart(int count)
    {
        SkillAddedEvent?.Invoke(enemy.skillList[count], enemy.skillList[count].Count());
    }

    private void Awake()
    {
        //save되어있는 것이 battle 상황에서 저장되었다면 결정되었던 enemy를 불러온다.
        if (saveManager.saving.isBattle)
        {
            if(saveManager.saving.turn == -1) { startFromMe = true; }
            else { startFromMe = false; }
            this.enemy = Resources.Load<Enemy_Base>("ScriptableObject/Enemy/" + saveManager.saving.selEnemy);
        }
        else
        {
            startFromMe = false;
        }
    }

    private void Start()
    {
        //startFromMe는 첫번째 저장에서만 저장이 되었을 경우, 즉 턴 -1 에서 저장이 되었을 경우 작동한다.
        //startFromMe가 아니라는 건 아예 저장이 안 되었었거나, 아니면 턴 -1이 아닐경우니까 문제 없음.
        if(startFromMe)
        {
            showEnemyPic();
            enemyDecidedEvent?.Invoke(this.enemy.realName, this.enemy.deathSFX, this.enemy.bgm);
        }
    }

}
