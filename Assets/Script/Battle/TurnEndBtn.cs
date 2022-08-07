using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TurnEndBtn : MonoBehaviour
{
    [SerializeField] private bool clicked;
    [SerializeField] private TextMeshProUGUI myText;
    [SerializeField] private Button btn;
    [SerializeField] private Image img;

    private Color oriColor = new Color(250f / 255f, 164f / 255f, 131f / 255f);
    private Color selClr = new Color(233f / 255f, 88f / 255f, 43f / 255f);

    public static event Action TurnEndEvent;

    private void OnEnable()
    {
        BattleResetManager.ResetBoardEvent += buttonReset;
        TurnManager.TurnStart += TurnManager_TurnStart;
    }
    private void OnDisable()
    {
        BattleResetManager.ResetBoardEvent -= buttonReset;
        TurnManager.TurnStart -= TurnManager_TurnStart;
    }

    private void TurnManager_TurnStart(int obj)
    {
        buttonReset();
    }


    public void onClick()
    {
        if (clicked)
        {
            this.img.color = oriColor;
            this.btn.interactable = false;
            this.myText.SetText("방어 절차 진행");
            TurnEndEvent?.Invoke();
        }
        else
        {
            this.clicked = true;
            this.myText.SetText("방어 진행 확정");
            this.img.color = selClr;
        }
    }

    private void buttonReset()
    {
        this.btn.interactable = true;
        this.clicked = false;
        this.myText.SetText("방어를 진행");
        this.img.color = oriColor;
    }

    private void Start()
    {
        this.myText = this.transform.GetComponentInChildren<TextMeshProUGUI>();
        this.btn = this.transform.GetComponent<Button>();
        this.img = this.transform.GetComponent<Image>();
    }
}
