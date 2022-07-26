using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenCollector : MonoBehaviour
{
    [SerializeField] private int tokenCount;

    [SerializeField] private GameObject token;
    [SerializeField] private GameObject brokenToken;
    [SerializeField] private GameObject neutralToken;

    [SerializeField] private GameObject hand;

    private void OnEnable()
    {
        TurnManager.TurnStart += TurnManager_TurnStart;
        //turnend시에 모든 token을 정리
        Quirk_Base.TokenChangeEvent += Quirk_Base_TokenChangeEvent;
    }

    private void OnDisable()
    {
        TurnManager.TurnStart -= TurnManager_TurnStart;
        Quirk_Base.TokenChangeEvent -= Quirk_Base_TokenChangeEvent;
    }

    private void Quirk_Base_TokenChangeEvent(int obj)
    {
        this.tokenCount += obj;
    }

    private void TurnManager_TurnStart(int obj)
    {
        for(int i = 0; i < this.tokenCount; i++)
        {
            if (i < 4) { Instantiate(token, hand.transform); }
            else { Instantiate(neutralToken, hand.transform); }
        }
        if (this.tokenCount < 4)
        {
            for(int i = this.tokenCount; i < 4; i++)
            {
                Instantiate(brokenToken, hand.transform);
            }
        }
    }

    private void Awake()
    {
        this.tokenCount = 4;
    }
}
