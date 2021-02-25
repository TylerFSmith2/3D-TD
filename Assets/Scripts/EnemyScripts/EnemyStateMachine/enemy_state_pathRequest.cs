using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class enemy_state_pathRequest : State<EnemyAI>
{

    private static enemy_state_pathRequest instance;
    private enemy_state_pathRequest()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;

    }
    public static enemy_state_pathRequest Instance
    {
        get
        {
            if (instance == null)
            {
                new enemy_state_pathRequest();
            }
            return instance;
        }
    }

    public override void EnterState(EnemyAI owner)
    {
        //Debug.Log("Entering PathRequest state");
        owner.RequestPath();
    }

    public override void ExitState(EnemyAI owner)
    {
        
    }

    public override void UpdateState(EnemyAI owner)
    {
        
    }

}
