using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DmgUIManager : MonoBehaviour
{
    [SerializeField] private Image dmgImg;

    private void OnEnable()
    {
        BattleDialogueProvider.DmgTypeShow += BattleDialogueProvider_DmgTypeShow;
    }
    private void OnDisable()
    {
        BattleDialogueProvider.DmgTypeShow -= BattleDialogueProvider_DmgTypeShow;
    }

    private void BattleDialogueProvider_DmgTypeShow(DmgType obj)
    {
        StartCoroutine(DmgImgShow(obj));
    }

    IEnumerator DmgImgShow(DmgType dmgType)
    {
        string a;
        dmgImg.gameObject.SetActive(true);
        switch(dmgType)
        {
            case DmgType.Phys: a = "Phys_Dmg"; break;
            case DmgType.Fear: a = "Fear_Dmg"; break;
            case DmgType.Abhorr: a = "Abhorr_Dmg"; break;
            default: a = "Delus_Dmg"; break;
        }
        dmgImg.sprite = Resources.Load<Sprite>("Sprite/UI/" + a);
        Color c = new Color(1f, 1f, 1f, 0.7f);
        dmgImg.color = c;

        for (int i = 0; i <= 100; i++)
        {
            c.a -= 0.007f;
            dmgImg.color = c;
            yield return new WaitForSeconds(0.01f);
        }

        dmgImg.gameObject.SetActive(false);
    }
}
