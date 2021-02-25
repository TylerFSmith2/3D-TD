using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class enemy_state_afterAttack : State<EnemyAI>
{

    private static enemy_state_afterAttack instance;
    private enemy_state_afterAttack()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;

    }
    public static enemy_state_afterAttack Instance
    {
        get
        {
            if (instance == null)
            {
                new enemy_state_afterAttack();
            }
            return instance;
        }
    }

    //-------- Abstract State Functions -------------------

    public override void EnterState(EnemyAI owner)
    {
        Debug.Log("enemy entered afterAttack state");
        owner.isAttacking = false;
        if(Mathf.Abs(Vector3.Magnitude(owner.attackTarget - owner.transform.position)) < owner.stats.attackRange)
        {
            owner.currTarget = Vector3.Normalize(owner.transform.position - owner.attackTarget) * owner.stats.attackRange + owner.transform.position;
            owner.currTarget.y = owner.transform.position.y;
            Debug.Log(owner.currTarget);
            owner.stateMachine.ChangeState(enemy_state_follow.Instance);
        }
        else
        {
            owner.stateMachine.ChangeState(enemy_state_follow.Instance);
        }
    }

    public override void ExitState(EnemyAI owner)
    {
        
    }

    public override void UpdateState(EnemyAI owner)
    {
        
    }
}
