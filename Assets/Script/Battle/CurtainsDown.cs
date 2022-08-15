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

    public static event Action CurtainCall;

    private void OnEnable()
    {
        SkillManager.enemyDecidedEvent += SkillManager_enemyDecidedEvent;
        TurnEndBtn.TurnEndEvent += TurnEndBtn_TurnEndEvent;
        TurnManager.TurnStart += TurnManager_TurnStart;
        TurnManager.BattleEndEvent += TurnManager_BattleEndEvent;
        BattleDialogueProvider.startFromDialogue += BattleDialogueProvider_startFromDialogue;
    }

    private void OnDisable()
    {
        SkillManager.enemyDecidedEvent -= SkillManager_enemyDecidedEvent;
        TurnEndBtn.TurnEndEvent -= TurnEndBtn_TurnEndEvent;
        TurnManager.TurnStart -= TurnManager_TurnStart;
        TurnManager.BattleEndEvent -= TurnManager_BattleEndEvent;
        BattleDialogueProvider.startFromDialogue -= BattleDialogueProvider_startFromDialogue;
    }

    private void BattleDialogueProvider_startFromDialogue()
    {
        Curtain.DOAnchorPos(new Vector2(0, 1100f), resetSpeed);
        StartCoroutine(DialogueCall());
    }

    private void SkillManager_enemyDecidedEvent(string arg1, string arg2)
    {
        Curtain.DOAnchorPos(new Vector2(0, 1100f), resetSpeed);
        StartCoroutine(DialogueCall());
    }

    private void TurnManager_TurnStart(int obj)
    {
        playerinput.actions.FindActionMap("PlayerInput").Enable();
    }

    private void TurnEndBtn_TurnEndEvent()
    {
        //esc빼고 잠금?
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
        //enemyDecided에서 왔으면 esc빼고 잠금?
        this.audioSource.Play();
        yield return new WaitForSeconds(resetSpeed);
        this.audioSource.Stop();
    }

    private IEnumerator DialogueCall()
    {
        //enemyDecided에서 왔으면 esc빼고 잠금?
        this.audioSource.Play();
        yield return new WaitForSeconds(resetSpeed);
        this.audioSource.Stop();
        yield return new WaitForSeconds(0.3f);
        CurtainCall?.Invoke();
    }

    private void Awake()
    {
        playerinput = GetComponent<PlayerInput>();
        playerinput.actions.FindActionMap("PlayerInput").Disable();
    }
}
