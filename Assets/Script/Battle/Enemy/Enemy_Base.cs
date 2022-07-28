using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy_Base", menuName = "ScriptableObject/Enemy_Base")]
public class Enemy_Base : ScriptableObject
{
    public int turnCount;
    public string enemyName;
    public string realName;

    public List<ListWrapper<EnemySkill>> skillList = new List<ListWrapper<EnemySkill>>();
}
