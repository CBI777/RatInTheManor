using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SupplyBtn : MonoBehaviour
{
    //false가 왼쪽, true가 오른쪽
    [SerializeField] private bool isRight;
    public static event Action<bool> SupplyBtnPressed;

    public void btnPress()
    {
        SupplyBtnPressed?.Invoke(isRight);
    }
}