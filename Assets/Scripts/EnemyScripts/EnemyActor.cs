using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyActor : ScriptableObject
{
    public VarInt maxHP;
    public VarFloat moveSpeed;
    public VarString Name;

    public float attackRate;

    public float viewDistance; //Distance enemy can see in front of them
    public float viewAngle;    //Angle the enemy can see in front of them
    public float attackRange;  //the enemy can attack when within this distance to a target
}
