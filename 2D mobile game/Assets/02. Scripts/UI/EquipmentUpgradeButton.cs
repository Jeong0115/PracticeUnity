using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EquipmentUpgradeButton : MonoBehaviour
{
    public GameManager.Equip_type type;

    public BigInteger cost = 10;
    public float equipmentCostRate = 1.1f;

    TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void LateUpdate()
    {
        text.text = Function.BigIntegerToString(cost);
    }

    public void OnClick()
    {
        bool isLevelUp = false;

        if (GameManager.Instance.UseGold(cost))
        {
            GameManager.Instance.EquipmentLevelUp(type);
            isLevelUp = true;
        }

        if (isLevelUp)
        {
            cost = (BigInteger)(10 * Mathf.Pow(equipmentCostRate, GameManager.Instance.GetEquipmentLevel(type)));
        }
    }
}
