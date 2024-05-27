using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;
    Transform target; // fps
    public bool isDead = false;
    public float turnSpeed;
    public float damage = 25f;
    public bool canAttack;
    [SerializeField]
    float attackTimer = 2f;

    void Start()
    {
        canAttack = true;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        // bu script zombinin içinde olacağı için kendi pozisyonu ile bizim pozisyonumuz arasındaki mesafeyi alacak

        if (distance < 10 && distance > agent.stoppingDistance && !isDead)
        {
            // bizi kovalasın istiyoruz
            // ilk önce pozisyonumuzu güncelleyeceğiz
            // ölü iken de takip etmemesi adına bir bool oluşturacağız

            ChasePlayer();
        }
        else if (distance <= agent.stoppingDistance && canAttack)
        {
            // yönü bize dönsün istiyoruz enemy nin
            agent.updateRotation = false;
            Vector3 direction = target.position - transform.position;
            direction.y = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);

            agent.updatePosition = false;
            anim.SetBool("isWalking", false);
            anim.SetBool("Attack", true);
        }
        else if (distance > 10)
        {
            StopChase();
        }
    }

    void StopChase()
    {
        // kovalamayı bıraksın
        agent.updatePosition = false;
        anim.SetBool("isWalking", false);
        anim.SetBool("Attack", false);
    }

    void ChasePlayer()
    {
       // if (agent.isActiveAndEnabled && agent.isOnNavMesh) {} 
            agent.updateRotation = true;
            agent.updatePosition = true;
            agent.SetDestination(target.position); // bize doğru gelecek

            anim.SetBool("isWalking", true);
            anim.SetBool("Attack", false);
        
    }

    void AttackPlayer()
    {
        PlayerHealth.PH.DamagePlayer(damage);

        // PlayerHealth.PH.DamagePlayer(damage); // bu şekilde devamlı hasar veriyor ama biz animasyon ile hasar versin istiyoruz.

        // StartCoroutine(AttackTime());
    }

    public void Hurt()
    {
        agent.enabled = false;
        anim.SetTrigger("Hit");
        StartCoroutine(Nav());
    }

    public void DeadAnim()
    {
        isDead = true;
        anim.SetTrigger("Dead");
    }

    IEnumerator Nav()
    {
        yield return new WaitForSeconds(1.5f);
        agent.enabled = true;
    }

    /*IEnumerator AttackTime()
    {
        canAttack = false;
        yield return new WaitForSeconds(0.5f);
        PlayerHealth.PH.DamagePlayer(damage);
        yield return new WaitForSeconds(attackTimer);
        canAttack = true;
    }*/
}
