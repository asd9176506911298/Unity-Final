using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawners;

    [SerializeField] private GameObject target;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            SpawnTarget();
        }
    }

    private void SpawnTarget()
    {
        int randomInt = Random.RandomRange(1, spawners.Length);
        Transform randomSpawner = spawners[randomInt];

        Instantiate(target, randomSpawner.position, randomSpawner.rotation);
    }
}
