using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    private bool up = false;

    private PlayerInput playerinput;

    [SerializeField] private RectTransform StatusUI_Area;
    [SerializeField] private float resetSpeed = 1f;

    [SerializeField] private Vector2 initialPlace;

    private void OnEnable()
    {
        BattleResetManager.ResetBoardEvent += BattleResetManager_ResetBoardEvent;
    }

    private void OnDisable()
    {
        BattleResetManager.ResetBoardEvent -= BattleResetManager_ResetBoardEvent;
    }

    private void BattleResetManager_ResetBoardEvent()
    {
        StatusUI_Area.position = initialPlace;
        this.up = false;
    }


    public void MoveBoard(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if(!up)
            {
                StartCoroutine(StatusUp());
            }
            else
            {
                StartCoroutine(StatusDown());
            }
        }
    }

    private IEnumerator StatusUp()
    {
        playerinput.actions.FindActionMap("PlayerInput").Disable();
        StatusUI_Area.DOAnchorPos(new Vector2(0, -180f), resetSpeed);
        yield return new WaitForSeconds(resetSpeed);
        this.up = true;
        playerinput.actions.FindActionMap("PlayerInput").Enable();
    }
    private IEnumerator StatusDown()
    {
        playerinput.actions.FindActionMap("PlayerInput").Disable();
        StatusUI_Area.DOAnchorPos(new Vector2(1920, -180f), resetSpeed);
        yield return new WaitForSeconds(resetSpeed);
        this.up = false;
        playerinput.actions.FindActionMap("PlayerInput").Enable();
    }

    private void Start()
    {
        this.initialPlace = this.StatusUI_Area.position;
        playerinput = GetComponent<PlayerInput>();
    }
}
