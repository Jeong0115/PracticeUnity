using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_attack : MonoBehaviour
{

    [SerializeField] private float damage = 10.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Monster monster = collision.gameObject.GetComponent<Monster>();
        if (monster != null)
        {
            monster.TakeDamage(damage);
        }
    }

}
