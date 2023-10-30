using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum HUD_type { Exp, Level, Health, Gold, Time };
    public HUD_type type;

    TextMeshProUGUI text;
    Slider slider;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        slider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch(type)
        {
            case HUD_type.Exp:
                {
                    slider.value = GameManager.Instance.GetPlayerExpRate();
                }
                break;
            case HUD_type.Level:
                {
                    text.text = string.Format("Lv.{0:D}",GameManager.Instance.level);
                }
                break;
            case HUD_type.Gold:
                {
                    text.text = GameManager.Instance.GetGoldToString();
                }
                break;
            case HUD_type.Health:
                {
                    slider.value = GameManager.Instance.GetPlayerHealthRate();
                }
                break;
            case HUD_type.Time: break;
            default: break;
        }
    }
}
