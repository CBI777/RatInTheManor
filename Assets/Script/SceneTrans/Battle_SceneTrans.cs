using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Battle_SceneTrans : MonoBehaviour
{
    [SerializeField] SaveM_Battle saveManager;
    private void OnEnable()
    {
        CurtainsDown.CurtainDownComplete += CurtainsDown_CurtainDownComplete;
        CurtainsDown.TheEnd += CurtainsDown_TheEnd;
    }

    private void OnDisable()
    {
        CurtainsDown.CurtainDownComplete -= CurtainsDown_CurtainDownComplete;
        CurtainsDown.TheEnd -= CurtainsDown_TheEnd;
    }

    private void CurtainsDown_TheEnd()
    {
        LoadScene("Story_BadEnd");
    }

    private void CurtainsDown_CurtainDownComplete()
    {
        if(saveManager.saving.stageNum == 8)
        {
            LoadScene("Story_GoodEnd");
        }
        else
        {
            LoadScene("Result");
        }
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
