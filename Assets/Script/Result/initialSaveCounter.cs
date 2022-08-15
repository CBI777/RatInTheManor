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
        //isBattle이면 battle에서 갓 넘어왔다는 뜻이기 때문에,
        //count를 시작해야한다.
        //아니면 count가 필요가 없음.
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
