using UnityEngine;
using StateMachine;

public class SlimeAI : EnemyAI
{

    private void Awake()
    {
        stateMachine = new StateMachine<EnemyAI>(this);
        Startup();
    }
    private void Start()
    {
        stateMachine.ChangeState(enemy_state_pathRequest.Instance);
    }
    // Update is called once per frame
    void Update()
    {
        //Gonna need this line
        stateMachine.Update();


        if (currTarget == attackTarget)
        {
            if (Vector3.Distance(transform.position, attackTarget) <= stats.attackRange && attackTimer <= 0)
            {
                //Debug.Log("Target withing range: " + stats.attackRange);
                //Debug.Log("Attacking target");
                stateMachine.ChangeState(slime_state_attack.Instance);
            }
        }
        // Timer
        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;
    }
}
