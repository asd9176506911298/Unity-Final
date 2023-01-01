using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHack : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private EnemySpawner enemySpawner;
    public Transform obj;

    private void Start()
    {
        GetReferences();
    }

    private void Update()
    {
        for (int i = 0; i < enemySpawner.enemyList.Count; i++)
        {
            if (enemySpawner.enemyList[i])
            {

            }
        }
    }

    private void GetReferences()
    {
        cam = GetComponentInChildren<Camera>();
    }
}
