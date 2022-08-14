using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Reward_Hallucination : MonoBehaviour
{
    [SerializeField] private GameObject[] halluImage;
    [SerializeField] private GameObject[] check;
    [SerializeField] private GameObject[] btn;

    [SerializeField] private ListOfItems halluList;
    private Hallucination_Base[] presentHallu = new Hallucination_Base[3];

    private int selectedNum = -1;
    private bool pickComplete = false;

    public int[] getHalluList()
    {
        int[] halluci = new int[3];
        for(int i =0; i<3; i++)
        {
            halluci[i] = this.presentHallu[i].index;
        }
        return halluci;
    }
    public bool getObtained()
    {
        return pickComplete;
    }
    public int getSelectedNum()
    {
        //obtained�� ���ʺ��� false�� �̰� �ǹ̰� ����.
        //�ݴ�� obtained�� true���� �ǹ̸� �����°���.
        return selectedNum;
    }

    //ȯ���� ���� ������ �� �ֱ� ������ ���
    public static event Action HalluObtainClicked;
    public static event Action HalluCancelClicked;
    public static event Action HallusPickComplete;

    private void OnEnable()
    {
        CurtainsUp.CurtainHasBeenLifted += enableBtn;
        Reward_Equip.EquipObtainClicked += disableBtn;
        Reward_Equip.EquipCancelClicked += enableBtn;
        Equipment_ResultInventory.EquipObtainCompleteFromInventory += enableBtn;
        Reward_Supply.SupplyCancelClicked += enableBtn;
        Reward_Supply.SupplyObtainClicked += disableBtn;
        Supply_ResultInventory.SupplyObtainCompleteFromInventory += enableBtn;
        PackComplete.CompletePressed += disableBtn;
    }
    private void OnDisable()
    {
        CurtainsUp.CurtainHasBeenLifted -= enableBtn;
        Reward_Equip.EquipObtainClicked -= disableBtn;
        Reward_Equip.EquipCancelClicked -= enableBtn;
        Equipment_ResultInventory.EquipObtainCompleteFromInventory -= enableBtn;
        Reward_Supply.SupplyCancelClicked -= enableBtn;
        Reward_Supply.SupplyObtainClicked -= disableBtn;
        Supply_ResultInventory.SupplyObtainCompleteFromInventory -= enableBtn;
        PackComplete.CompletePressed -= disableBtn;
    }


    private void showHallucination()
    {
        for (int i = 0; i < 3; i++)
        {
            halluImage[i].GetComponent<TooltipTrigger>().enabled = true;
            halluImage[i].GetComponent<TooltipTrigger>().header = presentHallu[i].halluName;
            halluImage[i].GetComponent<TooltipTrigger>().content = presentHallu[i].description;
            check[i].GetComponent<TooltipTrigger>().header = presentHallu[i].halluName;
            check[i].GetComponent<TooltipTrigger>().content = presentHallu[i].description;
            halluImage[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Hallucination/" + presentHallu[i].realName);
        }
    }

    private void RewardHallu_Onclick(int num)
    {
        //�� �� Ŭ���� ���� �ʾҴٸ�
        if(selectedNum == -1)
        {
            selectedNum = num;
            for (int i = 0; i < 3; i++)
            {
                if (i == num)
                {
                    btn[i].GetComponent<Image>().color = new Color32(255, 105, 105, 255);
                    btn[i].GetComponentInChildren<TextMeshProUGUI>().SetText("Ȯ   ��");
                }
                else if (i != num)
                {
                    btn[i].GetComponent<Image>().color = new Color32(165, 255, 235, 255);
                    btn[i].GetComponentInChildren<TextMeshProUGUI>().SetText("��   ��");
                }
            }
            HalluObtainClicked?.Invoke();
        }
        else
        {
            //Ȯ���� ���� ��
            if(selectedNum == num)
            {
                for (int i = 0; i < 3; i++)
                {
                    btn[i].GetComponent<Button>().interactable = false;
                    check[i].SetActive(true);
                    
                    if (i == num)
                    {
                        presentHallu[i].onUse();
                        btn[i].GetComponentInChildren<TextMeshProUGUI>().SetText("�޾Ƶ���");
                        check[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/UI/check");
                    }
                    else if (i != num)
                    {
                        btn[i].GetComponentInChildren<TextMeshProUGUI>().SetText("�� �� ��");
                        check[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/UI/xMark");
                    }
                }
                pickComplete = true;
                HalluCancelClicked?.Invoke(); //������ �� ��ȣ�� ��ο��Դ� activate ��ȣ�ϱ�, ���� halluspickcomplete�� �� �� �ʿ�� ����
                HallusPickComplete?.Invoke();
            }
            //Ȯ���� �ƴ� ���, �� ������ ���� ��
            else
            {
                selectedNum = -1;
                for (int i = 0; i < 3; i++)
                {
                    btn[i].GetComponent<Image>().color = Color.white;
                    btn[i].GetComponentInChildren<TextMeshProUGUI>().SetText("��   ��");
                }
                HalluCancelClicked?.Invoke();
            }
        }
    }

    private void enableBtn()
    {
        if (!pickComplete)
        {
            for (int i = 0; i < 3; i++)
            {
                btn[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    private void disableBtn(string n)
    {
        if (!pickComplete)
        {
            for (int i = 0; i < 3; i++)
            {
                btn[i].GetComponent<Button>().interactable = false;
            }
        }
    }
    private void disableBtn()
    {
        if (!pickComplete)
        {
            for (int i = 0; i < 3; i++)
            {
                btn[i].GetComponent<Button>().interactable = false;
            }
        }
    }

    private void Start()
    {
        int num;
        int pos;
        int halluNum = halluList.items.Count / 2;

        List<int> temp = new List<int>();
        for (int i = 0; i < 10; i++)
        {
            temp.Add(i);
        }

        for (int i = 0; i < 3; i++)
        {
            num = temp[UnityEngine.Random.Range(0, temp.Count)];
            pos = UnityEngine.Random.Range(0, 2);

            presentHallu[i] =
                (Hallucination_Base)Activator.CreateInstance(Type.GetType(halluList.items[(2 * num) + pos]));
            temp.Remove(num);
        }

        showHallucination();
    }
}
