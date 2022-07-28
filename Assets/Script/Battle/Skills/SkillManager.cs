using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    //적이 결정되면 조우 다이얼로그를 위해서 한 번 이벤트를 쏴준다.
    public static event Action<string> enemyDecidedEvent;

    //1. skill을 받아서 skill 들어옴 ㅅㄱ하면서 뿌려줌
    //2. 이 skill을 SlotManager랑 skillBtn이 받아먹음
    //이번 턴의 EnemySkill + Skill 갯수
    public static event Action<ListWrapper<EnemySkill>, int> SkillAddedEvent;

    //skillManager가 StageManager한테서 stage를 받으면, 그거에 맞춰서 skillmanager가 enemy를 고르고, 그러면 BattleStart임.
    private StageVariations vari = new StageVariations();

    //이 int는 남은 턴을 위한거임.
    public static event Action<int> BattleStart;

    [SerializeField] private Enemy_Base enemy;
    [SerializeField] private Image enemyPic;


    private void OnEnable()
    {
        StageManager.StageSpread += StageManager_StageSpread;
        TurnManager.TurnStart += TurnManager_TurnStart;
        BattleDialogueProvider.PrevDialogueDone += BattleDialogueProvider_PrevDialogueDone;
    }

    private void OnDisable()
    {
        StageManager.StageSpread -= StageManager_StageSpread;
        TurnManager.TurnStart -= TurnManager_TurnStart;
        BattleDialogueProvider.PrevDialogueDone -= BattleDialogueProvider_PrevDialogueDone;
    }

    private void BattleDialogueProvider_PrevDialogueDone()
    {
        BattleStart?.Invoke(this.enemy.turnCount);
    }

    private void StageManager_StageSpread(int obj)
    {
        int temp = UnityEngine.Random.Range(1, 101);
        List<EnemyVariation> varis = vari.variations[obj];
        int i = 0;

        while(true)
        {
            if(temp < varis[i].num)
            {
                this.enemy = Resources.Load<Enemy_Base>("ScriptableObject/Enemy/" + varis[i].realName);
                this.enemyPic.sprite = Resources.Load<Sprite>("Sprite/Enemy/" + enemy.realName);
                enemyDecidedEvent?.Invoke(this.enemy.realName);
                break;
            }
            i++;
        }
    }

    private void TurnManager_TurnStart(int obj)
    {
        skillSpread(obj);
    }

    public void skillSpread(int count)
    {
        SkillAddedEvent?.Invoke(enemy.skillList[count], enemy.skillList[count].Count());
    }

}
