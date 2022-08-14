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
        //save�Ǿ��ִ� ���� battle ��Ȳ���� ����� ���� �ƴ϶��,
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
        //��ó�� ������ ���
        if(!isLoaded)
        {
            StageSpread?.Invoke(stageNum);
        }
    }
}
