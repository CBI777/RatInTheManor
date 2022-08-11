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
        //���⼭ �����ϴ°� �θ���, �����ϰ� actionġ�� �ٷ� scene �Ѿ�� ��
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
