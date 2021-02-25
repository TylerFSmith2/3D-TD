using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyWaveManager : MonoBehaviour
{
    public GameObjectSet ActiveEnemies;
    public GameObjectSet InactiveEnemies;
    public EnemyWaveSet Wave; //a collection of all the waves for this level

    public GameObject[] SpawnPoints; //keeps track of all the spawn points in this level
    public GameObject PrimaryEnemyTarget; // the initial value that enemiew want to find a path to when spawned
    private int[] spawnPointAmounts = null;

    public VarInt enemyTotal;   //counts how many enemies exist before and during waves
    public VarInt waveCount;    //UI variable | counts which wave is currently happening
    public VarInt maxWaveCount; //UI variable | determines the max amount of waves for this level

    public GameEvent ReachedLastWave; // this event happens(raised) when the game has reached the last wave

    private int currWave; //counts which wave for array index

    // Start is called before the first frame update
    void Start()
    {
        if (ActiveEnemies.items.Count != 0)
            ActiveEnemies.items.RemoveRange(0, ActiveEnemies.items.Count);
        if (PrimaryEnemyTarget == null)
            Debug.Log("ERROR ERROR ERROR |  No primary enemy target object");
        spawnPointAmounts = new int[SpawnPoints.Length];
        waveCount.value = 1;
        maxWaveCount.value = Wave.items.Count;
        currWave = 0;
        GetWaveTotal(currWave); //gets the total enemy count for the first wave
        CreateEnemies(currWave);
        DesignateSpawnLocation();
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    //creates all enemies within the first wave
    void CreateEnemies(int waveIndex)
    {      
        for(int i = 0; i < Wave.items[waveIndex].amount.Length; i++)
        {
            for(int j = 0; j < Wave.items[waveIndex].amount[i]; j++)
            {
                GameObject enemy = Instantiate(Wave.items[i].Prefabs.items[i]);
                InactiveEnemies.Add(enemy);
                enemy.SetActive(false);
            }
        }
    }

    //counts how many enemies exist in this wave
    void GetWaveTotal(int waveIndex)
    {
        Debug.Log("Spawning Wave " + waveIndex);
        enemyTotal.value = 0;
        for (int i = 0; i < Wave.items[waveIndex].amount.Length; i++)
        {
            enemyTotal.value += Wave.items[waveIndex].amount[i];
        }
    }

    void DesignateSpawnLocation()
    {
        Debug.Log("Designate Spawn Location");
        for(int i = InactiveEnemies.items.Count-1; i >= 0 ; i--)
        {
            GameObject enemy = InactiveEnemies.items[i];
            enemy.GetComponent<EnemyAI>().currTarget = PrimaryEnemyTarget.transform.position;
            SpawnPoints[0].GetComponent<EnemySpawner>().QueueEnemy(enemy);
            ActiveEnemies.Add(enemy);
            InactiveEnemies.Remove(InactiveEnemies.items[i]);
        }
    }

    /// this function was meant for object pooling
    /// right now it DOESNT DO SHIT
    private bool CheckEnemyPoolForMatch(GameObject prefab)
    {
        for (int i = 0; i < InactiveEnemies.items.Count; i++)
        {
            if (prefab.Equals(PrefabUtility.GetCorrespondingObjectFromSource(InactiveEnemies.items[i])))
            {
                Debug.Log("there's a match");
                return true;
            }
        }
        Debug.Log("nada");
        return false;
    }

    //This function is meant to be called by a game event listener when an enemy dies
    public void OnEnemyDeath()
    {
        enemyTotal.value--;
        //Debug.Log("Enemy Died. TOTAL: " + enemyTotal.value);
        if (enemyTotal.value == 0)
        {
            //Debug.Log("All enemies died...");
            SpawnNextWave();
        }
    }

    //Queues the next wave to be spawned
    public void SpawnNextWave()
    {
        currWave++;
        waveCount.value++;
        //Debug.Log("Spawning NEXT Wave");
        GetWaveTotal(currWave);
        CreateEnemies(currWave);
        DesignateSpawnLocation();
        if(waveCount.value == maxWaveCount.value)
        {
            ReachedLastWave.Raise();
        }
    }
}

