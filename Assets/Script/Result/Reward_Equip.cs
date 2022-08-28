using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Reward_Equip : MonoBehaviour
{
    [SerializeField] private SaveM_Result saveManager;

    [SerializeField] private GameObject equipImage;
    [SerializeField] private GameObject check;
    [SerializeField] private GameObject btn;

    //false�� ȹ��, true�� ���
    private bool situ = false;
    //����� �Ϸ� �ߴ°�?
    private bool obtained = false;
    //�� ������ �����ϴ°�?
    private bool trouble = false;

    [SerializeField] private ListOfItems equipList;
    private List<string> potentialEquipment = new List<string>();
    private Equipment presentEquip;

    public int getEarnEquip()
    {
        return this.presentEquip.index;
    }
    public bool getObtained()
    {
        return obtained;
    }

    public static event Action<string> EquipObtainClicked;
    public static event Action EquipCancelClicked;
    public static event Action presentComplete;

    //�� �ڸ��� ���� �� ����ٸ�?
    public static event Action<string> EquipObtainComplete;

    private void OnEnable()
    {
        Equipment_ResultInventory.EquipObtainCompleteFromInventory += Equipment_ResultInventory_EquipmentObtainComplete;
        CurtainsUp.CurtainHasBeenLifted += enableBtn;
        Reward_Supply.SupplyCancelClicked += enableBtn;
        Reward_Supply.SupplyObtainClicked += disableBtn;
        Supply_ResultInventory.SupplyObtainCompleteFromInventory += enableBtn;
        Reward_Hallucination.HalluObtainClicked += disableBtn;
        Reward_Hallucination.HalluCancelClicked += enableBtn;
        PackComplete.CompletePressed += disableBtn;
    }
    private void OnDisable()
    {
        Equipment_ResultInventory.EquipObtainCompleteFromInventory -= Equipment_ResultInventory_EquipmentObtainComplete;
        CurtainsUp.CurtainHasBeenLifted -= enableBtn;
        Reward_Supply.SupplyCancelClicked -= enableBtn;
        Reward_Supply.SupplyObtainClicked -= disableBtn;
        Supply_ResultInventory.SupplyObtainCompleteFromInventory -= enableBtn;
        Reward_Hallucination.HalluObtainClicked -= disableBtn;
        Reward_Hallucination.HalluCancelClicked -= enableBtn;
        PackComplete.CompletePressed -= disableBtn;
    }

    private void disableBtn()
    {
        this.btn.GetComponent<Button>().interactable = false;
    }
    private void disableBtn(string obj)
    {
        this.btn.GetComponent<Button>().interactable = false;
    }

    private void enableBtn()
    {
        if (!obtained)
        {
            this.btn.GetComponent<Button>().interactable = true;
        }
    }

    private void Equipment_ResultInventory_EquipmentObtainComplete()
    {
        this.btn.GetComponent<Button>().interactable = false;
        obtained = true;
        this.btn.GetComponentInChildren<TextMeshProUGUI>().SetText("ȹ�� �Ϸ�");
        this.check.SetActive(true);
    }

    private void initPotenEquip(int[] b)
    {
        List<int> a = new List<int>();
        string realName;
        int lim = 0;
        Array.Sort(b);
        for (int i = 0; i < 3; i++)
        {
            if (b[i] != -1)
            {
                a.Add(b[i]);
            }
        }
        int temp = a.Count;
        
        if (b[0] <= 3)
        {
            lim = 1;
        }
        if (temp == 3)
        {
            trouble = true;
        }
        for(int i = 0; i<equipList.items.Count; i++)
        {
            potentialEquipment.Add(equipList.items[i]);
        }
        for (int i = (temp - 1); i >= lim; i--)
        {
            potentialEquipment.RemoveAt(b[i]);
        }
        for (int i = 0; i < 4; i++)
        {
            potentialEquipment.RemoveAt(0);
        }

        realName = potentialEquipment[(UnityEngine.Random.Range(0, potentialEquipment.Count))];

        presentEquip = Resources.Load<Equipment>("ScriptableObject/Equipment/" + realName);
    }

    private void presentEquipment()
    {
        equipImage.GetComponent<TooltipTrigger>().enabled = true;
        equipImage.GetComponent<TooltipTrigger>().header = presentEquip.equipName;
        equipImage.GetComponent<TooltipTrigger>().content = presentEquip.description;
        equipImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Equipment/" + presentEquip.realName);
    }

    private void RewardEquip_onClick()
    {
        //���� 3���� equip�� ������ ���� ������,
        if(!trouble)
        {
            this.btn.GetComponent<Button>().interactable = false;
            obtained = true;
            this.btn.GetComponentInChildren<TextMeshProUGUI>().SetText("ȹ�� �Ϸ�");
            EquipObtainComplete?.Invoke(presentEquip.realName);
            this.check.SetActive(true);
        }
        else
        {
            //ȹ���� Ŭ���ߴٸ�
            if (!situ)
            {
                situ = true;
                //�̰ɷ� �ϴ� �ٸ� �ֵ��� �� ��װ�,
                EquipObtainClicked?.Invoke(presentEquip.realName);
                this.btn.GetComponent<Image>().color = new Color32(165, 255, 235, 255);
                this.btn.GetComponentInChildren<TextMeshProUGUI>().SetText("ȹ�� ����");
            }
            //��Ҹ� Ŭ���ߴٸ�
            else
            {
                situ = false;
                EquipCancelClicked?.Invoke();
                this.btn.GetComponent<Image>().color = Color.white;
                this.btn.GetComponentInChildren<TextMeshProUGUI>().SetText("ȹ  ��");
            }
        }
    }

    private void Awake()
    {
        int temp = 0;
        if(saveManager.saving.isBattle)
        {
            initPotenEquip(saveManager.saving.equip);
        }
        else
        {
            presentEquip = Resources.Load<Equipment>("ScriptableObject/Equipment/" + equipList.items[saveManager.saving.earnEquip]);
            for(int i =0; i<3;i++)
            {
                if (saveManager.saving.equip[i] != -1)
                {
                    temp++;
                }
            }
            if(temp == 3) { trouble = true; }
            this.obtained = saveManager.saving.equipIsEarned;
        }
    }

    private void Start()
    {
        presentEquipment();
        if (saveManager.saving.isBattle)
        {
            presentComplete?.Invoke();
        }
        else
        {
            if (obtained)
            {
                Equipment_ResultInventory_EquipmentObtainComplete();
            }
        }
    }
}
