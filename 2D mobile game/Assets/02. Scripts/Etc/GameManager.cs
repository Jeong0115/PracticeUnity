using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("# GameObject #")]
    public ObjectPoolManager PoolManager;
    public Player player;

    public Canvas uiCanvas;

    public int level;
    public int exp;
    public BigInteger gold;

    public enum Equip_type { Attack, Health, Armor, Critical, Maximization, End };
    private int[] equipmentLevel = new int[(int)Equip_type.End];
    public BigInteger[] equipmentValue = new BigInteger[(int)Equip_type.End];

    public float equipmentValueRate;

    public BigInteger attackDamage = 10;

    public int[] maxExp = { 1, 2, 3, 4, 5, 6, 7 };

    private void Awake()
    {
        Instance = this;

        equipmentValueRate = 1.2f;
        for (int i=0; i< equipmentLevel.Length; i++)
        {
            EquipmentLevelUp((Equip_type)i);
        }

        gold = 1000000000;
        level = 0;
        exp = 0;
    }

    private void LateUpdate()
    {
        attackDamage = equipmentValue[(int)Equip_type.Attack];
    }

    public void EquipmentLevelUp(Equip_type type)
    {
        equipmentLevel[(int)type]++;
        equipmentValue[(int)type] = (BigInteger)(10 * Mathf.Pow((equipmentValueRate), equipmentLevel[(int)type]));
    }
    public string GetEquipmentLevelToString(Equip_type type)
    {
        return equipmentLevel[(int)type].ToString();
    }
    public int GetEquipmentLevel(Equip_type type)
    {
        return equipmentLevel[(int)type];
    }
    public string GetEquipmentValueToString(Equip_type type)
    {
        return Function.BigIntegerToString(equipmentValue[(int)type]);
    }
    public void GetExp(int _exp)
    {
        exp += _exp;

        if(exp >= maxExp[level])
        {
            exp -= maxExp[level];
            level++;
        }
    }

    public float GetPlayerHealthRate()
    {
        return player.health.GetHealthRate();
    }

    public float GetPlayerExpRate()
    {
        return (float)exp / (float)maxExp[level];
    }

    public void AddGold(BigInteger _gold)
    {
        gold += _gold;
    }

    public bool UseGold(BigInteger _gold)
    {
        if (_gold > gold)
        {
            return false;
        }
        else
        {
            gold -= _gold;
            return true;
        }
    }

    public string GetGoldToString()
    {
        return Function.BigIntegerToString(gold);
    }

    
}
