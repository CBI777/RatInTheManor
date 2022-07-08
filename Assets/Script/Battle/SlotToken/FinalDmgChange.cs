using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalDmgChange : MonoBehaviour
{
    [SerializeField] private int typeNum;
    [SerializeField] private TextMeshProUGUI myText;

    private void OnEnable()
    {
        myText = this.transform.GetComponent<TextMeshProUGUI>();
        SlotManager.FinalDmgChanged += SlotManager_FinalDmgChanged;
    }

    private void OnDisable()
    {
        SlotManager.FinalDmgChanged -= SlotManager_FinalDmgChanged;
    }

    private void SlotManager_FinalDmgChanged(int arg1, int arg2, int arg3, int arg4, int arg5)
    {
        switch (typeNum)
        {
            case 0:
                myText.SetText(arg1.ToString());
                break;
            case 1:
                myText.SetText(arg2.ToString());
                break;
            case 2:
                myText.SetText(arg3.ToString());
                break;
            case 3:
                myText.SetText(arg4.ToString());
                break;
            case 4:
                myText.SetText(arg5.ToString());
                break;
        }
    }
}
