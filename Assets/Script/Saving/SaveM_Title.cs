using System;
using System.IO;
using UnityEngine;

public class SaveM_Title : MonoBehaviour
{
    private string filePath;

    public SaveBase saving = new SaveBase();

    public static event Action NewGameTrans;
    //toBattle에 따라 다르다.
    public static event Action<bool> LoadGameTrans;

    private void OnEnable()
    {
        Title_UI.StartNewGame += Title_UI_StartNewGame;
        Title_UI.ResumeGame += Title_UI_ResumeGame;
    }

    private void OnDisable()
    {
        Title_UI.StartNewGame -= Title_UI_StartNewGame;
        Title_UI.ResumeGame -= Title_UI_ResumeGame;
    }

    private void Title_UI_ResumeGame()
    {
        LoadGameTrans?.Invoke(saving.toBattle);
    }

    public void Title_UI_StartNewGame()
    {
        killPlayer();
        SavePlayer();
        NewGameTrans?.Invoke();
    }

    //Player의 data를 awake시에 load하고 이걸 다른 아이들은 start시에 확인하도록
    private void Awake()
    {
        filePath = Application.dataPath + "/PlayerSave.json";
        saving = new SaveBase();
        loadPlayer();
    }

    private void SavePlayer()
    {
        saving.stageNum = -2;

        string content = JsonUtility.ToJson(this.saving, true);
        FileStream fileStream = new FileStream(this.filePath, FileMode.Create);
        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(content);
        }
    }

    public void loadPlayer()
    {
        string content;
        if (!(File.Exists(filePath)))
        {
            killPlayer();
        }

        using (StreamReader reader = new StreamReader(filePath))
        {
            content = reader.ReadToEnd();
        }

        this.saving = JsonUtility.FromJson<SaveBase>(content);
    }

    public void killPlayer()//죽었을 때, 완전 초기로 playerSave를 돌려버린다.
    {
        saving.madness = 0;
        saving.sanity = 100;
        saving.obsession = 0;
        ////////////////////
        for (int i = 0; i < 5; i++) { saving.will[i] = -1; }
        for (int i = 0; i < 5; i++) { saving.feeble[i] = -1; }
        ////////////////////
        saving.supply[0] = "Supply_NoUse";
        for (int i = 1; i < 6; i++) { saving.supply[i] = "NA"; }
        ////////////////////
        for (int i = 0; i < 3; i++) { saving.equip[i] = -1; }
        saving.curEquip = 0;
        ////////////////////
        for (int i = 0; i < 4; i++) { saving.resist[i] = 0; }
        ////////////////////
        saving.stageNum = -2;
        ////////////////////
        saving.isBattle = false;
        ////////////////////
        saving.turn = 0;
        ////////////////////
        saving.selEnemy = "NA";
        ////////////////////
        saving.earnEquip = -1;
        saving.equipIsEarned = false;
        ////////////////////
        saving.earnSupply = "NA";
        saving.supplyIsEarned = false;
        ////////////////////
        for (int i = 0; i < 3; i++) { saving.halluList[i] = -1; }
        saving.halluIsEarned = false;
        saving.earnedHalluIs = -1;
        ////////////////////
        saving.toBattle = true;

        string content = JsonUtility.ToJson(this.saving, true);
        FileStream fileStream = new FileStream(this.filePath, FileMode.Create);
        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(content);
        }
    }
}
