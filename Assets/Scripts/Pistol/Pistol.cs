using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    RaycastHit hit; //mesafe ayarlama gibi


    public ParticleSystem muzzleFlash;
    Animator anim;

    AudioSource pistolAS;
    public AudioClip shootAC; //müzik dosyasında problem var


    [SerializeField]
    int currentAmmo;

    [SerializeField]
    float rateOfFire; //ne kadar sürede bir ateş edebileceğimi hesaplamak için
    float nextFire =0;

    [SerializeField]
    float weaponRangre;

    public float damage = 20f;

    public Transform shootPoint;

    EnemyHealth enemy;

    void Start()
    {
        anim = GetComponent<Animator>();

        pistolAS = GetComponent<AudioSource>();
        muzzleFlash.Stop(); //oyun başladığında çalışmasın
        enemy = FindObjectOfType<EnemyHealth>();
    }

    
    void Update()
    {
        if(Input.GetButton("Fire1") && currentAmmo > 0)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        if(Time.time > nextFire) //oyundaki süre
        {
            nextFire = Time.time + rateOfFire;
            anim.SetTrigger("Shoot");
            currentAmmo--;
            

            if(Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, weaponRangre)) //bi ray çıkacak bu bir yere çarpıyor mu kontrol eder
              {
                if (hit.transform.tag == "Enemy")
                {
                    enemy.ReduceHealth(damage);
                }
                else
                {
                    Debug.Log("Something else");
                }
            }
            
        }
    }

     IEnumerator pistolEffect()
    {
        muzzleFlash.Play();
        pistolAS.PlayOneShot(shootAC);
        yield return new WaitForEndOfFrame(); //frame bittiği anda oynasın bitsin ateş efekti
        muzzleFlash.Stop();
    }
}
