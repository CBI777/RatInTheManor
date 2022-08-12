using UnityEngine;

public class SupplyBtn_ResultInventory : MonoBehaviour
{
    [SerializeField]int num;
    private bool supplyUse = true;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip chngClip;

    private void OnEnable()
    {
        Reward_Supply.SupplyObtainClicked += Reward_Supply_SupplyObtainClicked;
        Reward_Supply.SupplyCancelClicked += Reward_Supply_SupplyCancelClicked;
        Supply_ResultInventory.SupplyObtainCompleteFromInventory += Reward_Supply_SupplyCancelClicked;
    }

    private void OnDisable()
    {
        Reward_Supply.SupplyObtainClicked -= Reward_Supply_SupplyObtainClicked;
        Reward_Supply.SupplyCancelClicked -= Reward_Supply_SupplyCancelClicked;
        Supply_ResultInventory.SupplyObtainCompleteFromInventory -= Reward_Supply_SupplyCancelClicked;
    }

    private void Reward_Supply_SupplyCancelClicked()
    {
        supplyUse = true;
    }

    private void Reward_Supply_SupplyObtainClicked(string obj)
    {
        supplyUse = false;
    }

    public void onClick()
    {
        this.audioSource.PlayOneShot(chngClip);
        if (supplyUse)
        {
            SendMessageUpwards("UseSupply", num);
        }
        else
        {
            SendMessageUpwards("SupplyObtain", num);
        }
    }
}
