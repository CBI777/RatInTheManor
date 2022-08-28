using System.Collections;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class StoryOpening_SceneTrans : MonoBehaviour
{
    private AudioSource audioSource;
    private void Awake()
    {
        this.audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Story_Opening.StartGame += Story_Opening_StartGame;
    }

    private void OnDisable()
    {
        Story_Opening.StartGame -= Story_Opening_StartGame;
    }

    private void VolumeDown()
    {
        StartCoroutine(VolDownToEnd());
    }
    IEnumerator VolDownToEnd()
    {
        float v = this.audioSource.volume;

        do
        {
            v -= 0.01f;
            this.audioSource.volume = v;
            yield return new WaitForSeconds(0.01f);

        } while (v >= 0);

        this.audioSource.Stop();
    }



    private void Story_Opening_StartGame()
    {
        VolumeDown();
        LoadScene("Battle");
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
