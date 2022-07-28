using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ListWrapper<T>
{
    public List<T> myList;

    public T this[int key]
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

    public int Count()
    {
        return myList.Count;
    }
}
