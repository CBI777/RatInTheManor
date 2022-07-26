using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryEquipmentUIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] bg = new GameObject[3];
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
        for (int i = 0; i < arg2; i++)
        {
            if (i == arg1)
            {
                equipcheck[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/UI/SelectedEdge");
            }
            else
            {
                equipcheck[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/UI/DeselectedEdge");
            }
        }
    }

    private void EquipmentManager_EquipChangedEvent(int arg1, int arg2, Equipment[] arg3)
    {
        for (int i = 0; i < 3; i++)
        {
            if (i < arg2)
            {
                bg[i].SetActive(true);
                equips[i].GetComponent<TooltipTrigger>().enabled = true;
                equips[i].GetComponent<TooltipTrigger>().header = arg3[i].equipName;
                equips[i].GetComponent<TooltipTrigger>().content = arg3[i].description;
                equips[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Equipment/" + arg3[i].realName);

                if (i == arg1) { equipcheck[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/UI/SelectedEdge"); }
                else { equipcheck[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/UI/DeselectedEdge"); }
            }
            else
            {
                bg[i].SetActive(false);
            }
        }
    }
}
