using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public GameObjectSet TowersSet;
    public GameObjectSet EnemiesSet;

    //----MONO FUNCS--------------------------------------------------------

    private void Awake()
    {
        //if (TowersSet.items.Count > 0)
            //TowersSet.items.RemoveRange(0, TowersSet.items.Count);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DistanceToTower();
    }

    //----Custom Functions--------------------------------------------------------

    //Checks every enemies distance to every tower
    //Adds enemies to a tower's queue if in range
    void DistanceToTower()
    {
        //If there are towers and enemies to check...
        if(TowersSet.items.Count > 0 && EnemiesSet.items.Count > 0) 
        {
            //for each tower, check its position to each enemy
            foreach(GameObject tower in TowersSet.items) 
            {
                Tower towerScript = tower.GetComponent<Tower>();
                foreach(GameObject enemy in EnemiesSet.items)
                {                    
                    //If the enemy isn't within this tower's list...
                    if (!towerScript.EnemyInRange.Contains(enemy)) 
                    {
                        //If enemy is within range, add it to the list
                        if (Vector3.Distance(tower.transform.position, enemy.transform.position) <= towerScript.stats.range.value)
                        {
                            towerScript.AddEnemyToList(enemy);                       
                        }
                    }
                    else
                    {
                        //the enemy walked out of range of the tower
                        if(Vector3.Distance(tower.transform.position, enemy.transform.position) > towerScript.stats.range.value) 
                        {
                            towerScript.EnemyInRange.Remove(enemy);
                        }
                    }
                }
                
            }
        }
    }
}
