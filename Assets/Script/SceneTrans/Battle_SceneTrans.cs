using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Battle_SceneTrans : MonoBehaviour
{
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
        LoadScene("Result");
    }

    public async void LoadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        do
        {
            await Task.Delay(1000);
        } while (scene.progress < 0.9f);
        ////2022_02_09 - Unity�� scene�� 0.9�� �Ǿ��� �� load�� �Ǳ� ������ 0.9�� �ξ����.

        scene.allowSceneActivation = true;
    }
}
