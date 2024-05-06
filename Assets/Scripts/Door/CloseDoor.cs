using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{
    public float theDistance;
    public GameObject actionKey;
    //public GameObject actionText;
    //public GameObject hinge; animasyon oynatma olmayacak
    public AudioSource doorSound;
    public GameObject closedDoorText;
    public float waitTime;


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
            
        }
        else
        {
            actionKey.SetActive(false);
            
        }

        if (Input.GetButton("Action"))
        {
            if (theDistance <= 2)
            {
                
                actionKey.SetActive(true);
                closedDoorText.SetActive(true);
                StartCoroutine(ClosedDoor());
                doorSound.Play();
            }
        }
    }

    private void OnMouseExit()
    {

        //üstünde değilken
        actionKey.SetActive(false);
        
    }

    IEnumerator ClosedDoor()
    {
        yield return new WaitForSeconds(waitTime);
        closedDoorText.SetActive(false);
    } //Sayacın içeriğinin olduğu kısım

}
