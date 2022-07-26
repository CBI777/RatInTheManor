using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StageManager : MonoBehaviour
{
    [SerializeField] private int stageNum;

    public static event Action<int> StageSpread;

    private void Awake()
    {
        stageNum = 1;
    }

    private void Start()
    {
        StageSpread?.Invoke(stageNum);
    }
}
