using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySupplyUIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] supply = new GameObject[5];
    [SerializeField] private GameObject[] bg = new GameObject[5];
    [SerializeField] private GameObject myText;

    private void OnEnable()
    {
        SupplyManager.SupplyChangedEvent += SupplyManager_SupplyChangedEvent;
    }

    private void OnDisable()
    {
        SupplyManager.SupplyChangedEvent -= SupplyManager_SupplyChangedEvent;
    }

    private void SupplyManager_SupplyChangedEvent(int count, Supply_Base[] arg2)
    {
        if(count == 0) { myText.SetActive(true); }
        else { myText.SetActive(false); }

        int j = 0;
        for (int i = 1; i < 6; i++, j++)
        {
            if (i <= count)
            {
                bg[j].SetActive(true);
                supply[j].GetComponent<TooltipTrigger>().enabled = true;
                supply[j].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Supply/" + arg2[i].realName);
                supply[j].GetComponent<TooltipTrigger>().header = arg2[i].supplyName;
                supply[j].GetComponent<TooltipTrigger>().content = arg2[i].description;
            }
            else
            {
                bg[j].SetActive(false);
            }
        }
    }
}
