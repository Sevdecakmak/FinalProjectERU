using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float enemyHealth = 100;
    EnemyAI enemy;

    void Start()
    {
        enemy = GetComponent<EnemyAI>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyHealth <= 0)
        {
            enemyHealth = 0;
        }
    }

    public void ReduceHealth(float reduceHealth)
    {
        //canın ne kadar azaldığını belirtmek

        //hasar aldığında
        enemy.Hurt();

        if (!enemy.isDead)
        {
            enemy.Hurt();
        }
      
        enemyHealth -= reduceHealth;
        if(enemyHealth <= 0)
        {
            enemy.DeadAnim();
            Dead();
        }
    }

    void Dead()
    {
        Destroy(gameObject, 10f);
    }
}
