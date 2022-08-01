using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillDialogue
{
    public string dialogue;
    public string sfxName;
    public DmgType dmgType;
}

[CreateAssetMenu(fileName = "EnemySkillDia", menuName = "ScriptableObject/EnemySkillDia")]
public class EnemySkillDialogue : ScriptableObject
{
    public List<ListWrapper<SkillDialogue>> skillDia;
}
