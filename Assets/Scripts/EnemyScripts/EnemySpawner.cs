using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    float x = 1, y = 1;

    [SerializeField]
    private float spawnRate = 2.5f;
    private float spawnTimer = 0f;
    [SerializeField]
    private int spawnCount = 2;
    private Queue<GameObject> enemyQ = new Queue<GameObject>();

    private void Update()
    {
        if(enemyQ.Count != 0 && spawnTimer <= 0f)
        {
            //Debug.Log(enemyQ.Count + " Enemies in Que");
            //
            for(int i = 0; i < spawnCount; i++)
            {
                if (enemyQ.Count == 0)
                    break;
                //Debug.Log("Spawning Enemy");
                spawnEnemy(enemyQ.Dequeue());

            }
            spawnTimer = spawnRate;
        }
        else
        {
            spawnTimer -= Time.deltaTime;
        }
    }

    public void QueueEnemy(GameObject enemy)
    {
        enemyQ.Enqueue(enemy);
    }

    public void spawnEnemy(GameObject enemy)
    {
        float xhlf = x / 2;
        float yhlf = y / 2;
        //this position is calculated to a random position within it's spawn parameters
        enemy.transform.position = new Vector3(transform.position.x + Random.Range(-xhlf, xhlf), transform.position.y, transform.position.z + Random.Range(-yhlf, yhlf));
        //Debug.Log("// SPAWNING AT \\ " + enemy.transform.position);
        enemy.SetActive(true);
    }


    private void OnDrawGizmos()
    {
        if (Selection.Contains(gameObject))
        {
            Gizmos.color = Color.yellow;        
            Vector3 dims = new Vector3(1 * x, 0.2f, 1 * y);
            Gizmos.DrawCube(transform.position, dims);
        }        
    }
}
