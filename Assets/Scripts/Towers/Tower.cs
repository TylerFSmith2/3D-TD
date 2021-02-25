using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    //TEMPORARY TEST FIELDS
    public GameObjectSet TowersSet;
    //-----------------------------

    public TowerObject stats;
    public List<GameObject> EnemyInRange = new List<GameObject>();

    GameObject target = null;
    float fireRate = 0.0f;
    bool isAttacking = false;
    bool fireRateReady = true;
    bool aimingAtTarget = false;

    [SerializeField]
    private Transform aimTransform;
    [SerializeField]
    private Transform fireTransform;
    [SerializeField]
    private GameObject bulletPrefab;

    //TowerSpot that holds this tower
    public TowerSpot myTowerSpot;


    public TowerFunctionalityMenu towerFuncMenu;
    //----MONO FUNCS--------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        TowersSet.Add(gameObject); //FOR TESTING PURPOSES
        towerFuncMenu = FindObjectOfType<TowerFunctionalityMenu>();
    }

    void Update()
    {
        CheckTarget();
        fireRateReady = FireRateCheck();
        aimingAtTarget = IsAimingAtTarget();

        if (!aimingAtTarget) { AimTowardsTarget(); }
        if (aimingAtTarget && fireRateReady)
        {
            FireAtTarget();
        }       
    }

    //----NON-MONO FUNCS--------------------------------------------------------

    //chooses the first enemy in the list
    public void ChooseFirstTarget()
    {
        if (EnemyInRange.Count > 0) target = EnemyInRange[0];
        else target = null;
    }

    public void AddEnemyToList(GameObject enemy)
    {
        EnemyInRange.Add(enemy);
        if (target == null)
            ChooseFirstTarget();
    }

    public void RemoveEnemyFromList(GameObject enemy)
    {
        if (EnemyInRange.Contains(enemy)) EnemyInRange.Remove(enemy);
    }

    //Updates the fire rate of the tower
    //returns true when the tower is ready to attack
    bool FireRateCheck()
    {        
        if(fireRate <= 0.0) 
        {
            return true;
        }
        else //Fire rate is on cooldown
        {
            fireRate -= Time.deltaTime;
        }
        return false;
    }

    //fires the bullet prefab at a target
    void FireAtTarget()
    {
        //if there's a target to attack...
        if (target)
        {
            //ATTACK link goes here
            //Debug.Log(name + " Attacking " + target.name);
            GameObject bullet = Instantiate(bulletPrefab, fireTransform.position, Quaternion.identity, null);
            Bullet bscript = bullet.GetComponent<Bullet>();
            bscript.damage = stats.damage.value;
            bscript.targetPos = target.transform.position;
            fireRate = stats.fireRate.value;
            //---Alternate method of firing the bullet at the target
                bscript.rb = bullet.GetComponent<Rigidbody>();

                //Using RigidBody.AddForce
                //Note, verify the bullet script has turned off all other fire methods
                bscript.addForceTowardsTarget();
            //---Alternate fire methods-------------------------End--
        }
    }

    //Checks if the current target is within it's range
    void CheckTarget()
    {
        //if the target isn't within the list then find a new one
        if (!EnemyInRange.Contains(target))
        {
            ChooseFirstTarget();
        }
    }

    //Checks to see if the aimPosition is looking at the target position
    //returns true when this is most likely the case
    bool IsAimingAtTarget()
    {
        if (target)
        {
            Vector3 targetDir = target.transform.position - aimTransform.position;
            float dotProd = Vector3.Dot(targetDir.normalized, aimTransform.forward);

            //most likely means the cannon is looking at the target object
            if (dotProd > 0.9)
            {
                return true;
            }
        }
        
        return false;        
    }

    //Rotate the aimPosition towards the target
    void AimTowardsTarget()
    {
        if (target)
        {
            Vector3 targetDir = target.transform.position - aimTransform.position;

            // The step size is equal to speed times frame time.
            float step = 6 * Time.deltaTime;

            Vector3 newDir = Vector3.RotateTowards(aimTransform.forward, targetDir, step, 0.0f);
            // Move our position a step closer to the target.
            aimTransform.rotation = Quaternion.LookRotation(newDir);
        }
        
    }

    public void RemoveDeadEnemyFromList()
    {
        foreach(GameObject enemy in EnemyInRange.ToArray())
        {
            if (enemy.GetComponent<EnemyAI>().currHP <= 0)
                EnemyInRange.Remove(enemy);
        }
    }

    public void OnMouseDown()
    {
        Debug.Log("Tower Clicked");
        towerFuncMenu.OpentowerFunctionalityMenu(this);
    }

    public void SellTower()
    {
        myTowerSpot.TowerRemoved();
        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        if (Selection.Contains(gameObject))
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, stats.range.value);
        }
    }
}
