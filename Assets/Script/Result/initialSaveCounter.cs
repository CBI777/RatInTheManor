using UnityEngine;
using System;

public class initialSaveCounter : MonoBehaviour
{
    [SerializeField] private SaveM_Result saveManager;

    private bool equipComplete = false;
    private bool supplyComplete = false;
    private bool HalluComplete = false;

    private void OnEnable()
    {
        Reward_Equip.presentComplete += Reward_Equip_presentComplete;
        Reward_Supply.presentComplete += Reward_Supply_presentComplete;
        Reward_Hallucination.presentComplete += Reward_Hallucination_presentComplete;
    }

    private void OnDisable()
    {
        Reward_Equip.presentComplete -= Reward_Equip_presentComplete;
        Reward_Supply.presentComplete -= Reward_Supply_presentComplete;
        Reward_Hallucination.presentComplete -= Reward_Hallucination_presentComplete;
    }

    public static event Action InitialSavePlz;

    private void Reward_Hallucination_presentComplete()
    {
        this.HalluComplete = true;
    }
    private void Reward_Supply_presentComplete()
    {
        this.supplyComplete = true;
    }
    private void Reward_Equip_presentComplete()
    {
        this.equipComplete = true;
    }

    private void Awake()
    {
        //isBattle�̸� battle���� �� �Ѿ�Դٴ� ���̱� ������,
        //count�� �����ؾ��Ѵ�.
        //�ƴϸ� count�� �ʿ䰡 ����.
        if(!saveManager.saving.isBattle)
        {
            this.enabled = false;
        }
    }

    private void Update()
    {
        if(HalluComplete && supplyComplete && equipComplete)
        {
            InitialSavePlz?.Invoke();
            enabled = false;
        }
    }
}
