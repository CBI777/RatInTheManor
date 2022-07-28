using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillChangeBtn : MonoBehaviour
{
    [SerializeField] private int num;

    public void onClickBtn()
    {
        gameObject.SendMessageUpwards("SkillChange", num);
    }
}
