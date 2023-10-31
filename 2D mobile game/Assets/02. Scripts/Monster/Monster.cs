using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private enum State
    {
        Idle,
        Move,
        Attack,
        Hit,
        Dead
    }

    private State state;
    private FSM fsm;

    public Collider2D attackCollider;

    [SerializeField] private Player player;
    [SerializeField] private float moveSpeed = 0.2f;
    [SerializeField] private GameObject text;
    [SerializeField] private Transform texPos;

    public int Exp { get; private set; } = 1;
    public int Gold = 1545;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigid;

    private Health health;

    private bool playerInMoveRange = false;
    private bool playerInAttackRange = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();

        state = State.Idle;
        fsm = new FSM(new Monster_IdleState(this));

        //if(player.transform.position.x < transform.position.x)
        //{
        //    spriteRenderer.flipX = true;
        //}
    }

    void Update()
    {
        switch (state)
        { 
            case State.Idle:
                {
                    if(playerInMoveRange)
                    {
                        ChangeState(State.Move);
                    }
                }
                break;
            case State.Move:
                {
                    if (playerInAttackRange)
                    {
                        ChangeState(State.Attack);
                    }
                }
                    break;
            case State.Attack:
                {
                    if (!playerInAttackRange)
                    {
                        ChangeState(State.Move);
                    }
                }
                break;

            case State.Hit: break;
            case State.Dead: break;

            default: break;
        }

        fsm.UpdateState();

        if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void OnEnable()
    {
        state = State.Idle;
        player = GameManager.Instance.player;
    }

    public void TakeDamage(float damage, bool critical)
    {
        health.Hit(damage);
        GameObject damageText = Instantiate(text);
        damageText.transform.position = texPos.position;

        if (critical)
        {
            damageText.GetComponent<FloatingText>().Print(damage, Color.yellow);
        }
        else
        {
            damageText.GetComponent<FloatingText>().Print(damage);
        }

        if (health.GetHealthRate() <= 0.0f)
        {
            ChangeState(State.Dead);
        }
        else
        {
            ChangeState(State.Hit);
        }
    }

    private void ChangeState(State nextState)
    {
        state = nextState;

        switch (state)
        {
            case State.Idle:    fsm.ChangeState(new Monster_IdleState(this));   break;
            case State.Attack:  fsm.ChangeState(new Monster_AttackState(this)); break;
            case State.Move:    fsm.ChangeState(new Monster_MoveState(this));   break;
            case State.Hit:     fsm.ChangeState(new Monster_HitState(this));    break;
            case State.Dead:    fsm.ChangeState(new Monster_DeadState(this));   break;
            default: break;
        }
    }

    private void HitAnimationEnd()
    {
        if (playerInAttackRange)
        {
            ChangeState(State.Attack);
        }
        else
        {
            if(playerInMoveRange)
            {
                ChangeState(State.Move);
            }
            else
            {
                ChangeState(State.Idle);
            }
        }
    }
    private void DeadAnimationEnd()
    {
        this.gameObject.SetActive(false);
    }

    public void MoveToPlayer()
    {
        transform.position += (player.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime;
    }

    public void KnockBack()
    {
        Vector3 dir = (transform.position - player.transform.position).normalized;
        rigid.AddForce(dir * 2.0f, ForceMode2D.Impulse);
    }

    public void PlayerInMoveRange(bool inRange)
    {
        playerInMoveRange = inRange;
    }
    public void PlayerInAttackRange(bool inRange)
    {
        playerInAttackRange = inRange;
    }

    private void AttackColliderOn()
    {
        attackCollider.enabled = true;
    }
}
