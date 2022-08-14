using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Result_StatusManager : MonoBehaviour
{
    [SerializeField] private int sanity; //이성. 한계 이하로 줄어들면 일정 수준만큼 광기와 집착이 된다.
    private int sanityMax; //이성의 최대치. 이성이 전부 줄어들면 여기서부터 다시시작.
    private int sanityMin; //이성의 한계.
    private int sanityBoundary; //이성이 다음에도 남는 최소 수치

    [SerializeField] private int madness; //광기. 최대치를 넘어서게 되면 미쳐버린다.
    private int madnessMax; //광기의 최대치.
    [SerializeField] private int obsession; //(삶에 대한) 집착. 한계 이상으로 쌓이게 되면 의지나 나약이 되어버린다.
    private int obsessionMax;

    public int getSanity() { return sanity; }
    public int getMadness() { return madness; }
    public int getObsession() { return obsession; }

    private int madnessSub; //이성이 광기로 치환되는 양
    private int obsessionSub; //이성이 광기로 치환되는 양

    [SerializeField] private TextMeshProUGUI sanityText;
    [SerializeField] private TextMeshProUGUI madnessText;
    [SerializeField] private TextMeshProUGUI obsessionText;
    [SerializeField] private Slider SanitySlider;
    [SerializeField] private Slider MadnessSlider;
    [SerializeField] private Slider ObsessionSlider;
    [SerializeField] private Image SanityBar;
    [SerializeField] private Image MadnessBar;
    [SerializeField] private Image ObsessionBar;

    public static event Action ObsessionMaxEvent;
    public static event Action MadnessMaxEvent;

    private void OnEnable()
    {
        Supply_Base.SupplySanityChange += SanityChange;
        Supply_Base.SupplyMadnessChange += MadnessChange;
        Supply_Base.SupplyObsessionChange += ObsessionChange;
        Hallucination_Base.HalluMadnessChange += MadnessChange;
        Hallucination_Base.HalluSanityChange += SanityChange;
        Hallucination_Base.HalluObsessionChange += ObsessionChange;
    }

    private void OnDisable()
    {
        Supply_Base.SupplySanityChange -= SanityChange;
        Supply_Base.SupplyMadnessChange -= MadnessChange;
        Supply_Base.SupplyObsessionChange -= ObsessionChange;
        Hallucination_Base.HalluMadnessChange -= MadnessChange;
        Hallucination_Base.HalluSanityChange -= SanityChange;
        Hallucination_Base.HalluObsessionChange -= ObsessionChange;
    }

    private void sanitySubstitution()
    {
        if ((madness + madnessSub) >= 100)
        {
            setSanity(0);
            MadnessChange(madnessSub);
        }
        else
        {
            if (sanity <= sanityBoundary)
            {
                SanityChange((-1 * sanityMax));
            }
            else
            {
                setSanity(sanityMax);
            }
            MadnessChange(madnessSub);
            ObsessionChange(obsessionSub);
        }
    }

    private void ObsessionChange(int n)
    {
        this.obsession += n;

        this.ObsessionBar.color = new Color(1f, 185f / 255f, 75 / 255f);

        if (this.obsession <= 0) { this.obsession = 0; this.ObsessionBar.gameObject.SetActive(false); }
        else { if (!this.ObsessionBar.gameObject.activeSelf) { this.ObsessionBar.gameObject.SetActive(true); } }
        if (this.obsession >= obsessionMax)
        {
            this.obsession = 0;
            ObsessionMaxEvent?.Invoke();
        }

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
            sanitySubstitution();
        }

        if (!this.SanityBar.gameObject.activeSelf) { this.SanityBar.gameObject.SetActive(true); }
        this.SanitySlider.value = ((float)this.sanity / sanityMax);

        this.sanityText.SetText(this.sanity + " / " + sanityMax);


    }
    private void setSanity(int n)
    {
        this.sanity = n;

        if (this.sanity < sanityMin) { this.sanity = sanityMin; }
        else if (this.sanity > sanityMax) { this.sanity = sanityMax; }

        if (!this.SanityBar.gameObject.activeSelf) { this.SanityBar.gameObject.SetActive(true); }
        this.SanitySlider.value = ((float)this.sanity / sanityMax);

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

        if(madness >= madnessMax)
        {
            MadnessMaxEvent?.Invoke();
        }
    }

    private void Start()
    {
        this.madnessMax = 100;
        this.sanityMax = 100;
        this.obsessionMax = 100;
        this.sanityMin = -50;
        this.sanityBoundary = -30;
        this.sanity = 100;
        this.madness = 0;
        this.obsession = 0;
        this.madnessSub = 10;
        this.obsessionSub = 20;

        SanityChange(95);
        MadnessChange(70);
        ObsessionChange(95);
    }
}
