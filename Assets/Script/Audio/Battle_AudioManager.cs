using System.Collections;
using UnityEngine;

public class Battle_AudioManager : MonoBehaviour
{
    [SerializeField] SaveM_Battle saveManager;
    private Enemy_Base enemy;

    private AudioSource audioSource;
    private void Awake()
    {
        this.audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        SkillManager.enemyDecidedEvent += SkillManager_enemyDecidedEvent;
        BattleDialogueProvider.startFromDialogue += BattleDialogueProvider_startFromDialogue;
        TurnManager.BattleEndEvent += VolumeDown;
        BattleDialogueProvider.YouAreMad += VolumeDown;
    }


    private void OnDisable()
    {
        SkillManager.enemyDecidedEvent -= SkillManager_enemyDecidedEvent;
        BattleDialogueProvider.startFromDialogue -= BattleDialogueProvider_startFromDialogue;
        TurnManager.BattleEndEvent -= VolumeDown;
        BattleDialogueProvider.YouAreMad -= VolumeDown;
    }

    private void BattleDialogueProvider_startFromDialogue()
    {
        this.enemy = Resources.Load<Enemy_Base>("ScriptableObject/Enemy/" + saveManager.saving.selEnemy);
        this.audioSource.clip = Resources.Load<AudioClip>("BGM/" + this.enemy.bgm);
        StartCoroutine(VolUpToStart(0.5f));
    }

    private void SkillManager_enemyDecidedEvent(string arg1, string arg2, string arg3)
    {
        this.audioSource.clip = Resources.Load<AudioClip>("BGM/" + arg3);
        StartCoroutine(VolUpToStart(0.5f));
    }

    private void VolumeDown()
    {
        StartCoroutine(VolDownToEnd());
    }

    IEnumerator VolUpToStart(float b)
    {
        this.audioSource.Play();
        float v = 0f;

        do
        {
            v += 0.00125f;
            this.audioSource.volume = v;
            yield return new WaitForSeconds(0.01f);

        } while (v <= b);
    }
    IEnumerator VolDownToEnd()
    {
        float v = this.audioSource.volume;

        do
        {
            v -= 0.00125f;
            this.audioSource.volume = v;
            yield return new WaitForSeconds(0.01f);

        } while (v >= 0);

        this.audioSource.Stop();
    }
}
