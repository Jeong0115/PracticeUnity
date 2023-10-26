using System.Collections;
using System.Collections.Generic;
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
    public int[] maxExp = { 1, 2, 3, 4, 5, 6, 7 };

    private void Awake()
    {
        level = 0;
        exp = 0;
        Instance = this;
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
}
