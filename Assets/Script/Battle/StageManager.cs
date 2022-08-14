using UnityEngine;
using System;

public class StageManager : MonoBehaviour
{
    [SerializeField] private int stageNum;
    [SerializeField] private SaveM_Battle saveManager;
    private bool isLoaded;

    public int getStageNum() { return stageNum; }

    public static event Action<int> StageSpread;

    private void Awake()
    {
        //save되어있는 것이 battle 상황에서 저장된 것이 아니라면,
        if(!saveManager.saving.isBattle)
        {
            this.stageNum = (saveManager.saving.stageNum + 1);
            isLoaded = false;
        }
        else
        {
            this.stageNum = saveManager.saving.stageNum;
            isLoaded = true;
        }
    }
    private void Start()
    {
        //맨처음 들어왔을 경우
        if(!isLoaded)
        {
            StageSpread?.Invoke(stageNum);
        }
    }
}
