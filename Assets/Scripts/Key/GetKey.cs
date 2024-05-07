using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetKey : MonoBehaviour
{
    public float theDistance;
    public GameObject actionKey;

    public GameObject getKeyText;
    //public AudioSource doorSound;
    public GameObject activeCross;

    public bool isKeyTaken;
    public GameObject key;

    
    private void Start()
    {
        isKeyTaken = false;
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
                isKeyTaken = true;
                this.gameObject.GetComponent<BoxCollider>().enabled = false; //var olmaz

                actionKey.SetActive(true);
                getKeyText.SetActive(true);

                StartCoroutine(keyTakenText());

                //  doorSound.Play();

                key.GetComponent<MeshRenderer>().enabled = false;

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
        getKeyText.SetActive(false);
    }
}
