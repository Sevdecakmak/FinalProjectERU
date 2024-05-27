using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pistol : MonoBehaviour
{
    RaycastHit hit; //mesafe ayarlama gibi


    public ParticleSystem muzzleFlash;
    Animator anim;

    AudioSource pistolAS;
    public AudioClip shootAC; //müzik dosyasında problem var
    public AudioClip emptyFire;


    
    public int currentAmmo=12; //mermi
    public int maxAmmo=12; //şarjörün kapasitesi
    public int carriedAmmo=60; //taşınabilen mermi

    bool isReloding;


    public GameObject metalBulletHole; // duvarda oluşan metal obje 
    public AudioClip shootMetalAC; // duvara ateş ettiğindeki metal ses

    [SerializeField]
    float rateOfFire; //ne kadar sürede bir ateş edebileceğimi hesaplamak için
    float nextFire =0;

    [SerializeField]
    float weaponRangre;

    public float damage = 20f;

    public Transform shootPoint;

    //EnemyHealth enemy;

    public Text currentAmmoText;
    public Text carriedAmmoText;


    public GameObject bloodEffect;

    void Start()
    {
        UpdateAmmoUI();
        anim = GetComponent<Animator>();

        pistolAS = GetComponent<AudioSource>();
        muzzleFlash.Stop(); //oyun başladığında çalışmasın
     //   enemy = FindObjectOfType<EnemyHealth>();

    }

    
    void Update()
    {
        if(Input.GetButton("Fire1") && currentAmmo > 0)
        {
            Shoot();
        }
        else if(Input.GetButton("Fire1") && currentAmmo <= 0 && !isReloding)
        {
            EmptyFire();
        }

        else if(Input.GetKeyDown(KeyCode.R) && currentAmmo <= maxAmmo && !isReloding)
        {
            isReloding = true;
            Reload();
        }
    }

    public void Shoot()
    {
        if(Time.time > nextFire) //oyundaki süre
        {
            nextFire = Time.time + rateOfFire;
            anim.SetTrigger("Shoot");
            currentAmmo--;

            ShootRay();
            UpdateAmmoUI();
         
        }
    }

    void ShootRay()
    {
        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, weaponRangre)) //bi ray çıkacak bu bir yere çarpıyor mu kontrol eder
        {
            if (hit.transform.tag == "Enemy")
            {
                EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>(); //49. satırdaki düzeltme
                
                Instantiate(bloodEffect, hit.point, transform.rotation); //kan efektimizin oluştuğu kısım
                enemy.ReduceHealth(damage);
            }
            else if (hit.transform.tag == "Metal")
            {
                pistolAS.PlayOneShot(shootMetalAC);
                Instantiate(metalBulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)); //nesne üretme mermi izi duvarda
            }
            else
            {
                Debug.Log("Something else");
            }
        }

    }


    void Reload()
    {
        if (carriedAmmo <= 0) return;
        anim.SetTrigger("Reload");
        StartCoroutine(ReloadCountDown(2f));
    }


    public void UpdateAmmoUI()
    {
        currentAmmoText.text = currentAmmo.ToString();
        carriedAmmoText.text = carriedAmmo.ToString();
    }

    void EmptyFire()
    {
        if(Time.time> nextFire)
        {
            nextFire = Time.time + rateOfFire;
            pistolAS.PlayOneShot(emptyFire);
            anim.SetTrigger("Empty");
        }
    }


    IEnumerator pistolEffect()
    {
        muzzleFlash.Play();
        pistolAS.PlayOneShot(shootAC);
        yield return new WaitForEndOfFrame(); //frame bittiği anda oynasın bitsin ateş efekti
        muzzleFlash.Stop();
    }

    IEnumerator ReloadCountDown(float timer)
    {
        while (timer > 0f)
        {
            
            timer -= Time.deltaTime; //1 er sn azaltma
            yield return null;
        }
        if (timer <= 0)
        {
            isReloding = false;
            int bulletNeeded= maxAmmo- currentAmmo; //
            int bulletsToDeduct = (carriedAmmo >= bulletNeeded) ? bulletNeeded : carriedAmmo;
            //if else düşecek mermi sayısı bulletsToDeduct

            carriedAmmo -= bulletsToDeduct;
            currentAmmo += bulletsToDeduct;

            UpdateAmmoUI();
        }
    }
}
