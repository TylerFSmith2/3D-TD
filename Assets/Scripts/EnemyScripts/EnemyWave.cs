using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyWave : ScriptableObject
{
    public GameObjectSet Prefabs;
    public int[] amount;
}
