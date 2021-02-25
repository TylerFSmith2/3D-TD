using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendPoint : MonoBehaviour
{
    public VarInt MaxHealth;
    public VarInt CurrHealth;

    // Start is called before the first frame update
    void Start()
    {
        CurrHealth.value = MaxHealth.value;
    }

    public void TakeDamage(int damage)
    {
        CurrHealth.value -= damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            //collision.gameObject.GetComponent<EnemyAI>().TakeDamage(9000, 0);
        }
    }
}
