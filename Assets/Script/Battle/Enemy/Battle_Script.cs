using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class scriptClass
{
    public bool isRight;
    public bool isPorted;
    public string line;
    public string portName;
}

[CreateAssetMenu(fileName = "Battle_Script", menuName = "ScriptableObject/Battle_Script")]
public class Battle_Script : ScriptableObject
{
    public List<ListWrapper<scriptClass>> script;
}
