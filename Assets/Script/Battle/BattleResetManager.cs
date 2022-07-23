using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class BattleResetManager : MonoBehaviour
{
    private PlayerInput playerinput;

    [SerializeField] private RectTransform Play_ResetArea;
    [SerializeField] private RectTransform ResetArrow;

    [SerializeField] private int resetMove = 1920;
    [SerializeField] private float resetSpeed = 1f;

    [SerializeField] private Vector2 initialPlace;

    public static event Action ResetBoardEvent;

    public void ResetBoard(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            playerinput.actions.FindActionMap("PlayerInput").Disable();
            //Dotween은 코루틴처럼 작동함.
            ResetArrow.DORotate(new Vector3(0, 0, 360), 3f, RotateMode.FastBeyond360);
            Play_ResetArea.DOAnchorPos(new Vector2(0, -180f), resetSpeed);
            StartCoroutine(ResetCoverBoard());
        }
    }

    private IEnumerator ResetCoverBoard()
    {
        yield return new WaitForSeconds(resetSpeed);
        ResetBoardEvent?.Invoke();
        yield return new WaitForSeconds(resetSpeed);
        Play_ResetArea.DOAnchorPos(new Vector2(-resetMove, -180f), resetSpeed);
        yield return new WaitForSeconds(resetSpeed);
        Play_ResetArea.position = initialPlace;
        playerinput.actions.FindActionMap("PlayerInput").Enable();
    }

    private void Start()
    {
        this.initialPlace = this.Play_ResetArea.position;
        playerinput = GetComponent<PlayerInput>();
    }
}
