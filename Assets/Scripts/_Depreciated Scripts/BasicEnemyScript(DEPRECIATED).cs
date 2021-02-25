using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Basic Enemy Script really only exists for testing things, not intended for use
//Just follows the player until they are in range
public class BasicEnemyScript : MonoBehaviour
{
    public int health = 50;
    int MoveSpeed = 4;
    Transform PlayerTransf;
    int MaxDist = 10;
    int MinDist = 5;
    void Start()
    {
        //Retrieves the Player Transform
        PlayerTransf = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(PlayerTransf);

        if(Vector3.Distance(transform.position, PlayerTransf.position) >= MinDist)
        {
            //Chases the Player
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;

            if(Vector3.Distance(transform.position, PlayerTransf.position) <= MaxDist)
            {
                //Call Actions that should be preformed when the enemy is in range
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
