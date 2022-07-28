using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySkill
{
    public string skillName;

    public int[] dmgs = new int[4];
    public int[] tokens = new int[4];

    public string Description;
    //여기 효과음 넣어야됨
}
