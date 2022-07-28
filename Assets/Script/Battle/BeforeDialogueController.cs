using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using UnityEngine.InputSystem;

public class BeforeDialogueController : MonoBehaviour
{
    private PlayerInput playerinput;

    [SerializeField] private RectTransform Play_CoverArea;

    private AudioSource audioSource;
    [SerializeField] private AudioClip clip;

    [SerializeField] private int resetMove = 1920;
    [SerializeField] private float resetSpeed = 1f;

    private void OnEnable()
    {
        SkillManager.enemyDecidedEvent += SkillManager_enemyDecidedEvent;
        BattleDialogueProvider.PrevDialogueDone += BattleDialogueProvider_PrevDialogueDone;
    }

    private void OnDisable()
    {
        SkillManager.enemyDecidedEvent -= SkillManager_enemyDecidedEvent;
        BattleDialogueProvider.PrevDialogueDone -= BattleDialogueProvider_PrevDialogueDone;
    }

    private void BattleDialogueProvider_PrevDialogueDone()
    {
        this.audioSource.PlayOneShot(clip);
        Play_CoverArea.DOAnchorPos(new Vector2(-resetMove, -180f), resetSpeed);
        playerinput.actions.FindActionMap("PlayerInput").Enable();
    }

    private void SkillManager_enemyDecidedEvent(string obj)
    {
        playerinput.actions.FindActionMap("PlayerInput").Disable();
    }


    private void Awake()
    {
        this.audioSource = this.transform.GetComponent<AudioSource>();
    }

    private void Start()
    {
        playerinput = GetComponent<PlayerInput>();
    }
}
