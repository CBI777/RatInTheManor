using UnityEngine;
using System;

public class StoryButton : MonoBehaviour
{
    [SerializeField] private int num;

    public static event Action<int> OnCharaDecision;

    public void onClick()
    {
        OnCharaDecision?.Invoke(this.num);
    }
}
