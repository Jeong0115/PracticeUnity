using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static GameManager;

public class EquipmentHUD : MonoBehaviour
{
    public enum EuqipHUD_type { level, value };

    public GameManager.Equip_type type;
    public EuqipHUD_type hud_type;

    TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        switch (hud_type)
        {
            case EuqipHUD_type.level:
                {
                    text.text = string.Format("Level " + GameManager.Instance.GetEquipmentLevelToString(type));
                }
                break;

            case EuqipHUD_type.value:
                {
                    text.text = GameManager.Instance.GetEquipmentValueToString(type);
                }
                break;

            default:
                break;
        }
    }
}
