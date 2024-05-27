using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour
{
    public float theDistance;
    public GameObject actionKey; //E tuşu


    //public AudioSource doorSound;
    public GameObject activeCross;


    public GameObject medKitBox;
    public GameObject fullHealthText;

    PlayerHealth player;

    private void Start()
    {
        player = FindAnyObjectByType<PlayerHealth>();
    }





    void Update()
    {
        theDistance = PlayerRay.distanceFromTarget;
    }

    private void OnMouseOver()
    {
        if (theDistance <= 4)
        {
            if (player.currentHealt == 100)
            {
                //mouse un üstündeyken
                actionKey.SetActive(false);

                activeCross.SetActive(true);

                fullHealthText.SetActive(true);
                
            }
            else if(player.currentHealt < 100)
            {
                actionKey.SetActive(true);

                activeCross.SetActive(true);
            }

            
        }
        else
        {
            actionKey.SetActive(false);
            activeCross.SetActive(false);

        }

        if (Input.GetButton("Action"))
        {
            if (theDistance <= 6)
            {
               if(player.currentHealt < 100)
                {
                    player.currentHealt += 25;
                    player.UpdateText();
                    player.healthBarSlider.value += 25;

                    actionKey.SetActive(false);
                    activeCross.SetActive(false);

                    Destroy(medKitBox);
                }

            }
        }
    }

    private void OnMouseExit()
    {

        //üstünde değilken
        actionKey.SetActive(false);

        activeCross.SetActive(false);
        fullHealthText.SetActive(false);

    }

   /* IEnumerator keyTakenText()
    {
        //getkeytext objesi iki sn tut bu kısım sayaç
        yield return new WaitForSeconds(1.5f);

    }*/
}
