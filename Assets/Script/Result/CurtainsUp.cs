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

    private IEnumerator CurtainLift()
    {
        this.audioSource.Play();
        yield return new WaitForSeconds(resetSpeed);
        this.audioSource.Stop();
        playerinput.actions.FindActionMap("PlayerInput").Enable();
        CurtainHasBeenLifted?.Invoke();
    }

    private void Awake()
    {
        playerinput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        playerinput.actions.FindActionMap("PlayerInput").Disable();
        Curtain.DOAnchorPos(new Vector2(0, 1100f), resetSpeed);
        StartCoroutine(CurtainLift());
    }
}
