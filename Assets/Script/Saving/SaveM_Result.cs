using System.IO;
using UnityEngine;
using System;
using System.Collections;

public class SaveM_Result : MonoBehaviour
{
    private string filePath;

    public SaveBase saving;

    [SerializeField] Result_QuirkManager quirkManager;
    [SerializeField] Supply_ResultInventory supplyManager;
    [SerializeField] Equipment_ResultInventory equipmentManager;
    [SerializeField] Player_Result player;
    [SerializeField] Result_StatusManager statusManager;
    [SerializeField] Reward_Equip rewardEquip;
    [SerializeField] Reward_Supply rewardSupply;
    [SerializeField] Reward_Hallucination rewardHallu;

    //isbattle이 false라면 battle 중에 save가 된 것이 아니기 때문에, false.
    //반대면 true.
    //false면 save를 해야하고, true면 load를 해야겠지?
    private bool wasBattle;
    public bool getWasBattle() { return wasBattle; }

    public static event Action firstSaveFinished;
    public static event Action middleSaveFinished;
    public static event Action finalSaveFinished;

    private void OnEnable()
    {
        initialSaveCounter.InitialSavePlz += InitialSaveCounter_InitialSavePlz;
        PackComplete.CompletePressed += PackComplete_CompletePressed;
        Reward_Hallucination.HallusPickComplete += Reward_Hallucination_HallusPickComplete;
    }

    private void OnDisable()
    {
        initialSaveCounter.InitialSavePlz -= InitialSaveCounter_InitialSavePlz;
        PackComplete.CompletePressed -= PackComplete_CompletePressed;
        Reward_Hallucination.HallusPickComplete -= Reward_Hallucination_HallusPickComplete;
    }


    private void PackComplete_CompletePressed()
    {
        SavePlayer(true);
        finalSaveFinished?.Invoke();
    }

    private void Reward_Hallucination_HallusPickComplete()
    {
        StartCoroutine(MiddleSave());
    }

    private IEnumerator MiddleSave()
    {
        yield return new WaitForFixedUpdate();
        SavePlayer(false);
        middleSaveFinished?.Invoke();
    }

    private void InitialSaveCounter_InitialSavePlz()
    {
        SavePlayer(false);
        firstSaveFinished?.Invoke();
    }

    //Player의 data를 awake시에 load하고 이걸 다른 아이들은 start시에 확인하도록
    private void Awake()
    {
        filePath = Application.dataPath + "/PlayerSave.json";
        saving = new SaveBase();
        loadPlayer();

        if (!saving.isBattle) { wasBattle = false; }
        else { wasBattle = true; }
    }

    private void SavePlayer(bool toBat)
    {
        saving.madness = statusManager.getMadness();
        saving.sanity = statusManager.getSanity();
        saving.obsession = statusManager.getObsession();
        ////////////////////
        saving.will = quirkManager.getWillNum();
        saving.feeble = quirkManager.getFeebleNum();
        ////////////////////
        saving.supply = supplyManager.getSupplyString();
        ////////////////////
        saving.equip = equipmentManager.getEquipNum();
        saving.curEquip = equipmentManager.getCurEquip();
        ////////////////////
        saving.resist = player.getResist();
        ////////////////////
        saving.isBattle = false;
        saving.turn = -1;
        ////////////////////
        saving.earnEquip = rewardEquip.getEarnEquip();
        saving.equipIsEarned = rewardEquip.getObtained();
        ////////////////////
        saving.earnSupply = rewardSupply.getEarnSupply();
        saving.supplyIsEarned = rewardSupply.getObtained();
        ////////////////////
        saving.halluList = rewardHallu.getHalluList();
        saving.halluIsEarned = rewardHallu.getObtained();
        saving.earnedHalluIs = rewardHallu.getSelectedNum();

        saving.toBattle = toBat;

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
        saving.turn = -2;
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
