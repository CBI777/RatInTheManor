using UnityEngine;
using System;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private SaveM_Battle saveManager;
    private bool startFromMe;

    //������ ���ؼ�
    public static event Action enemySetEvent;
    //���� �����Ǹ� ���� ���̾�α׸� ���ؼ� �� �� �̺�Ʈ�� ���ش�.
    public static event Action<string, string, string> enemyDecidedEvent;

    //1. skill�� �޾Ƽ� skill ���� �����ϸ鼭 �ѷ���
    //2. �� skill�� SlotManager�� skillBtn�� �޾Ƹ���
    //�̹� ���� EnemySkill + Skill ����
    public static event Action<ListWrapper<EnemySkill>, int> SkillAddedEvent;
    //�� int�� ���� ���� ���Ѱ���.
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
        //save�Ǿ��ִ� ���� battle ��Ȳ���� ����Ǿ��ٸ� �����Ǿ��� enemy�� �ҷ��´�.
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
        //startFromMe�� ù��° ���忡���� ������ �Ǿ��� ���, �� �� -1 ���� ������ �Ǿ��� ��� �۵��Ѵ�.
        //startFromMe�� �ƴ϶�� �� �ƿ� ������ �� �Ǿ����ų�, �ƴϸ� �� -1�� �ƴҰ��ϱ� ���� ����.
        if(startFromMe)
        {
            showEnemyPic();
            enemyDecidedEvent?.Invoke(this.enemy.realName, this.enemy.deathSFX, this.enemy.bgm);
        }
    }

}
