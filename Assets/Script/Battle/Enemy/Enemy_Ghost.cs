using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ghost : Enemy_Base
{
    public Enemy_Ghost()
        : base(2, "망령", "Enemy_Ghost")
    {
        EnemySkill skill1 = new EnemySkill(
            "허공 가리키기", 0, 2, 1, 2, 0, 1, 1, 1, "망령은 일그러진 손으로 어딘가를 가리킨다...");

        EnemySkill skill2 = new EnemySkill(
            "중얼거리기", 0, 4, 2, 0, 0, 3, 1, 0, "망령은 들을 수 없는 말을 중얼거린다...");

        /////////////////////////////////////

        EnemySkill skill3 = new EnemySkill(
            "허우적거리기", 2, 5, 2, 0, 2, 2, 1, 0, "망령은 뼈만 남은 손을 휘적거린다...");

        EnemySkill skill4 = new EnemySkill(
            "비명", 0, 6, 3, 0, 0, 4, 2, 0, "망령은 끔찍한 비명을 내질렀다.");

        List<EnemySkill> count1Skill = new List<EnemySkill> { skill1, skill2 };
        List<EnemySkill> count2Skill = new List<EnemySkill> { skill1, skill3, skill4 };

        this.skillList.Add(count1Skill);
        this.skillList.Add(count2Skill);
    }
}