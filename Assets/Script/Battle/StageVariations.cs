using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVariation
{
    public string realName;
    public int num;

    public EnemyVariation(string realName, int num)
    {
        this.realName = realName;
        this.num = num;
    }
}

public class StageVariations
{
    public Dictionary<int, List<EnemyVariation>> variations = new Dictionary<int, List<EnemyVariation>>();

    public StageVariations()
    {
        List<EnemyVariation> stg1 = new List<EnemyVariation> { new EnemyVariation("Enemy_Ghost", 100) };
        variations.Add(1, stg1);

    }
}
