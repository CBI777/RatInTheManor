using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
using System.Collections;
using System;

public class CurtainsUp : MonoBehaviour
{
    private PlayerInput playerinput;

    [SerializeField] private RectTransform Curtain;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float resetSpeed = 4f;

    public static event Action CurtainHasBeenLifted;

    private void OnEnable()
    {
        PackComplete.CompletePressed += CurtainDown;
    }
    private void OnDisable()
    {
        PackComplete.CompletePressed -= CurtainDown;
    }

    private IEnumerator CurtainLift(bool isUp)
    {
        this.audioSource.Play();
        yield return new WaitForSeconds(resetSpeed);
        this.audioSource.Stop();
        if(isUp)
        {
            CurtainHasBeenLifted?.Invoke();
            playerinput.actions.FindActionMap("PlayerInput").Enable();
        }
    }

    private void CurtainDown()
    {
        playerinput.actions.FindActionMap("PlayerInput").Disable();
        Curtain.DOAnchorPos(new Vector2(0, -120f), resetSpeed);
        StartCoroutine(CurtainLift(false));
    }

    private void Awake()
    {
        playerinput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        //start말고 ???이 신호를 보내주면 그 때 시작
        playerinput.actions.FindActionMap("PlayerInput").Disable();
        Curtain.DOAnchorPos(new Vector2(0, 1100f), resetSpeed);
        StartCoroutine(CurtainLift(true));
    }
}
