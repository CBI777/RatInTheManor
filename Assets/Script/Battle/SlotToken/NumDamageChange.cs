using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class NumDamageChange : MonoBehaviour
{
    [SerializeField] private int typeNum;
    [SerializeField] private TextMeshProUGUI myText;

    private void OnEnable()
    {
        myText = this.transform.GetComponent<TextMeshProUGUI>();
        SkillBtn.SkillChanged += SkillBtn_SkillChanged;
        SkillManager.SkillAddedEvent += SkillManager_SkillAddedEvent;
    }

    private void OnDisable()
    {
        SkillBtn.SkillChanged -= SkillBtn_SkillChanged;
        SkillManager.SkillAddedEvent -= SkillManager_SkillAddedEvent;
    }

    private void SkillManager_SkillAddedEvent(List<EnemySkill> arg1, int arg2)
    {
        myText.SetText(arg1[0].dmgs[typeNum].ToString());
    }

    private void SkillBtn_SkillChanged(EnemySkill arg1, int arg2)
    {
        myText.SetText(arg1.dmgs[typeNum].ToString());
    }
}
