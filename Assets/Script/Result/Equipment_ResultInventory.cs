using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Equipment_ResultInventory : MonoBehaviour
{
    [SerializeField] private GameObject[] bg = new GameObject[3];
    [SerializeField] private GameObject[] equips = new GameObject[3];
    [SerializeField] private GameObject[] equipcheck = new GameObject[3];
    [SerializeField] private GameObject[] btns = new GameObject[3];
    
    public static event Action<int[]> equipmentChangedEvent;

    private List<Equipment> equipment = new List<Equipment>();
    [SerializeField] private ListOfItems equipList;
    private List<string> potentialEquipment = new List<string>();

    private int curEquip = 0;
    private int equipCount;

    private void OnEnable()
    {
        CurtainsUp.CurtainHasBeenLifted += enableAll;
    }
    private void OnDisable()
    {
        CurtainsUp.CurtainHasBeenLifted -= enableAll;
    }

    private void CurEquipChange(int a)
    {
        this.curEquip = a;
        for (int i = 0; i < equipCount; i++)
        {
            if (i == curEquip)
            {
                btns[i].GetComponent<Button>().interactable = false;
                btns[i].GetComponentInChildren<TextMeshProUGUI>().SetText("Àå Âø Áß");
                equipcheck[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/UI/SelectedEdge");
            }
            else
            {
                btns[i].GetComponent<Button>().interactable = true;
                btns[i].GetComponentInChildren<TextMeshProUGUI>().SetText("Àå  Âø");
                equipcheck[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/UI/DeselectedEdge");
            }
        }
        equipmentChangedEvent?.Invoke(equipment[curEquip].resChange);
    }

    private void EquipChanged()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i < equipCount)
            {
                bg[i].SetActive(true);
                equips[i].GetComponent<TooltipTrigger>().enabled = true;
                equips[i].GetComponent<TooltipTrigger>().header = equipment[i].equipName;
                equips[i].GetComponent<TooltipTrigger>().content = equipment[i].description;
                equips[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Equipment/" + equipment[i].realName);

                if (i == curEquip)
                {
                    btns[i].GetComponent<Button>().interactable = false;
                    btns[i].GetComponentInChildren<TextMeshProUGUI>().SetText("Àå Âø Áß");
                    equipcheck[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/UI/SelectedEdge");
                }
                else
                {
                    btns[i].GetComponent<Button>().interactable = true;
                    btns[i].GetComponentInChildren<TextMeshProUGUI>().SetText("Àå  Âø");
                    equipcheck[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/UI/DeselectedEdge");
                }
                btns[i].SetActive(true);
                
            }
            else
            {
                bg[i].SetActive(false);
                btns[i].SetActive(false);
            }
        }
    }

    private void disableAll()
    {

        for (int i = 0; i < equipCount; i++)
        {
            btns[i].GetComponent<Button>().interactable = false;
        }
    }
    private void enableAll()
    {
        CurEquipChange(curEquip);
    }

    public void obtainEquipment(string realName)
    {
        this.equipment.Add(Resources.Load<Equipment>("ScriptableObject/Equipment/" + realName));
        this.equipCount = this.equipment.Count;
    }

    private void initEquip(int[] b)
    {
        int temp = b.Length;
        for (int i = 0; i < temp; i++)
        {
            obtainEquipment(potentialEquipment[b[i]]);
        }
    }

    private void initPotenEquip(int[] b)
    {
        int temp = b.Length;
        for (int i = (temp - 1); i >= 0; i--)
        {
            potentialEquipment.RemoveAt(b[i]);
        }
    }

    private void Start()
    {
        for (int i = 0; i < equipList.items.Count; i++)
        {
            this.potentialEquipment.Add(equipList.items[i]);
        }

        int[] a = { 3, 0, 1 };
        initEquip(a);

        Array.Sort(a);
        initPotenEquip(a);

        this.equipCount = this.equipment.Count;
        curEquip = 0;
        CurEquipChange(curEquip);
        EquipChanged();
        disableAll();
    }
}
