using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ListWrapper
{
    public List<string> myList;

    public string this[int key]
    {
        get
        {
            return myList[key];
        }
        set
        {
            myList[key] = value;
        }
    }
}
