using UnityEngine;

public class EquipBtn_ResultInventory : MonoBehaviour
{
    [SerializeField] private int num;
    private bool equipChange = true;
    private AudioSource audioSource;
    [SerializeField] private AudioClip chngClip;

    private void OnEnable()
    {
        Reward_Equip.EquipObtainClicked += Reward_Equip_EquipObtainClicked;
        Reward_Equip.EquipCancelClicked += Reward_Equip_EquipCancelClicked;
        Equipment_ResultInventory.EquipObtainCompleteFromInventory += Reward_Equip_EquipCancelClicked;
    }

    private void OnDisable()
    {
        Reward_Equip.EquipObtainClicked -= Reward_Equip_EquipObtainClicked;
        Reward_Equip.EquipCancelClicked -= Reward_Equip_EquipCancelClicked;
        Equipment_ResultInventory.EquipObtainCompleteFromInventory -= Reward_Equip_EquipCancelClicked;
    }

    private void Reward_Equip_EquipCancelClicked()
    {
        this.equipChange = true;
    }

    private void Reward_Equip_EquipObtainClicked(string obj)
    {
        this.equipChange = false;
    }
    public void onClick()
    {
        this.audioSource.PlayOneShot(chngClip);
        if (equipChange)
        {
            SendMessageUpwards("CurEquipChange", num);
        }
        else
        {
            SendMessageUpwards("EquipmentObtain", num);
        }
    }
    private void Start()
    {
        this.audioSource = transform.GetComponent<AudioSource>();
    }
}
