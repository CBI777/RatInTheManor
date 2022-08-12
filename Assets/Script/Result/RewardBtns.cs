using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardBtns : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip clips;
    [SerializeField] private string funcName;

    public void onClick()
    {
        this.audioSource.PlayOneShot(clips);
        SendMessageUpwards(funcName);
    }
    private void Start()
    {
        this.audioSource = transform.GetComponent<AudioSource>();
    }
}
