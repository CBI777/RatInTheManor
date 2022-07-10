using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem current;

    [SerializeField] private Tooltip tooltip;

    public void Awake()
    {
        current = this;
    }

    public static void ShowTooltip(string content, string header = "")
    {
        current.tooltip.SetText(content, header);
        current.tooltip.gameObject.SetActive(true);
    }
    public static void HideTooltip()
    {
        current.tooltip.gameObject.SetActive(false);
    }
}
