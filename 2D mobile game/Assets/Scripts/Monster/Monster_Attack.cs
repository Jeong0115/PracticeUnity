using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Attack : MonoBehaviour
{
    [SerializeField] private float damage = 5.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }

}
