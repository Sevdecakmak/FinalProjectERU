using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPistol : MonoBehaviour
{
    public float theDistance;
    public GameObject actionKey;

  
    //public AudioSource doorSound;
    public GameObject activeCross;
    public GameObject pistolInActive;
    public GameObject pistolActive;

    

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
                
                this.gameObject.GetComponent<BoxCollider>().enabled = false; //var olmaz

                actionKey.SetActive(true);
                pistolActive.SetActive(true);



                //  doorSound.Play();
                Destroy(pistolInActive);
                

            }
        }
    }

    private void OnMouseExit()
    {

        //üstünde değilken
        actionKey.SetActive(false);

        activeCross.SetActive(false);
    }

    IEnumerator keyTakenText()
    {
        //getkeytext objesi iki sn tut bu kısım sayaç
        yield return new WaitForSeconds(2f);
        
    }
}
