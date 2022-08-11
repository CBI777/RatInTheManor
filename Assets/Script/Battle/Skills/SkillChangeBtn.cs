using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillChangeBtn : MonoBehaviour
{
    [SerializeField] private int num;
    [SerializeField] private AudioSource audSource;
    [SerializeField] private AudioClip audClip;

    public void onClickBtn()
    {
        audSource.PlayOneShot(audClip);
        gameObject.SendMessageUpwards("SkillChange", num);
    }
}
