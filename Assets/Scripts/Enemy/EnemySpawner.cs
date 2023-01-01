using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawners;

    [SerializeField] private GameObject target;
    [SerializeField] private List<EnemyStats> enemyList;
    
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
        int i = 0;

        foreach(CharacterStats enemy in enemyList)
        {
            if (enemy.IsDead())
                i++;
            else return false;
        }

        return true;
    }
    
}
