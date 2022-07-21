using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenCollector : MonoBehaviour
{
    [SerializeField] private int tokenCount;

    private void OnEnable()
    {
        //turnStart¿¡ ¸ÂÃç¼­ +=
    }

    private void OnDisable()
    {
        //turnStart¿¡ ¸ÂÃç¼­ -=
    }

    




    private void Awake()
    {
        this.tokenCount = 4;
    }
}
