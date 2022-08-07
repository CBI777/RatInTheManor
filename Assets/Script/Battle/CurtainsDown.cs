using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
using System.Collections;

public class CurtainsDown : MonoBehaviour
{
    private PlayerInput playerinput;

    [SerializeField] private RectTransform Curtain;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float resetSpeed = 4f;


    private void OnEnable()
    {
        TurnEndBtn.TurnEndEvent += TurnEndBtn_TurnEndEvent;
        TurnManager.TurnStart += TurnManager_TurnStart;
        TurnManager.BattleEndEvent += TurnManager_BattleEndEvent;
    }

    private void OnDisable()
    {
        TurnEndBtn.TurnEndEvent -= TurnEndBtn_TurnEndEvent;
        TurnManager.TurnStart -= TurnManager_TurnStart;
        TurnManager.BattleEndEvent -= TurnManager_BattleEndEvent;
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
