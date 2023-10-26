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

        float damageMultiplier = Random.Range(minDamage, player.DamageRange / 2.0f) + 1.0f;
        realDamage*= damageMultiplier;

        bool isChritcal = false;
        if (Random.value <= player.CriticalChance)
        {
            isChritcal = true;
            realDamage *= 2.0f;
        }

        // ������ ���� : �⺻ ������ * �ش�ȭ ��ġ (ũ��Ƽ�� �߻��� *2)
        // �ش�ȭ ��ġ :  Random(-player.DamageRange / 2 + 1, player.DamageRange / 2 + 1)
        // player.Maximization ���� 1�� ����� ������ �ش�ȭ �ּ� ��ġ�� �ش�ȭ �ִ� ��ġ�� �������

        Monster monster = collision.gameObject.GetComponent<Monster>();
        if (monster != null)
        {
            monster.TakeDamage(realDamage, isChritcal);
        }
    }

}