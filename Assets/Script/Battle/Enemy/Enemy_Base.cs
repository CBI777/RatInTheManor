using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Base
{
    public int turnCount;
    public string enemyName;
    public string realName;

    public List<List<EnemySkill>> skillList = new List<List<EnemySkill>>();

    public Enemy_Base(int turnCount, string enemyName, string realName)
    {
        this.turnCount = turnCount;
        this.enemyName = enemyName;
        this.realName = realName;
    }
}
