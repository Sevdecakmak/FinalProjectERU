using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealt;
    public float maxHealth = 100f;
    public static PlayerHealth PH;

    public Slider healthBarSlider;
    public Text healthText;

    [Header("Damage Screen")]
    public Color damageColor;
    public Image damageImage;
    bool isTakinDamage= false;
    float colorSpeed = 5f;


    public bool isDead=false;

    private void Awake()
    {
        //start dan önce çalışır
        PH = this;
    }
    void Start()
    {
        //unity i açtığında update den önce çalışır
        healthText.text = maxHealth.ToString();
        currentHealt = maxHealth;
        healthBarSlider.value = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealt <= 0)
        {
            
            currentHealt = 0;
        }

        if (isTakinDamage)
        {
            damageImage.color = damageColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, 5 * Time.deltaTime); // kan efektinin yavaş yavaş gitmesini sağlar
        }
    }

    public void DamagePlayer(float damage)
    {
        if (currentHealt > 0) //canım 0 dan büyükse
        {

            if (damage >= currentHealt)
            {
                isTakinDamage = true;
                Dead();
            }

            else
            {
                isTakinDamage = true;
                currentHealt -= damage;
                //health barım damage aldıkça azalacak

                healthBarSlider.value -= damage;
                UpdateText();

            }
            isTakinDamage = false; //hasar almadığım kısım
        }
    }

    void UpdateText() 
    {
        //health textimizi güncellediğimiz kısım
        healthText.text = currentHealt.ToString();

    }

    void Dead()
    {
        currentHealt = 0;
        isDead = true;
        healthBarSlider.value = 0;
        UpdateText();
        Debug.Log("öldün");
    }
}
