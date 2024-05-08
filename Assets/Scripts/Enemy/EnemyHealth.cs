using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float enemyHealth = 100;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReduceHealth(float reduceHealth)
    {
        //canın ne kadar azaldığını belirtmek
        enemyHealth -= reduceHealth;
        if(enemyHealth <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        Destroy(gameObject);
    }
}
