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
    [SerializeField] Image img;

    public static event Action<EnemySkill, int> SkillChanged;

    private void OnEnable()
    {
        btn = this.transform.GetComponent<Button>();
        img = this.transform.GetComponent<Image>();
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
            skill = arg1[skillNum];
            myText.SetText(skill.skillName);
            isLoaded = true;
            btn.interactable = true;
            img.enabled = true;
        }
        else
        {
            myText.SetText("");
            isLoaded = false;
            btn.interactable = false;
            img.enabled = false;
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
