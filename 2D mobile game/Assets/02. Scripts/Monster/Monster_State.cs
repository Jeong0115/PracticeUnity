using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Monster_IdleState : BaseState
{
    public Monster_IdleState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
       
    }

    public override void OnStateUpdate()
    {

    }

    public override void OnStateExit()
    {

    }
}

public class Monster_MoveState : BaseState
{
    public Monster_MoveState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
        monster.GetComponent<Animator>().SetBool("isMove", true);
    }

    public override void OnStateUpdate()
    {
        monster.MoveToPlayer();
    }

    public override void OnStateExit()
    {
        monster.GetComponent<Animator>().SetBool("isMove", false);
    }
}

public class Monster_AttackState : BaseState
{
    public Monster_AttackState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
        monster.GetComponent<Animator>().SetBool("isAttack", true);
    }

    public override void OnStateUpdate()
    {

    }

    public override void OnStateExit()
    {
        monster.attackCollider.enabled = false;
        monster.GetComponent<Animator>().SetBool("isAttack", false);
    }
}

public class Monster_HitState : BaseState
{
    public Monster_HitState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
        monster.GetComponent<Animator>().SetTrigger("isHit");
        monster.KnockBack();
    }

    public override void OnStateUpdate()
    {

    }

    public override void OnStateExit()
    {

    }
}

public class Monster_DeadState : BaseState
{
    public Monster_DeadState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
        monster.GetComponent<Animator>().SetTrigger("isDead");
        GameManager.Instance.GetExp(monster.Exp);
        GameManager.Instance.AddGold((BigInteger)monster.Gold);

        StageManager.Instance.AddKillScore();
    }

    public override void OnStateUpdate()
    {

    }

    public override void OnStateExit()
    {

    }
}