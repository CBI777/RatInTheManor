using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Story_Ending_SceneTrans : MonoBehaviour
{
    private string filePath;

    public SaveBase saving = new SaveBase();
    [SerializeField] private GameObject blackOut;
    [SerializeField] private AudioSource audioSource;

    public void ToTitle()
    {
        LoadScene("Title");
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void fadeStart(bool a)
    {
        StartCoroutine(VolDownToEnd());
        StartCoroutine(BackGroundFade(a));
    }

    public async void LoadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        do
        {
            await Task.Delay(1000);
        } while (scene.progress < 0.9f);
        ////2022_02_09 - Unity는 scene이 0.9가 되었을 때 load가 되기 때문에 0.9로 두어야함.

        scene.allowSceneActivation = true;
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

    IEnumerator BackGroundFade(bool isQuit)
    {
        blackOut.transform.SetAsLastSibling();
        blackOut.gameObject.SetActive(true);
        Color c = new Color(0f, 0f, 0f, 0f);
        for (int i = 0; i <= 200; i++)
        {
            c.a = i / 200f;
            blackOut.GetComponent<Image>().color = c;
            yield return new WaitForSeconds(0.01f);
        }
        killPlayer();
        if(isQuit)
        {
            QuitGame();
        }
        else
        {
            ToTitle();
        }
    }
    IEnumerator VolDownToEnd()
    {
        float v = this.audioSource.volume;
        do
        {
            v -= 0.001f;
            this.audioSource.volume = v;
            yield return new WaitForSeconds(0.01f);

        } while (v >= 0.3);

        this.audioSource.Stop();
    }
    private void Awake()
    {
        filePath = Application.dataPath + "/PlayerSave.json";
        saving = new SaveBase();
    }
}
