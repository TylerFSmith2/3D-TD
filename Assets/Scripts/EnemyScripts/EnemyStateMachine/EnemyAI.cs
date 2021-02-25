using UnityEngine;
using StateMachine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{

    public StateMachine<EnemyAI> stateMachine { get; set; }

    //Enemy STATS SO
    public EnemyActor stats;

    //Eventually these two parameters will get moved to somewhere else
    //probably implemented into a state machine of some sort
    public GameObjectSet InactiveList;
    public GameObjectSet ActiveList;
    public GameEvent EnemyDeath;

    //Target vectors, places this enemy is interested in
    public Vector3 moveTarget;
    public Vector3 currTarget;
    public Vector3 attackTarget;
    //[HideInInspector]
    public Vector3[] movePath;
    public int moveTargetIndex = -1;

    //Layermasks
    public LayerMask attackableMask;
    public LayerMask obstacleMask;

    //Other components
    public Rigidbody rb;

    //Timers
    public float attackTimer = 0;
    // - For when a general state needs to have a timed delay between states, this variable can be most useful
    public float timeDelay = 0;

    public bool isAttacking = false;
    public bool switchState = false;
    private bool interuptState = false;
    public bool InteruptState
    {
        get { return interuptState; }
        set { interuptState = value; }
    }

    [SerializeField]
    public int currHP;

    //==========================
    //======= Functions ========
    //==========================

    private void OnEnable()
    {
        currHP = stats.maxHP.value;
        attackTimer = 0;
        timeDelay = 0;
    }


    //----------------- Custom Functions -------------------------


    public void UpdateAttackTarget(Vector3 target)
    {
        moveTarget = target;
        currTarget = moveTarget;
    }

    public void TakeDamage(int damage, float? knockback)
    {
        currHP -= damage;
        stateMachine.HaltState();
        if(knockback != null)
        {
            rb.AddForce(-transform.forward.x * (float)knockback, 150f, -transform.forward.z * (float)knockback);
        }
             

        if (currHP <= 0)
        {
            StopAllCoroutines();
            EnemyDeath.Raise();
            //InactiveList.Add(gameObject);
            //ActiveList.Remove(gameObject);
            //gameObject.SetActive(false);
        }
        StartCoroutine(InterruptPhase());
    }

    // waits for 1/3 of a second and then returns the enemy to the FOLLOW state
    IEnumerator InterruptPhase()
    {
        Debug.Log("Interupting enemy phase");
        yield return new WaitForSeconds(0.33f);
        stateMachine.ChangeState(enemy_state_follow.Instance);
    }

    public void Startup()
    {
        rb = GetComponent<Rigidbody>();
        currHP = stats.maxHP.value;
    }

    public void RequestPath()
    {
        if (currTarget != null)
        {
            PathRequestManager.RequestPath(new PathRequest(transform.position, currTarget, OnPathFound));
        }        
    }
    // __        __        __
    // ||        ||        ||
    // ||        ||        ||
    // \/        \/        \/
    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            this.movePath = newPath;
            this.moveTargetIndex = 0;
            this.stateMachine.ChangeState(enemy_state_follow.Instance);
        }        
    }
    
    private void OnDrawGizmos()
    {
        //Draw's the enemy's field of vision
        Vector3 fovLine1 = Quaternion.AngleAxis(stats.viewAngle, transform.up) * transform.forward * stats.viewDistance;
        Vector3 fovLine2 = Quaternion.AngleAxis(-(stats.viewAngle), transform.up) * transform.forward * stats.viewDistance;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, fovLine1);
        Gizmos.DrawRay(transform.position, fovLine2);

        Gizmos.color = Color.red;
        if(isAttacking == true)
            Gizmos.DrawRay(transform.position, (attackTarget - transform.position).normalized * stats.attackRange);

        Gizmos.color = Color.black;
        foreach(Vector3 point in movePath)
        {
            Gizmos.DrawCube(point, Vector3.one * 0.5f);
        }
        
        //-------------------------------------
    }
    
}

