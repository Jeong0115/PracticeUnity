using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float curHealth = 100.0f;
    [SerializeField] private float maxHealth = 100.0f;

    private void OnEnable()
    {
        curHealth = maxHealth;
    }

    public void Hit(float damage)
    {
        curHealth -= damage;
    }

    public float GetHealthRate()
    {
        return curHealth / maxHealth;
    }

    public void SetMaxHp()
    {
        curHealth = maxHealth;
    }
}
