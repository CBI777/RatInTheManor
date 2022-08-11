using UnityEngine;
using TMPro;

public class ResistUI_ResultInventory : MonoBehaviour
{
    [SerializeField] private GameObject[] resists = new GameObject[4];

    private void OnEnable()
    {
        Player_Result.InventoryResistChangedEvent += Player_Result_InventoryResistChangedEvent;
    }
    private void OnDisable()
    {
        Player_Result.InventoryResistChangedEvent -= Player_Result_InventoryResistChangedEvent;
    }

    private void Player_Result_InventoryResistChangedEvent(int[] obj)
    {
        for (int i = 0; i < 4; i++)
        {
            resists[i].GetComponent<TextMeshProUGUI>().SetText(obj[i].ToString());
        }
    }
}
