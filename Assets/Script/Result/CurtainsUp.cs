using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
using System.Collections;
using System;
using UnityEngine.UI;

public class CurtainsUp : MonoBehaviour
{
    private PlayerInput playerinput;

    [SerializeField] private SaveM_Result saveManager;
    [SerializeField] private RectTransform Curtain;
    [SerializeField] GameObject blackOut;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float resetSpeed = 4f;

    public static event Action CurtainHasBeenLifted;
    public static event Action<bool> CurtainDownComplete;

    private void OnEnable()
    {
        PackComplete.SafeToGo += CurtainDown;
        SaveM_Result.firstSaveFinished += SaveM_Result_firstSaveFinished;
        Result_StatusManager.MadnessMaxEvent += Result_StatusManager_MadnessMaxEvent; ;
    }

    private void OnDisable()
    {
        PackComplete.SafeToGo -= CurtainDown;
        SaveM_Result.firstSaveFinished -= SaveM_Result_firstSaveFinished;
        Result_StatusManager.MadnessMaxEvent -= Result_StatusManager_MadnessMaxEvent;
    }

    private void Result_StatusManager_MadnessMaxEvent()
    {
        playerinput.actions.FindActionMap("PlayerInput").Disable();
        Curtain.DOAnchorPos(new Vector2(0, -120f), resetSpeed);
        StartCoroutine(CurtainLift(false, true));
        StartCoroutine(BackGroundFade());
    }

    private void SaveM_Result_firstSaveFinished()
    {
        Curtain.DOAnchorPos(new Vector2(0, 1100f), resetSpeed);
        StartCoroutine(CurtainLift(true, false));
        StartCoroutine(BackGroundLight());
    }

    private IEnumerator CurtainLift(bool isUp, bool isMad)
    {
        this.audioSource.Play();
        yield return new WaitForSeconds(resetSpeed);
        this.audioSource.Stop();
        if(isUp)
        {
            CurtainHasBeenLifted?.Invoke();
            playerinput.actions.FindActionMap("PlayerInput").Enable();
        }
        else
        {
            if(isMad)
            {
                CurtainDownComplete?.Invoke(true);
            }
            else
            {
                CurtainDownComplete?.Invoke(false);
            }
        }
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

    private void CurtainDown()
    {
        playerinput.actions.FindActionMap("PlayerInput").Disable();
        Curtain.DOAnchorPos(new Vector2(0, -120f), resetSpeed);
        StartCoroutine(CurtainLift(false, false));
        StartCoroutine(BackGroundFade());
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
