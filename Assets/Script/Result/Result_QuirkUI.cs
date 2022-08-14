using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Result_QuirkUI : MonoBehaviour
{
    [SerializeField] private GameObject[] wills = new GameObject[5];

    [SerializeField] private GameObject[] feeble = new GameObject[5];

    private void OnEnable()
    {
        Result_QuirkManager.WillChangedEvent += QuirkManager_WillChangedEvent;
        Result_QuirkManager.FeebleChangedEvent += QuirkManager_FeebleChangedEvent;
    }

    private void OnDisable()
    {
        Result_QuirkManager.WillChangedEvent -= QuirkManager_WillChangedEvent;
        Result_QuirkManager.FeebleChangedEvent -= QuirkManager_FeebleChangedEvent;
    }

    private void QuirkManager_WillChangedEvent(int arg1, Quirk_Base[] arg2)
    {
        for (int i = 0; i < 5; i++)
        {
            if (i < arg1)
            {
                wills[i].GetComponent<Image>().enabled = true;
                wills[i].GetComponent<TooltipTrigger>().enabled = true;
                wills[i].GetComponentInChildren<TextMeshProUGUI>().SetText(arg2[i].quirkName);
                wills[i].GetComponent<TooltipTrigger>().header = arg2[i].quirkName;
                wills[i].GetComponent<TooltipTrigger>().content = arg2[i].quirkDescription;
            }
            else
            {
                wills[i].GetComponent<Image>().enabled = false;
                wills[i].GetComponentInChildren<TextMeshProUGUI>().SetText("");
                wills[i].GetComponent<TooltipTrigger>().enabled = false;
            }
        }
    }

    private void QuirkManager_FeebleChangedEvent(int arg1, Quirk_Base[] arg2)
    {
        for (int i = 0; i < 5; i++)
        {
            if (i < arg1)
            {
                feeble[i].GetComponent<Image>().enabled = true;
                feeble[i].GetComponent<TooltipTrigger>().enabled = true;
                feeble[i].GetComponentInChildren<TextMeshProUGUI>().SetText(arg2[i].quirkName);
                feeble[i].GetComponent<TooltipTrigger>().header = arg2[i].quirkName;
                feeble[i].GetComponent<TooltipTrigger>().content = arg2[i].quirkDescription;
            }
            else
            {
                feeble[i].GetComponent<Image>().enabled = false;
                feeble[i].GetComponentInChildren<TextMeshProUGUI>().SetText("");
                feeble[i].GetComponent<TooltipTrigger>().enabled = false;
            }
        }
    }
}
