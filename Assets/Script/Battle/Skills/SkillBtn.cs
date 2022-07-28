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

    public static event Action<EnemySkill, int> SkillChanged;

    private void OnEnable()
    {
        for(int i = 0; i< btns.Length; i++) { btns[i].SetActive(false); }
        SkillManager.SkillAddedEvent += SkillManager_SkillAddedEvent;
        SkillBtn.SkillChanged += SkillBtn_SkillChanged;
    }

    private void OnDisable()
    {
        SkillManager.SkillAddedEvent -= SkillManager_SkillAddedEvent;
        SkillBtn.SkillChanged -= SkillBtn_SkillChanged;
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
                if (i == 0) { btns[i].GetComponent<Button>().interactable = false; }
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
                btns[i].GetComponent<Button>().interactable = false;
            }
            else
            {
                if (btns[i].activeSelf)
                {
                    btns[i].GetComponent<Button>().interactable = true;
                }
            }
        }
    }
}
