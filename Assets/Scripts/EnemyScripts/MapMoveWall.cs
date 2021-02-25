using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMoveWall : MonoBehaviour
{
    public Vector3 position; 
    public GameObject[] next = new GameObject[1];

    private void Awake()
    {
        position = gameObject.transform.position;
    }

    public Vector3 sendNextPosition()
    {
        if (next.Length == 1)
        {
            //Debug.Log("Sending new locale");
            return next[0].transform.position;
        }
        else
        {
            int c = next.Length;
            int max = 100 / c;
            int rand = Random.Range(1, 100);
            int total = max;
            for (int i = 0; i < c; i++)
            {
                if (rand <= total)
                    return next[i].transform.position;
                else
                    total += max;
            }
        }
        
        return position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //Debug.Log("Detected Enemy at " + gameObject.name);
            other.gameObject.GetComponent<EnemyAI>().UpdateAttackTarget(sendNextPosition());
        }
    }

}
