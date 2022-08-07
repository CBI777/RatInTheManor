using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySupplyUIManager : MonoBehaviour
{
    //�ϴ� ���� script�� �����ϱ� ���ؼ� ó�� ���ص� / ���߿��� ó���ؾ��Ѵ�.
    //���� �ܿ��� ����� �� ������ ���� �ȴ�.
    [SerializeField] private bool[] checkBool = new bool[5];
    [SerializeField] private GameObject[] supply = new GameObject[5];
    [SerializeField] private GameObject[] bg = new GameObject[5];
    [SerializeField] private GameObject[] btn = new GameObject[5];
    [SerializeField] private GameObject myText;

    private void OnEnable()
    {
        SupplyManager.SupplyChangedEvent += SupplyManager_SupplyChangedEvent;
        SkillManager.BattleStart += SkillManager_BattleStart;
        TurnManager.BattleEndEvent += TurnManager_BattleEnd;
    }

    private void OnDisable()
    {
        SupplyManager.SupplyChangedEvent -= SupplyManager_SupplyChangedEvent;
        SupplyManager.SupplyChangedEvent -= SupplyManager_SupplyChangedEvent;
        TurnManager.BattleEndEvent -= TurnManager_BattleEnd;
    }

    private void TurnManager_BattleEnd()
    {
        for (int i = 0; i < 5; i++)
        {
            if (checkBool[i])
            {
                btn[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    private void SkillManager_BattleStart(int obj)
    {
        for(int i = 0; i < 5; i++)
        {
            if (btn[i].activeSelf)
            {
                btn[i].GetComponent<Button>().interactable = false;
            }
        }
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
                if (arg2[i].usage != 1) { checkBool[j] = true;}

                //��ư ����
                btn[j].SetActive(true);
            }
            else
            {
                bg[j].SetActive(false);
                btn[j].SetActive(false);
            }
        }
    }
}
