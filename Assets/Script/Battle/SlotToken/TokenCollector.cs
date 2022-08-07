using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenCollector : MonoBehaviour
{
    [SerializeField] private List<GameObject> tokens;
    [SerializeField] private int tokenCount;

    [SerializeField] private GameObject token;
    [SerializeField] private GameObject brokenToken;
    [SerializeField] private GameObject neutralToken;

    [SerializeField] private GameObject hand;

    private void OnEnable()
    {
        TurnManager.TurnStart += TurnManager_TurnStart;
        BattleDialogueProvider.betweenTurnDia += BattleDialogueProvider_betweenTurnDia;
        Quirk_Base.TokenChangeEvent += Quirk_Base_TokenChangeEvent;
        TurnEndBtn.TurnEndEvent += TurnEndBtn_TurnEndEvent;
    }

    private void OnDisable()
    {
        TurnManager.TurnStart -= TurnManager_TurnStart;
        BattleDialogueProvider.betweenTurnDia -= BattleDialogueProvider_betweenTurnDia;
        Quirk_Base.TokenChangeEvent -= Quirk_Base_TokenChangeEvent;
        TurnEndBtn.TurnEndEvent -= TurnEndBtn_TurnEndEvent;
    }

    private void BattleDialogueProvider_betweenTurnDia()
    {
        for (int i = 0; i < tokens.Count; i++)
        {
            Destroy(tokens[i]);
        }
        tokens.Clear();
    }
    private void TurnEndBtn_TurnEndEvent()
    {
        TokenDragDrop temp;
        for(int i = 0; i<tokens.Count; i++)
        {
            temp = tokens[i].GetComponent<TokenDragDrop>();
            if(temp != null) { temp.enabled = false; }
        }
    }

    private void Quirk_Base_TokenChangeEvent(int obj)
    {
        this.tokenCount += obj;
    }

    private void TurnManager_TurnStart(int obj)
    {
        for(int i = 0; i < this.tokenCount; i++)
        {
            if (i < 4) { tokens.Add(Instantiate(token, hand.transform)); }
            else { tokens.Add(Instantiate(neutralToken, hand.transform)); }
        }
        if (this.tokenCount < 4)
        {
            for(int i = this.tokenCount; i < 4; i++)
            {
                tokens.Add(Instantiate(brokenToken, hand.transform));
            }
        }
    }

    private void Awake()
    {
        this.tokens = new List<GameObject>();
        this.tokenCount = 4;
    }
}
