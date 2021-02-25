using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TowerObject : ScriptableObject
{
    public VarFloat fireRate;
    public VarInt damage;
    public VarFloat range;
    public Vector3 towerOffset = Vector3.zero;
    public VarInt cost;
}
