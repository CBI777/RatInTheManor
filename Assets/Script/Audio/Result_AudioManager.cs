using System;
using System.Collections;
using UnityEngine;

public class Result_AudioManager : MonoBehaviour
{
    [SerializeField] private SaveM_Result saveManager;
    private AudioSource audioSource;
    private void Awake()
    {
        this.audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        PackComplete.SafeToGo += VolumeDown;
        SaveM_Result.firstSaveFinished += SaveM_Result_firstSaveFinished;
    }

    private void OnDisable()
    {
        PackComplete.SafeToGo -= VolumeDown;
        SaveM_Result.firstSaveFinished -= SaveM_Result_firstSaveFinished;
    }

    private void SaveM_Result_firstSaveFinished()
    {
        StartCoroutine(VolUpToStart(0.5f));
    }

    private void VolumeDown()
    {
        StartCoroutine(VolDownToEnd());
    }

    IEnumerator VolUpToStart(float b)
    {
        this.audioSource.Play();
        float v = 0f;

        do
        {
            v += 0.00125f;
            this.audioSource.volume = v;
            yield return new WaitForSeconds(0.01f);

        } while (v <= b);
    }
    IEnumerator VolDownToEnd()
    {
        float v = this.audioSource.volume;

        do
        {
            v -= 0.00125f;
            this.audioSource.volume = v;
            yield return new WaitForSeconds(0.01f);

        } while (v >= 0);

        this.audioSource.Stop();
    }

    private void Start()
    {
        if (!saveManager.saving.isBattle)
        {
            SaveM_Result_firstSaveFinished();
        }
    }
}
