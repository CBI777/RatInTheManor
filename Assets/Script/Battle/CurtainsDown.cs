using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
using System.Collections;
using System;

public class CurtainsDown : MonoBehaviour
{
    private PlayerInput playerinput;

    [SerializeField] private RectTransform Curtain;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float resetSpeed = 4f;

    private void OnEnable()
    {
        SkillManager.enemyDecidedEvent += SkillManager_enemyDecidedEvent;
        TurnEndBtn.TurnEndEvent += TurnEndBtn_TurnEndEvent;
        TurnManager.TurnStart += TurnManager_TurnStart;
        TurnManager.BattleEndEvent += TurnManager_BattleEndEvent;
    }

    private void OnDisable()
    {
        SkillManager.enemyDecidedEvent -= SkillManager_enemyDecidedEvent;
        TurnEndBtn.TurnEndEvent -= TurnEndBtn_TurnEndEvent;
        TurnManager.TurnStart -= TurnManager_TurnStart;
        TurnManager.BattleEndEvent -= TurnManager_BattleEndEvent;
    }

    private void SkillManager_enemyDecidedEvent(string arg1, string arg2)
    {
        playerinput.actions.FindActionMap("PlayerInput").Disable();
        Curtain.DOAnchorPos(new Vector2(0, 1100f), resetSpeed);
        StartCoroutine(PlaySound());
    }

    private void TurnManager_TurnStart(int obj)
    {
        playerinput.actions.FindActionMap("PlayerInput").Enable();
    }

    private void TurnEndBtn_TurnEndEvent()
    {
        playerinput.actions.FindActionMap("PlayerInput").Disable();
    }

    private void TurnManager_BattleEndEvent()
    {
        playerinput.actions.FindActionMap("PlayerInput").Disable();
        Curtain.DOAnchorPos(new Vector2(0, -120f), resetSpeed);
        StartCoroutine(PlaySound());
    }
    private IEnumerator PlaySound()
    {
        this.audioSource.Play();
        yield return new WaitForSeconds(resetSpeed);
        this.audioSource.Stop();
    }

    private void Awake()
    {
        playerinput = GetComponent<PlayerInput>();
    }
}
