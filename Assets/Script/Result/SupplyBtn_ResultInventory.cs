using UnityEngine;

public class SupplyBtn_ResultInventory : MonoBehaviour
{
    [SerializeField]int num;

    public void onClick()
    {
        SendMessageUpwards("UseSupply", num);
    }
}
