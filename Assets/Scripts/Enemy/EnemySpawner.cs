using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawners;

    [SerializeField] private GameObject target;
    [SerializeField] public List<EnemyStats> enemyList;
    
    void Update()
    {


        if(EnemiesAreDead())
        {
            SpawnTarget();
        }

    }

    public void SpawnTarget()
    {
        int randomInt = Random.Range(1, spawners.Length);
        Transform randomSpawner = spawners[randomInt];

        GameObject newEnemy = Instantiate(target, randomSpawner.position, randomSpawner.rotation);
        EnemyStats newEnemyStats = newEnemy.GetComponent<EnemyStats>();

        enemyList.Add(newEnemyStats);
    }

    public bool EnemiesAreDead()
    {

        for(int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i].IsDead())
            {
                Debug.Log(enemyList[i].IsDead());
                enemyList.RemoveAt(i);  
            }
            else
                return false;
        }

        return true;
    }
    

}
