using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Transform target = null;
    private SpriteRenderer spriteRenderer = null;
    private Animator animator;
    
    public Health health;

    [SerializeField] private Collider2D normalAttackCollider;
    [SerializeField] private GameObject text;
    [SerializeField] private Transform texPos;

    public float attackRange = 1.0f;
    public float moveSpeed = 0.5f;

    public float CriticalChance { get; private set; } = 0.1f;
    public float Maximization { get; private set; } = 0.0f;
    public float DamageRange { get; private set; } = 0.6f; // �� ���� ���� ��ŭ �⺻ ���������� ���� ����

    private enum State
    {
        Detect,
        Move,
        Attack,
        Wait,
    }

    private State state;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        state = State.Detect;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Detect: detect(); break;
            case State.Move: move(); break;
            case State.Attack: attack(); break;
            case State.Wait: wait(); break;
            default: break;

        }

        if (target != null)
        {
            if (transform.position.x > target.transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private void detect() 
    {
        FindClosestMonster();

        if(target != null)
        {
            state = State.Move;
            animator.SetBool("isMove", true);
        }
    }

    private void move()
    {
        if (target != null)
        {
            if (!target.gameObject.activeInHierarchy)
            {
                target = null;
                state = State.Detect;
                animator.SetBool("isMove", false);
                return;
            }

            transform.position += (target.position - transform.position).normalized * moveSpeed * Time.deltaTime;

            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget <= attackRange)
            {
                animator.SetBool("isMove", false);
                animator.SetBool("isAttack", true);
                state = State.Attack;
            }
            else
            {
                animator.SetBool("isAttack", false);
            }
        }
        else
        {
            animator.SetBool("isMove", false);
            state = State.Detect;
        }
    }

    private void attack()
    {
        
    }

    private void wait()
    {

    }

    public void TakeDamage(float damage)
    {
        health.Hit(damage);

        GameObject damageText = Instantiate(text);
        damageText.transform.position = texPos.position;
        damageText.GetComponent<FloatingText>().Print(string.Format("{0:D}", (int)damage), Color.red);

        if (health.GetHealthRate() <= 0.0f)
        {
        }
    }

    // ���� ��� ���͸� Ȯ�� �� �Ÿ� ��
    void FindClosestMonster()
    {
        // ��� 'Monster' �±׸� ���� ���� ������Ʈ�� ã���ϴ�.
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

        float closestDistance = Mathf.Infinity; // �ʱ� �Ÿ��� ���Ѵ�� ����
        Transform closestMonster = null;

        foreach (GameObject monster in monsters)
        {
            float distance = Vector3.Distance(transform.position, monster.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestMonster = monster.transform;
            }
        }

        target = closestMonster;
    }

    private void TurnNormalAttackCollider(int turn)
    {
        if(turn == 0)
        {
            normalAttackCollider.enabled = false;
        }
        else if(turn == 1)
        {
            normalAttackCollider.enabled = true;
        }
    }
    
    private void AttackAnimationEnd()
    {
        if (!target.gameObject.activeInHierarchy)
        {
            target = null;
            state = State.Detect;
            animator.SetBool("isAttack", false);
        }
        else
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget > attackRange)
            {
                animator.SetBool("isMove", true);
                animator.SetBool("isAttack", false);
                state = State.Move;
            }
            else
            {
                animator.SetBool("isAttack", true);
            }
        }
    }
}