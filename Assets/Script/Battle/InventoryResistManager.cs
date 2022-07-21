using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryResistManager : MonoBehaviour
{
    [SerializeField] private GameObject[] resists = new GameObject[4];

    private void OnEnable()
    {
        Player.InventoryResistChangedEvent += Player_InventoryResistChangedEvent;
    }

    private void OnDisable()
    {
        Player.InventoryResistChangedEvent -= Player_InventoryResistChangedEvent;
    }

    private void Player_InventoryResistChangedEvent(int[] obj)
    {
        for (int i = 0; i < 4; i++)
        {
            resists[i].GetComponent<TextMeshProUGUI>().SetText(obj[i].ToString());
        }
    }
}
