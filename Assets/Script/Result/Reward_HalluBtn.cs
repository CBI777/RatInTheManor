using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward_HalluBtn : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip clips;
    [SerializeField] private int num;

    public void onClick()
    {
        this.audioSource.PlayOneShot(clips);
        SendMessageUpwards("RewardHallu_Onclick", num);
    }
    private void Start()
    {
        this.audioSource = transform.GetComponent<AudioSource>();
    }
}
