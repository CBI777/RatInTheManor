using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    private void Awake()

    {
        this.audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Title_UI.ResumeGame += VolumeDown;
        Title_UI.StartNewGame += VolumeDown;
    }
    private void OnDisable()
    {
        Title_UI.ResumeGame -= VolumeDown;
        Title_UI.StartNewGame -= VolumeDown;
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
}
