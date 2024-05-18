using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAmmo : MonoBehaviour
{
    public float theDistance;
    public GameObject actionKey; //E tuşu


    //public AudioSource doorSound;
    public GameObject activeCross;
    public GameObject pistolInActive;


    public GameObject ammoBox;

    //yeterli mermi alındığında ekranda mermi yeterli yazsın ve daha fazla mermi alamasın!

    public GameObject AmmoTaken; //texti ekledim



    void Update()
    {
        theDistance = PlayerRay.distanceFromTarget;
    }

    private void OnMouseOver()
    {
        if (theDistance <= 2)
        {
            //mouse un üstündeyken
            actionKey.SetActive(true);

            activeCross.SetActive(true);
        }
        else
        {
            actionKey.SetActive(false);

        }

        if (Input.GetButton("Action"))
        {
            if (theDistance <= 2)
            {
                Pistol pistolScript = pistolInActive.GetComponent<Pistol>();
                pistolScript.carriedAmmo += 8;

                if(pistolScript.carriedAmmo >= 24)
                {
                    pistolScript.carriedAmmo = 24;
                    AmmoTaken.SetActive(true);

                }
                

                pistolScript.UpdateAmmoUI();


                this.gameObject.GetComponent<BoxCollider>().enabled = false; //var olmaz

                actionKey.SetActive(true);
                Destroy(ammoBox);


            }
        }
    }

    private void OnMouseExit()
    {

        //üstünde değilken
        actionKey.SetActive(false);

        activeCross.SetActive(false);
        AmmoTaken.SetActive(false);
    }

    IEnumerator keyTakenText()
    {
        //getkeytext objesi iki sn tut bu kısım sayaç
        yield return new WaitForSeconds(2f);

    }
}
