using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_SceneTrans : MonoBehaviour
{
    private void OnEnable()
    {
        SaveM_Title.NewGameTrans += SaveM_Title_NewGameTrans;
        SaveM_Title.LoadGameTrans += SaveM_Title_LoadGameTrans;
    }

    private void OnDisable()
    {
        SaveM_Title.NewGameTrans -= SaveM_Title_NewGameTrans;
        SaveM_Title.LoadGameTrans -= SaveM_Title_LoadGameTrans;
    }

    private void SaveM_Title_LoadGameTrans(bool obj)
    {
        if(obj)
        {
            LoadScene("Battle");
        }
        else
        {
            LoadScene("Result");
        }
    }

    private void SaveM_Title_NewGameTrans()
    {
        LoadScene("Story");
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
}
