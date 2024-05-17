using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public float theDistance;
    public GameObject actionKey;
    public GameObject actionText;
    public GameObject hinge;
    public AudioSource doorSound;
    public GameObject activeCross;




    void Update()
    {
        theDistance = PlayerRay.distanceFromTarget;
    }

    private void OnMouseOver()
    {
        if(theDistance <= 2) { 
        //mouse un üstündeyken
        actionKey.SetActive(true);
        actionText.SetActive(true);
        activeCross.SetActive(true);
        }
        else
        {
            actionKey.SetActive(false);
            actionText.SetActive(false);
        }

        if (Input.GetButton("Action"))
        {
            if (theDistance <= 2)
            {
                this.gameObject.GetComponent<BoxCollider>().enabled = false; //var olmaz
                actionKey.SetActive(true);
                actionText.SetActive(true);
                hinge.GetComponent<Animation>().Play("OppositeDoor");
                hinge.GetComponent<Animation>().Play("OppositeDoor");
                doorSound.Play();
            }
        }
    }

    private void OnMouseExit()
    {

        //üstünde değilken
        actionKey.SetActive(false);
        actionText.SetActive(false);
        activeCross.SetActive(false);
    }
}
