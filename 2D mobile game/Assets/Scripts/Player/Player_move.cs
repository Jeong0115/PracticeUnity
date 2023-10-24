using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_move : MonoBehaviour
{
    private Transform targetMonster = null;
    private Animator animator;

    [SerializeField] private Collider2D normalAttackCollider;

    public float attackRange = 1.0f;
    public float moveSpeed = 0.5f;

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

        if (targetMonster != null)
        {
            Debug.Log("target distance : " + Vector3.Distance(transform.position, targetMonster.position));
        }
    }

    private void detect() 
    {
        FindClosestMonster();

        if(targetMonster != null)
        {
            state = State.Move;
            animator.SetBool("isMove", true);
        }
    }

    private void move()
    {
        if (targetMonster != null)
        {
            if (!targetMonster.gameObject.activeInHierarchy)
            {
                targetMonster = null;
                state = State.Detect;
                animator.SetBool("isMove", false);
                return;
            }

            transform.position += (targetMonster.position - transform.position).normalized * moveSpeed * Time.deltaTime;

            float distanceToTarget = Vector3.Distance(transform.position, targetMonster.position);

            if (distanceToTarget <= attackRange)
            {
                animator.SetBool("isMove", false);
                state = State.Attack;
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
        if (!targetMonster.gameObject.activeInHierarchy)
        {
            targetMonster = null;
            state = State.Detect;
            animator.SetBool("isAttack", false);
        }

        animator.SetBool("isAttack", true);

    }

    private void wait()
    {

    }

    // 지금 모든 몬스터를 확인 후 거리 비교
    void FindClosestMonster()
    {
        // 모든 'Monster' 태그를 가진 게임 오브젝트를 찾습니다.
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

        float closestDistance = Mathf.Infinity; // 초기 거리는 무한대로 설정
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

        targetMonster = closestMonster;
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
}
