using StateMachine;
using UnityEngine;

public class slime_state_attack : State<EnemyAI>
{
    private static slime_state_attack instance;
    private slime_state_attack()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;

    }

    public static slime_state_attack Instance
    {
        get
        {
            if (instance == null)
            {
                new slime_state_attack();
            }
            return instance;
        }
    }

    //-------- Abstract State Functions -------------------

    public override void EnterState(EnemyAI owner)
    {
        //Debug.Log("Entering slime_attack");
        owner.timeDelay = 1.5f;
        owner.isAttacking = false;
        ThrowAtPlayer(owner);
    }

    public override void ExitState(EnemyAI owner)
    {
        //Debug.Log("Exiting slime_attack");
        owner.timeDelay = 0;
        owner.attackTimer = owner.stats.attackRate;
    }

    public override void UpdateState(EnemyAI owner)
    {
        owner.timeDelay -= Time.deltaTime;
        if (owner.timeDelay <= 0)
            owner.stateMachine.ChangeState(enemy_state_afterAttack.Instance);
    }

    //-------- Custom Functions --------------------------

    private void ThrowAtPlayer(EnemyAI owner)
    {
        //float forceMult = Random.Range(30, 50);
        float forceMult = 1000f;
        owner.rb.AddForce(owner.transform.forward * forceMult + owner.transform.up * (forceMult * .4f));
    }
}
