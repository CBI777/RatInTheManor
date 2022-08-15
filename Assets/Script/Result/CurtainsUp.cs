using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
using System.Collections;
using System;

public class CurtainsUp : MonoBehaviour
{
    private PlayerInput playerinput;

    [SerializeField] private SaveM_Result saveManager;
    [SerializeField] private RectTransform Curtain;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float resetSpeed = 4f;

    public static event Action CurtainHasBeenLifted;

    private void OnEnable()
    {
        PackComplete.SafeToGo += CurtainDown;
        SaveM_Result.firstSaveFinished += SaveM_Result_firstSaveFinished;
    }

    private void OnDisable()
    {
        PackComplete.SafeToGo -= CurtainDown;
        SaveM_Result.firstSaveFinished -= SaveM_Result_firstSaveFinished;
    }

    private void SaveM_Result_firstSaveFinished()
    {
        Curtain.DOAnchorPos(new Vector2(0, 1100f), resetSpeed);
        StartCoroutine(CurtainLift(true));
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
        playerinput.actions.FindActionMap("PlayerInput").Disable();
    }
    private void Start()
    {
        if(!saveManager.saving.isBattle)
        {
            SaveM_Result_firstSaveFinished();
        }
    }
}
