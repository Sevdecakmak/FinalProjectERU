using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float enemyHealth = 100;
    EnemyAI enemy;


    //public GameObject bloodEffect;

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
        enemyHealth -= reduceHealth;
        

        if (!enemy.isDead)
        {
            enemy.Hurt();
        }
      
        
        if(enemyHealth <= 0)
        {
            enemy.DeadAnim();
            Dead();
        }
    }

    void Dead()
    {
       // bloodEffect.SetActive(true);
        enemy.canAttack = false;
        Destroy(gameObject, 10f);
    }
}
