using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySupplyUIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] supply = new GameObject[5];
    [SerializeField] private GameObject[] btn = new GameObject[5];

    private void OnEnable()
    {
        SupplyManager.SupplyChangedEvent += SupplyManager_SupplyChangedEvent;
        //turnstart시에 모든 버튼을 다 꺼야됨.
    }

    private void OnDisable()
    {
        SupplyManager.SupplyChangedEvent -= SupplyManager_SupplyChangedEvent;
    }

    private void SupplyManager_SupplyChangedEvent(int arg1, Supply_Base[] arg2)
    {
        int j = 0;
        for (int i = 1; i < 6; i++, j++)
        {
            if (i < arg1)
            {
                supply[j].SetActive(true);
                supply[j].GetComponent<TooltipTrigger>().enabled = true;
                //이미지 설정
                supply[j].GetComponent<TooltipTrigger>().header = arg2[i].supplyName;
                supply[j].GetComponent<TooltipTrigger>().content = arg2[i].description;

                //버튼 관련
                btn[j].SetActive(true);
            }
            else
            {
                supply[j].SetActive(false);
                btn[j].SetActive(false);
            }
        }
    }
}
