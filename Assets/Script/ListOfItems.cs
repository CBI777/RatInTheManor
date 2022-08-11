using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ListOfItems", menuName = "ScriptableObject/List_Of_Items")]
public class ListOfItems : ScriptableObject
{
    public List<string> items;
}
