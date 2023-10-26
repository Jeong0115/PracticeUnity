using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_AttackRange : MonoBehaviour
{
    [SerializeField] private Monster monster;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            monster.PlayerInAttackRange(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            monster.PlayerInAttackRange(false);
        }
    }

}
