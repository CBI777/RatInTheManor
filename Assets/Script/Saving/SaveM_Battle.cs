using System;
using System.IO;
using UnityEngine;

public class SaveM_Battle : MonoBehaviour
{
    private string filePath;

    public SaveBase saving = new SaveBase();

    [SerializeField] QuirkManager quirkManager;
    [SerializeField] SupplyManager supplyManager;
    [SerializeField] EquipmentManager equipmentManager;
    [SerializeField] Player player;
    [SerializeField] StatusManager statusManager;
    [SerializeField] StageManager stageManager;
    [SerializeField] TurnManager turnManager;
    [SerializeField] SkillManager skillManager;

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
        SkillManager.enemySetEvent += SkillManager_enemySetEvent;
        BattleDialogueProvider.beforeTurnEvent += BattleDialogueProvider_beforeTurnEvent;
        TurnManager.BatEndReached += TurnManager_BatEndReached;
    }

    private void OnDisable()
    {
        SkillManager.enemySetEvent -= SkillManager_enemySetEvent;
        BattleDialogueProvider.beforeTurnEvent -= BattleDialogueProvider_beforeTurnEvent;
        TurnManager.BatEndReached -= TurnManager_BatEndReached;
    }

    private void TurnManager_BatEndReached()
    {
        SavePlayer(false);
        finalSaveFinished?.Invoke();
    }

    private void BattleDialogueProvider_beforeTurnEvent()
    {
        SavePlayer(true);
        middleSaveFinished?.Invoke();
    }

    private void SkillManager_enemySetEvent()
    {
        SavePlayer(true);
        firstSaveFinished?.Invoke();
    }

    //Player의 data를 awake시에 load하고 이걸 다른 아이들은 start시에 확인하도록
    private void Awake()
    {
        filePath = Application.dataPath + "/PlayerSave.json";
        saving = new SaveBase();
        loadPlayer();

        if(!saving.isBattle) { wasBattle = false; }
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
        saving.stageNum = stageManager.getStageNum();
        ////////////////////
        saving.isBattle = true;
        ////////////////////
        saving.turn = turnManager.getTurnCount();
        ////////////////////
        saving.selEnemy = skillManager.getSelEnemy();
        ////////////////////
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
        for(int i = 1; i<6; i ++) { saving.supply[i] = "NA"; }
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
