using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityGate : MonoBehaviour
{
    public float theDistance;
    public GameObject actionKey;
    public GameObject actionText;
    public GameObject doorAnim;
    public AudioSource doorSound;
    public GameObject activeCross;
    GetKey key;


    private void Start()
    {
        key = FindObjectOfType<GetKey>();
    }




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
            actionText.SetActive(true);
            activeCross.SetActive(true);
        }
        else
        {
            actionKey.SetActive(false);
            actionText.SetActive(false);
        }

        if (Input.GetButton("Action") && key.isKeyTaken==true)
        {
            if (theDistance <= 2)
            {
                this.gameObject.GetComponent<BoxCollider>().enabled = false; //var olmaz
                actionKey.SetActive(true);
                actionText.SetActive(true);
                doorAnim.GetComponent<Animation>().Play("SecurityGate");
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
