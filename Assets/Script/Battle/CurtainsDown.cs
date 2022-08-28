using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
using System.Collections;
using System;
using UnityEngine.UI;

public class CurtainsDown : MonoBehaviour
{
    private PlayerInput playerinput;

    [SerializeField] private RectTransform Curtain;
    [SerializeField] GameObject blackOut;

    [SerializeField] private AudioSource audioSource;
    private float resetSpeed = 4f;

    public static event Action CurtainCall;
    public static event Action CurtainDownComplete;
    public static event Action TheEnd;

    private void OnEnable()
    {
        SkillManager.enemyDecidedEvent += SkillManager_enemyDecidedEvent;
        TurnEndBtn.TurnEndEvent += TurnEndBtn_TurnEndEvent;
        TurnManager.TurnStart += TurnManager_TurnStart;
        TurnManager.BattleEndEvent += TurnManager_BattleEndEvent;
        BattleDialogueProvider.startFromDialogue += BattleDialogueProvider_startFromDialogue;
        BattleDialogueProvider.YouAreMad += BattleDialogueProvider_YouAreMad;
    }

    private void OnDisable()
    {
        SkillManager.enemyDecidedEvent -= SkillManager_enemyDecidedEvent;
        TurnEndBtn.TurnEndEvent -= TurnEndBtn_TurnEndEvent;
        TurnManager.TurnStart -= TurnManager_TurnStart;
        TurnManager.BattleEndEvent -= TurnManager_BattleEndEvent;
        BattleDialogueProvider.startFromDialogue -= BattleDialogueProvider_startFromDialogue;
        BattleDialogueProvider.YouAreMad -= BattleDialogueProvider_YouAreMad;
    }

    private void BattleDialogueProvider_YouAreMad()
    {
        playerinput.actions.FindActionMap("PlayerInput").Disable();
        Curtain.DOAnchorPos(new Vector2(0, -120f), resetSpeed);
        StartCoroutine(BackGroundFade());
        StartCoroutine(PlayTheEnd());
    }

    private void BattleDialogueProvider_startFromDialogue()
    {
        Curtain.DOAnchorPos(new Vector2(0, 1100f), resetSpeed);
        StartCoroutine(DialogueCall());
        StartCoroutine(BackGroundLight());
    }

    private void SkillManager_enemyDecidedEvent(string arg1, string arg2, string arg3)
    {
        Curtain.DOAnchorPos(new Vector2(0, 1100f), resetSpeed);
        StartCoroutine(DialogueCall());
        StartCoroutine(BackGroundLight());
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
        StartCoroutine(BackGroundFade());
    }
    private IEnumerator PlaySound()
    {
        //enemyDecided에서 왔으면 esc빼고 잠금?
        this.audioSource.Play();
        yield return new WaitForSeconds(resetSpeed);
        this.audioSource.Stop();
        CurtainDownComplete?.Invoke();
    }

    private IEnumerator PlayTheEnd()
    {
        //enemyDecided에서 왔으면 esc빼고 잠금?
        this.audioSource.Play();
        yield return new WaitForSeconds(resetSpeed);
        this.audioSource.Stop();
        TheEnd?.Invoke();
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

    IEnumerator BackGroundFade()
    {
        blackOut.gameObject.SetActive(true);
        Color c = new Color(0f, 0f, 0f, 0f);
        for (int i = 0; i <= 400; i++)
        {
            c.a = i / 400f;
            blackOut.GetComponent<Image>().color = c;
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator BackGroundLight()
    {
        Color c = new Color(0f, 0f, 0f, 0f);
        for (int i = 400; i >= 0; i--)
        {
            c.a = i / 400f;
            blackOut.GetComponent<Image>().color = c;
            yield return new WaitForSeconds(0.01f);
        }
        blackOut.gameObject.SetActive(false);
    }

    private void Awake()
    {
        playerinput = GetComponent<PlayerInput>();
        playerinput.actions.FindActionMap("PlayerInput").Disable();
    }
}
