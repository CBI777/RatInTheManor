using UnityEngine;

public class EquipBtn_ResultInventory : MonoBehaviour
{
    [SerializeField] int num;

    public void onClick()
    {
        SendMessageUpwards("CurEquipChange", num);
    }
}
