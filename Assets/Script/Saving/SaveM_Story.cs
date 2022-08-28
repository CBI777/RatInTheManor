using System;
using System.IO;
using UnityEngine;

public class SaveM_Story : MonoBehaviour
{
    private string filePath;

    public SaveBase saving = new SaveBase();

    public static event Action StorySaveComplete;

    private void OnEnable()
    {
        StoryButton.OnCharaDecision += StoryButton_OnCharaDecision;
    }
    private void OnDisable()
    {
        StoryButton.OnCharaDecision -= StoryButton_OnCharaDecision;
    }

    private void StoryButton_OnCharaDecision(int obj)
    {
        switch(obj)
        {
            case 0:
                this.saving.resist[0] = 1;
                this.saving.resist[2] = -1;
                this.saving.equip[0] = 0;
                this.saving.will[0] = 0;
                break;
            case 1:
                this.saving.resist[1] = 1;
                this.saving.resist[0] = -1;
                this.saving.equip[0] = 1;
                this.saving.will[0] = 1;
                break;
            case 2:
                this.saving.resist[2] = 1;
                this.saving.resist[3] = -1;
                this.saving.equip[0] = 2;
                this.saving.will[0] = 2;
                break;
            default:
                this.saving.resist[3] = 1;
                this.saving.resist[1] = -1;
                this.saving.equip[0] = 3;
                this.saving.will[0] = 3;
                break;
        }
        saving.stageNum = 0;

        SavePlayer();
        StorySaveComplete?.Invoke();
    }


    private void Awake()
    {
        filePath = Application.dataPath + "/PlayerSave.json";
        saving = new SaveBase();
        loadPlayer();
    }

    private void SavePlayer()
    {
        saving.stageNum = 0;

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
        using (StreamReader reader = new StreamReader(filePath))
        {
            content = reader.ReadToEnd();
        }

        this.saving = JsonUtility.FromJson<SaveBase>(content);
    }
}
