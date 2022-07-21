using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class StatusManager : MonoBehaviour
{
    [SerializeField] private int sanity; //이성. 한계 이하로 줄어들면 일정 수준만큼 광기와 집착이 된다.
    [SerializeField] private int sanityMax; //이성의 최대치. 이성이 전부 줄어들면 여기서부터 다시시작.
    [SerializeField] private int sanityMin; //이성의 한계.
    [SerializeField] private int sanityBoundary; //이성이 다음에도 남는 최소 수치

    [SerializeField] private int madness; //광기. 최대치를 넘어서게 되면 미쳐버린다.
    [SerializeField] private int madnessMax; //광기의 최대치.
    [SerializeField] private int obsession; //(삶에 대한) 집착. 한계 이상으로 쌓이게 되면 의지나 나약이 되어버린다.
    [SerializeField] private int obsessionMax;

    [SerializeField] private TextMeshProUGUI sanityText;
    [SerializeField] private TextMeshProUGUI madnessText;
    [SerializeField] private TextMeshProUGUI obsessionText;
    [SerializeField] private Slider SanitySlider;
    [SerializeField] private Slider MadnessSlider;
    [SerializeField] private Slider ObsessionSlider;
    [SerializeField] private Image SanityBG;
    [SerializeField] private Image SanityBar;
    [SerializeField] private Image MadnessBar;
    [SerializeField] private Image ObsessionBar;

    //기본적으로 전투 중에는 일단 처리를 한 다음, 턴과 턴 사이에서 확인을 하고 추가적인 무언가를 하도록 되어있기 때문에...
    //일단 sanity가 바닥이 나거나, obsession이 꽉 차거나, Madness가 꽉 차도 '일단은' 아무 문제가 없이 진행이 됨.

    //Obsession은 배틀 종료시에 처리를 해 줄거고, Sanity나 Madness는 턴과 턴 사이에서 수시로 체크.
    //즉, turn관련 manager들이 주는 action들 내에서 처리가 된다는거임. supply가 뭘 하든 일단 상관이 없음.
    //이건 skill을 하나하나 보면서 처리를 해 줄 때도 상관이 없음.

    public static event Action<int> SanityEmptyEvent; //UI쪽에서 받겠지? 뭐... 소리를 지르고...
    public static event Action<int> MadnessFullEvent; //게임 오버와 관련된 아이가 받을 event
    public static event Action<int> ObsessionFullEvent; //UI쪽과 기벽쪽에서 받겠지?

    private void OnEnable()
    {
        Supply_Base.SupplySanityChange += SanityChange;
        Supply_Base.SupplyMadnessChange += MadnessChange;
        Supply_Base.SupplyObsessionChange += ObsessionChange;
    }

    private void OnDisable()
    {
        Supply_Base.SupplySanityChange -= SanityChange;
        Supply_Base.SupplyMadnessChange -= MadnessChange;
        Supply_Base.SupplyObsessionChange -= ObsessionChange;
    }


    private void ObsessionChange(int n)
    {
        this.obsession += n;

        if (this.obsession > obsessionMax) { this.obsession = obsessionMax; this.ObsessionBar.color = new Color(1f, 80f / 255f, 50f / 255f); }
        else { this.ObsessionBar.color = new Color(1f, 185f/255f, 75/255f); }
        
        if (this.obsession <= 0) { this.obsession = 0; this.ObsessionBar.gameObject.SetActive(false); }
        else { if (!this.ObsessionBar.gameObject.activeSelf) { this.ObsessionBar.gameObject.SetActive(true); } }

        this.obsessionText.SetText(this.obsession + " / " + obsessionMax);
        this.ObsessionSlider.value = ((float)this.obsession / obsessionMax);
    }
    private void SanityChange(int n)
    {
        this.sanity -= n;

        if (this.sanity < sanityMin) { this.sanity = sanityMin; }
        else if (this.sanity > sanityMax) { this.sanity = sanityMax; }

        if (this.sanity <= 0)
        { 
            this.SanityBar.gameObject.SetActive(false);
            this.SanityBG.color = new Color(1f, (125 + this.sanity) / 255f, (125 + this.sanity) / 255f);
        }
        else
        {
            this.SanityBG.color = Color.white;
            if (!this.SanityBar.gameObject.activeSelf) { this.SanityBar.gameObject.SetActive(true); }
            this.SanitySlider.value = ((float)this.sanity / sanityMax);
        }

        this.sanityText.SetText(this.sanity + " / " + sanityMax);
    }
    private void MadnessChange(int n)
    {
        this.madness += n;
        if (this.madness > madnessMax) { this.madness = madnessMax; }

        if (this.madness <= 0) { this.madness = 0; this.MadnessBar.gameObject.SetActive(false); }
        else { if (!this.MadnessBar.gameObject.activeSelf) { this.MadnessBar.gameObject.SetActive(true); } }

        this.madnessText.SetText(this.madness + " / " + madnessMax);

        float temp = ((float)this.madness / madnessMax);
        this.MadnessSlider.value = temp;
        this.MadnessBar.color = new Color((200 - (70 * temp)) / 255f, (150 - (150 * temp)) / 255f, 1f);
    }

    private void Start()
    {
        this.madnessMax = 100;
        this.sanityMax = 100;
        this.obsessionMax = 100;
        this.sanityMin = -50;
        this.sanity = 100;
        this.madness = 0;
        this.obsession = 0;

        SanityChange(80);
        MadnessChange(40);
        ObsessionChange(30);
    }
}
