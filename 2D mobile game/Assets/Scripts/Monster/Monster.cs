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

    [SerializeField] private GameObject player;
    [SerializeField] private float health = 100.0f;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool playerInMoveRange = false;
    private bool playerInAttackRange = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        state = State.Idle;
        fsm = new FSM(new Monster_IdleState(this));

        if(player.transform.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
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
            spriteRenderer.flipX = true;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        
        if(health <= 0.0f)
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

    public void PlayerInMoveRange(bool inRange)
    {
        playerInMoveRange = inRange;
    }

    public void PlayerInAttackRange(bool inRange)
    {
        playerInAttackRange = inRange;
    }
}
