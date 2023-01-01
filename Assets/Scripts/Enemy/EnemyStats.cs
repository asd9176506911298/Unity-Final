using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private EnemySpawner enemySpawner;

    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }



}
