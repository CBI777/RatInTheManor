using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill
{
    public string skillName;

    public int[] dmgs = new int[4];
    public int[] tokens = new int[4];

    public string Description;

    public EnemySkill(string skillName, int pdmg, int fdmg, int admg, int ddmg, int ptokens, int ftokens, int atokens, int dtokens, string description)
    {
        this.skillName = skillName;
        Description = description;

        tokens[0] = ptokens;
        tokens[1] = ftokens;
        tokens[2] = atokens;
        tokens[3] = dtokens;
        dmgs[0] = pdmg;
        dmgs[1] = fdmg;
        dmgs[2] = admg;
        dmgs[3] = ddmg;
        //여기 효과음 넣어야됨
    }
}
