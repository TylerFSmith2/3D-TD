using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float force;
    public int damage;
    public Vector3 targetPos;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //moveToTarget();        
    }

    //---------------------------------------
    //---- Methods of Moving towards target--
    //---------------------------------------

    //Using RigidBody.Force
    public void addForceTowardsTarget()
    {
        rb.AddForce((targetPos - transform.position).normalized * force, ForceMode.Force);
    }

    //Using Vector3.MoveTowards
    void moveToTarget()
    {
        float step = 150 * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
    }

    //---- Methods of Moving towards target --End--

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyAI>().TakeDamage(damage, 150f);
            Destroy(gameObject);
        }
    }
}
