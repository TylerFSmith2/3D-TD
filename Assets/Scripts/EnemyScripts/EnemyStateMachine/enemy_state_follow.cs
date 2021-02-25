using UnityEngine;
using StateMachine;
using System.Collections.Generic;

public class enemy_state_follow : State<EnemyAI>
{
    private static enemy_state_follow instance;
    private enemy_state_follow()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;

    }
    public static enemy_state_follow Instance
    {
        get
        {
            if (instance == null)
            {
                new enemy_state_follow();
            }
            return instance;
        }
    }

    //-------- Abstract State Functions -------------------

    public override void EnterState(EnemyAI owner)
    {
        //Debug.Log("Entering enemy_follow");
    }

    public override void ExitState(EnemyAI owner)
    {
        //Debug.Log("Exiting enemy_follow");
    }

    public override void UpdateState(EnemyAI owner)
    {
        FollowPath(owner);
        if(owner.attackTarget == Vector3.zero)
        {
            SeekAttackTarget(owner);
        }
        else
        {
            //if attack target is within the owner's view distance, move towards that target
            if(Vector3.Distance(owner.transform.position, owner.attackTarget) <= owner.stats.viewDistance && owner.isAttacking == false)
            {
                //Debug.Log("attack target is within view range...");
                owner.isAttacking = true;
                owner.currTarget = owner.attackTarget;
                owner.stateMachine.ChangeState(enemy_state_pathRequest.Instance);
            }
        }
        
    }

    //-------- Custom Functions --------------------------

    private void LookAtTarget(EnemyAI owner, Vector3 currWaypoint)
    {
        Vector3 targetDir = currWaypoint - owner.transform.position;
        Vector3 localTarget = owner.transform.InverseTransformPoint(currWaypoint);

        float angle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;

        Vector3 eulerAngularVelocity = new Vector3(0, angle, 0) * 10;
        Quaternion deltaRotation = Quaternion.Euler(eulerAngularVelocity * Time.deltaTime);
        owner.rb.MoveRotation(owner.rb.rotation * deltaRotation);
    }


    //checks to see if an attackable target is within eye sight to this enemy
    private void SeekAttackTarget(EnemyAI owner)
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(owner.transform.position, owner.stats.viewDistance, owner.attackableMask);
        for(int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Vector3 target = targetsInViewRadius[i].transform.position;
            Vector3 dirToTarget = (target - owner.transform.position).normalized;
            //if the target is in view angle of the owner...
            if (Vector3.Angle(owner.transform.forward, dirToTarget) <= owner.stats.viewAngle / 2)
            {
                float distToTarget = Vector3.Distance(owner.transform.position, target);
                //and the target is visible, enemy has an attack target
                if(!Physics.Raycast(owner.transform.position, dirToTarget, distToTarget, owner.obstacleMask))
                {
                    //Debug.Log("i can see an attack target");
                    owner.attackTarget = target;
                }
            }
        }
    }

    private void FollowPath(EnemyAI owner)
    {
        if(owner.moveTargetIndex > -1)
        {
            Vector3 currentWaypoint = owner.movePath[owner.moveTargetIndex];
            if (SameWorldPos(currentWaypoint, owner.transform.position))
            {
                owner.moveTargetIndex++;
                if (owner.moveTargetIndex >= owner.movePath.Length)
                {
                    //Owner needs to find a new path...
                    //Debug.Log("Owner needs a new path...");
                    owner.stateMachine.ChangeState(enemy_state_pathRequest.Instance);
                }
            }
            LookAtTarget(owner, currentWaypoint);
            owner.transform.position = Vector3.MoveTowards(owner.transform.position, owner.movePath[owner.moveTargetIndex], owner.stats.moveSpeed.value);
        }
        else
        {
            Debug.Log("Has no movePath");
        }
            
    }

    bool SameWorldPos(Vector3 a, Vector3 b)
    {
        if(a.x == b.x && a.z == b.z && a.y == b.y)
        {
            return true;
        }
        return false;
    }
}
 
