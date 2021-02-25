using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class TD_state_wave : State<TD_GameManager>
{
    private static TD_state_wave instance;
    private TD_state_wave()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;

    }
    public static TD_state_wave Instance
    {
        get
        {
            if (instance == null)
            {
                new TD_state_wave();
            }
            return instance;
        }
    }

    //-------- Abstract State Functions -------------------

    public override void EnterState(TD_GameManager owner)
    {
        Debug.Log("GAME STATE ENTERED: TD_state_wave");
        owner.WaveManager.SetActive(true);
        owner.TowerManager.SetActive(true);
    }

    public override void ExitState(TD_GameManager owner)
    {
        
    }

    public override void UpdateState(TD_GameManager owner)
    {
        
    }

    //-------- Custom Functions --------------------------

}
