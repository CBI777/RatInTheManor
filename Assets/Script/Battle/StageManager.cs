using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StageManager : MonoBehaviour
{
    [SerializeField] private int stageNum;

    public static event Action<int> StageSpread;

    private void OnEnable()
    {
        TurnManager.BattleEndEvent += TurnManager_BattleEndEvent;
    }
    private void OnDisable()
    {
        TurnManager.BattleEndEvent -= TurnManager_BattleEndEvent;
    }

    private void TurnManager_BattleEndEvent()
    {
        stageNum++;
        //여기서 저장하는걸 부르고, 저장하고 action치면 바로 scene 넘어가면 됨
    }

    private void Awake()
    {
        stageNum = 1;
    }
    private void Start()
    {
        StageSpread?.Invoke(stageNum);
    }
}
