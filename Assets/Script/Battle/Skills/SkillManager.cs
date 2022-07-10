using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SkillManager : MonoBehaviour
{
    //1. skill을 받아서 skill 들어옴 ㅅㄱ하면서 뿌려줌
    //2. 이 skill을 SlotManager랑 skillBtn이 받아먹음
    public static event Action<List<EnemySkill>, int> SkillAddedEvent;

    [SerializeField] private List<EnemySkill> skillList = new List<EnemySkill>();

    public void skillSpread()
    {
        SkillAddedEvent?.Invoke(skillList, skillList.Count);
    }

    private void Awake()
    {
        EnemySkill skill1 = new EnemySkill();
        skill1.skillName = "스킬1";
        skill1.tokens[0] = 1;
        skill1.tokens[1] = 2;
        skill1.tokens[2] = 2;
        skill1.tokens[3] = 0;
        skill1.dmgs[0] = 3;
        skill1.dmgs[1] = 2;
        skill1.dmgs[2] = 1;
        skill1.dmgs[3] = 0;
        skillList.Add(skill1);

        EnemySkill skill2 = new EnemySkill();
        skill2.skillName = "스킬2";
        skill2.tokens[0] = 3;
        skill2.tokens[1] = 0;
        skill2.tokens[2] = 0;
        skill2.tokens[3] = 1;
        skill2.dmgs[0] = 5;
        skill2.dmgs[1] = 0;
        skill2.dmgs[2] = 0;
        skill2.dmgs[3] = 1;
        skillList.Add(skill2);

        EnemySkill skill3 = new EnemySkill();
        skill3.skillName = "스킬3";
        skill3.tokens[0] = 2;
        skill3.tokens[1] = 1;
        skill3.tokens[2] = 0;
        skill3.tokens[3] = 2;
        skill3.dmgs[0] = 1;
        skill3.dmgs[1] = 5;
        skill3.dmgs[2] = 0;
        skill3.dmgs[3] = 1;
        skillList.Add(skill3);
    }

    private void Start()
    {
        skillSpread();
    }
}
