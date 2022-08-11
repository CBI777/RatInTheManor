using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class BeforeDialogueController : MonoBehaviour
{
    private PlayerInput playerinput;

    [SerializeField] private RectTransform Play_CoverArea;

    private AudioSource audioSource;
    [SerializeField] private AudioClip clip;

    [SerializeField] private int resetMove = 1920;
    [SerializeField] private float resetSpeed = 1f;


    private void OnEnable()
    {
        SkillManager.enemyDecidedEvent += SkillManager_enemyDecidedEvent;
        SkillManager.SkillAddedEvent += SkillManager_SkillAddedEvent;
        BattleDialogueProvider.betweenTurnDia += BattleDialogueProvider_betweenTurnDia;
    }

    private void OnDisable()
    {
        SkillManager.enemyDecidedEvent -= SkillManager_enemyDecidedEvent;
        SkillManager.SkillAddedEvent -= SkillManager_SkillAddedEvent;
        BattleDialogueProvider.betweenTurnDia -= BattleDialogueProvider_betweenTurnDia;
    }

    private void BattleDialogueProvider_betweenTurnDia()
    {
        this.audioSource.PlayOneShot(clip);
        playerinput.actions.FindActionMap("PlayerInput").Disable();
        Play_CoverArea.DOAnchorPos(new Vector2(0, -180f), resetSpeed);
    }

    private void SkillManager_SkillAddedEvent(ListWrapper<EnemySkill> arg1, int arg2)
    {
        this.audioSource.PlayOneShot(clip);
        Play_CoverArea.DOAnchorPos(new Vector2(-resetMove, -180f), resetSpeed);
        playerinput.actions.FindActionMap("PlayerInput").Enable();
    }

    private void SkillManager_enemyDecidedEvent(string obj, string sfx)
    {
        playerinput.actions.FindActionMap("PlayerInput").Disable();
    }
    
    private void Awake()
    {
        this.audioSource = this.transform.GetComponent<AudioSource>();
        playerinput = GetComponent<PlayerInput>();
    }
}
