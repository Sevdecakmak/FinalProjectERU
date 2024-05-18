using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    NavMeshAgent agent;
    Animator anim;
    Transform target; //fps
    public bool isDead = false;

    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        //bu script zombinin içinde olacağı için kendi pozisyonu ile bizim pozisyonumuz arasındaki mesafeyi alacak

        if(distance <10 && distance > 2 && !isDead)
        {
            //bizi kovalasın istiyoruz
            //ilk önce pozisyonumuzu güncelleyeceğiz
            //ölü ikende takip etmemesi adına bir bool oluşturacağız

            agent.updatePosition = true;
            agent.SetDestination(target.position); //bize doğru gelecek

            anim.SetBool("isWalking", true);
            anim.SetBool("Attack", false);
        }
        else if(distance <= 2)
        {
            agent.updatePosition = false;
            anim.SetBool("isWalking", false);
            anim.SetBool("Attack", true);
        }
    }


    public void Hurt()
    {
        anim.SetTrigger("Hit");
    }

    public void DeadAnim()
    {
        isDead = true;
        anim.SetTrigger("Dead");
    }
}
