using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryEquipmentUIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] equips = new GameObject[3];
    [SerializeField] private GameObject[] equipcheck = new GameObject[3];

    private void OnEnable()
    {
        EquipmentManager.CurEquipChanged += EquipmentManager_CurEquipChanged;
        EquipmentManager.EquipChangedEvent += EquipmentManager_EquipChangedEvent;
    }

    private void OnDisable()
    {
        EquipmentManager.CurEquipChanged -= EquipmentManager_CurEquipChanged;
        EquipmentManager.EquipChangedEvent -= EquipmentManager_EquipChangedEvent;
    }

    private void EquipmentManager_CurEquipChanged(int arg1, int arg2)
    {
        Debug.Log("dfadf");
        for (int i = 0; i < arg2; i++)
        {
            if (i == arg1) { equipcheck[i].SetActive(true); }
            else { equipcheck[i].SetActive(false); }
        }
    }

    private void EquipmentManager_EquipChangedEvent(int arg1, int arg2, Equipment[] arg3)
    {
        for (int i = 0; i < 3; i++)
        {
            if (i < arg2)
            {
                equips[i].SetActive(true);
                //이미지
                equips[i].GetComponent<TooltipTrigger>().enabled = true;
                equips[i].GetComponent<TooltipTrigger>().header = arg3[i].equipName;
                equips[i].GetComponent<TooltipTrigger>().content = arg3[i].description;
                if(i == arg1) { equipcheck[i].SetActive(true); }
                else { equipcheck[i].SetActive(false); }
            }
            else
            {
                equips[i].SetActive(false);
                equipcheck[i].SetActive(false);
            }
        }
    }
    //가능하면 클릭하는 버튼을 image에 달아야...
}
