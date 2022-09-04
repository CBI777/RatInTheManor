using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class SkillBtn : MonoBehaviour
{
    [SerializeField]
    private EnemySkill[] skill = new EnemySkill[3];
    [SerializeField] GameObject[] btns = new GameObject[3];
    [SerializeField] GameObject[] highlights = new GameObject[3];

    public static event Action<EnemySkill, int> SkillChanged;
    public static event Action<EnemySkill, int> SkillDiaProgress;

    private void OnEnable()
    {
        for(int i = 0; i< btns.Length; i++) { btns[i].SetActive(false); }
        SkillManager.SkillAddedEvent += SkillManager_SkillAddedEvent;
        SkillBtn.SkillChanged += SkillBtn_SkillChanged;
        BattleDialogueProvider.skillDiaStart += BattleDialogueProvider_skillDiaStart;
        SkillBtn.SkillDiaProgress += SkillBtn_SkillDiaProgress;
        BattleDialogueProvider.betweenTurnDia += BattleDialogueProvider_betweenTurnDia;
    }


    private void OnDisable()
    {
        SkillManager.SkillAddedEvent -= SkillManager_SkillAddedEvent;
        SkillBtn.SkillChanged -= SkillBtn_SkillChanged;
        BattleDialogueProvider.skillDiaStart -= BattleDialogueProvider_skillDiaStart;
        SkillBtn.SkillDiaProgress -= SkillBtn_SkillDiaProgress;
        BattleDialogueProvider.betweenTurnDia -= BattleDialogueProvider_betweenTurnDia;
    }


    private void BattleDialogueProvider_betweenTurnDia()
    {
        for (int i = 0; i < btns.Length; i++)
        {
            if(btns[i] != null)
            {
                highlights[i].SetActive(false);
                btns[i].SetActive(false);
            }
        }
    }

    private void SkillBtn_SkillDiaProgress(EnemySkill arg1, int arg2)
    {
        for (int i = 0; i < btns.Length; i++)
        {
            if (i == arg2)
            {
                highlights[i].SetActive(true);
            }
            else
            {
                if (btns[i].activeSelf)
                {
                    highlights[i].SetActive(false);
                }
            }
        }
    }

    private void BattleDialogueProvider_skillDiaStart(int obj)
    {
        if(obj == 0)
        {
            for (int i = 1; i < btns.Length; i++)
            {
                if (btns[i].activeSelf)
                {
                    highlights[i].SetActive(false);
                    btns[i].GetComponent<Button>().interactable = false;
                }
            }
        }
        highlights[obj].SetActive(true);
        SkillDiaProgress?.Invoke(skill[obj], obj);
    }

    private void SkillManager_SkillAddedEvent(ListWrapper<EnemySkill> arg1, int arg2)
    {
        for(int i = 0; i < btns.Length; i++)
        {
            if(i < arg2)
            {
                btns[i].SetActive(true);
                skill[i] = arg1[i];
                btns[i].GetComponentInChildren<TextMeshProUGUI>().SetText(skill[i].skillName);
                if (i == 0)
                {
                    btns[i].GetComponent<Button>().interactable = false;
                    highlights[i].SetActive(true);
                }
                else
                {
                    btns[i].GetComponent<Button>().interactable = true;
                    highlights[i].SetActive(false);
                }
            }
            else
            {
                btns[i].GetComponentInChildren<TextMeshProUGUI>().SetText("");
                btns[i].SetActive(false);
            }
        }
    }

    public void SkillChange(int num)
    {
        SkillChanged?.Invoke(skill[num], num);
    }

    private void SkillBtn_SkillChanged(EnemySkill arg1, int arg2)
    {
        for (int i = 0; i < btns.Length; i++)
        {
            if (i == arg2)
            {
                highlights[i].SetActive(true);
                btns[i].GetComponent<Button>().interactable = false;
            }
            else
            {
                if (btns[i].activeSelf)
                {
                    highlights[i].SetActive(false);
                    btns[i].GetComponent<Button>().interactable = true;
                }
            }
        }
    }
}
