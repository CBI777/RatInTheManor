using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class SkillBtn : MonoBehaviour
{
    [SerializeField] private int skillNum;
    private EnemySkill skill;
    [SerializeField] private TextMeshProUGUI myText;
    private bool isLoaded = false;
    [SerializeField] Button btn;

    public static event Action<EnemySkill, int> SkillChanged;

    private void OnEnable()
    {
        btn = this.transform.GetComponent<Button>();
        myText = this.transform.GetComponentInChildren<TextMeshProUGUI>();
        SkillManager.SkillAddedEvent += SkillManager_SkillAddedEvent;
        SkillBtn.SkillChanged += SkillBtn_SkillChanged;
    }

    private void OnDisable()
    {
        SkillManager.SkillAddedEvent -= SkillManager_SkillAddedEvent;
        SkillBtn.SkillChanged -= SkillBtn_SkillChanged;
    }

    private void SkillManager_SkillAddedEvent(List<EnemySkill> arg1, int arg2)
    {
        if(skillNum < arg2)
        {
            this.transform.transform.gameObject.SetActive(true);
            skill = arg1[skillNum];
            myText.SetText(skill.skillName);
            isLoaded = true;
        }
        else
        {
            myText.SetText("");
            isLoaded = false;
            this.transform.transform.gameObject.SetActive(false);
        }

        if(this.skillNum == 0)
        {
            btn.interactable = false;
        }
    }

    public void SkillChange()
    {
        SkillChanged?.Invoke(skill, skillNum);
    }

    private void SkillBtn_SkillChanged(EnemySkill arg1, int arg2)
    {
        if(this.skillNum == arg2)
        {
            btn.interactable = false;
        }
        else
        {
            if(isLoaded == true)
            {
                btn.interactable = true;
            }
        }
    }
}
