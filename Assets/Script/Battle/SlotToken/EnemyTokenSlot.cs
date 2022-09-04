using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTokenSlot : MonoBehaviour
{
    [SerializeField] private int typeNum;
    [SerializeField] GameObject tokenPrefab;

    private void OnEnable()
    {
        SkillBtn.SkillChanged += SkillBtn_SkillChanged;
        SkillManager.SkillAddedEvent += SkillManager_SkillAddedEvent;
        SkillBtn.SkillDiaProgress += SkillBtn_SkillChanged;
        BattleDialogueProvider.betweenTurnDia += BattleDialogueProvider_betweenTurnDia;
    }

    private void OnDisable()
    {
        SkillBtn.SkillChanged -= SkillBtn_SkillChanged;
        SkillManager.SkillAddedEvent -= SkillManager_SkillAddedEvent;
        SkillBtn.SkillDiaProgress -= SkillBtn_SkillChanged;
        BattleDialogueProvider.betweenTurnDia -= BattleDialogueProvider_betweenTurnDia;
    }


    private void BattleDialogueProvider_betweenTurnDia()
    {
        int childCount = transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    private void SkillManager_SkillAddedEvent(ListWrapper<EnemySkill> arg1, int arg2)
    {
        for (int i = 0; i < arg1[0].tokens[typeNum]; i++)
        {
            Instantiate(tokenPrefab, transform);
        }
    }

    private void SkillBtn_SkillChanged(EnemySkill arg1, int arg2)
    {
        int childCount = transform.childCount;
        int temp = arg1.tokens[typeNum];
        if(childCount == temp) { return; }
        else if(childCount > temp)
        {
            for(int i = childCount - 1; i >= temp; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
        else
        {
            for (int i = childCount; i < temp; i++)
            {
                Instantiate(tokenPrefab, transform);
            }
        }
    }
}
