using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class TD_GameManager : MonoBehaviour
{
    public StateMachine<TD_GameManager> stateMachine { get; set; }

    public GameObject WaveManager;
    public GameObject TowerManager;

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = new StateMachine<TD_GameManager>(this);
        stateMachine.ChangeState(TD_state_wave.Instance);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
