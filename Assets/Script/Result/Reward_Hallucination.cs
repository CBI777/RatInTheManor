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
        //obtained가 애초부터 false면 이건 의미가 없음.
        //반대로 obtained가 true여야 의미를 가지는거임.
        return selectedNum;
    }

    //환각을 골라야 진행할 수 있기 때문에 사용
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
        //한 번 클릭을 하지 않았다면
        if(selectedNum == -1)
        {
            selectedNum = num;
            for (int i = 0; i < 3; i++)
            {
                if (i == num)
                {
                    btn[i].GetComponent<Image>().color = new Color32(255, 105, 105, 255);
                    btn[i].GetComponentInChildren<TextMeshProUGUI>().SetText("확   정");
                }
                else if (i != num)
                {
                    btn[i].GetComponent<Image>().color = new Color32(165, 255, 235, 255);
                    btn[i].GetComponentInChildren<TextMeshProUGUI>().SetText("보   류");
                }
            }
            HalluObtainClicked?.Invoke();
        }
        else
        {
            //확정을 했을 때
            if(selectedNum == num)
            {
                for (int i = 0; i < 3; i++)
                {
                    btn[i].GetComponent<Button>().interactable = false;
                    check[i].SetActive(true);
                    
                    if (i == num)
                    {
                        presentHallu[i].onUse();
                        btn[i].GetComponentInChildren<TextMeshProUGUI>().SetText("받아들임");
                        check[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/UI/check");
                    }
                    else if (i != num)
                    {
                        btn[i].GetComponentInChildren<TextMeshProUGUI>().SetText("외 면 됨");
                        check[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/UI/xMark");
                    }
                }
                pickComplete = true;
                HalluCancelClicked?.Invoke(); //어차피 이 신호가 모두에게는 activate 신호니까, 굳이 halluspickcomplete를 또 줄 필요는 없지
                HallusPickComplete?.Invoke();
            }
            //확정이 아닐 경우, 즉 보류를 했을 때
            else
            {
                selectedNum = -1;
                for (int i = 0; i < 3; i++)
                {
                    btn[i].GetComponent<Image>().color = Color.white;
                    btn[i].GetComponentInChildren<TextMeshProUGUI>().SetText("선   택");
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
