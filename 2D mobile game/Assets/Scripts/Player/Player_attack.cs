using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_attack : MonoBehaviour
{
    [SerializeField] private float damage = 10.0f;
    [SerializeField] private Player player;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        float realDamage = damage;
        float minDamage = player.DamageRange * player.Maximization - player.DamageRange / 2.0f;

        float damageMultiplier = Random.Range(minDamage, player.DamageRange) + 1.0f;
        realDamage*= damageMultiplier;

        if(Random.value <= player.ChriticalChance)
        {
            realDamage *= 2.0f;
        }

        // 데미지 공식 : 기본 데미지 * 극대화 수치 (크리티컬 발생시 *2)
        // 극대화 수치 :  Random(-player.DamageRange / 2 + 1, player.DamageRange / 2 + 1)
        // player.Maximization 값이 1에 가까워 질수록 극대화 최소 수치가 극대화 최대 수치에 가까워짐

        Monster monster = collision.gameObject.GetComponent<Monster>();
        if (monster != null)
        {
            monster.TakeDamage(realDamage);
        }
    }

}
