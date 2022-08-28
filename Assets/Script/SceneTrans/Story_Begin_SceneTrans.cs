using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Story_Begin_SceneTrans : MonoBehaviour
{
    private void OnEnable()
    {
        Story_Begin.SafeToGo += CurtainsDown_CurtainDownComplete;
    }
    private void OnDisable()
    {
        Story_Begin.SafeToGo -= CurtainsDown_CurtainDownComplete;
    }

    private void CurtainsDown_CurtainDownComplete()
    {
        LoadScene("Story_Before");
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