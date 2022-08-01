using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyVariation
{
    public string realName;
    public int num;
}

[CreateAssetMenu(fileName = "StageVaration", menuName = "ScriptableObject/StageVaration")]
public class StageVariations : ScriptableObject
{
    public ListWrapper<EnemyVariation> variationList;
}
