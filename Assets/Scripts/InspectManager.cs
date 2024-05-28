using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class InspectManager : MonoBehaviour
{

    public float distance;
    public Transform playerSocket; //karakterin ne kadar önünde gözükecek
    Vector3 originalPos;
    bool onInspect; // incelemeyi kontrol eder
    GameObject inspected; //incelenen nesne

    public GameObject activisionKey;
    public GameObject droptext;

    public FirstPersonController playerScript;

    void Start()
    {
        
    }

    
    void Update()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward); //ray i tanımladık
        RaycastHit hit;

        if(Physics.Raycast(transform.position,fwd,out hit, distance))
        {

            if (hit.transform.tag == "Object")
            {
                activisionKey.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    inspected = hit.transform.gameObject;
                    originalPos = hit.transform.position;
                    onInspect = true;
                    StartCoroutine(PickUpItem());
                }

            }
            else
            {
                activisionKey.SetActive(false);
            }
        }

        if (onInspect)
        {
            //koda tekrar bak
            inspected.transform.position = Vector3.Lerp(inspected.transform.position, playerSocket.position, 0.2f);
            playerSocket.Rotate(new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Time.deltaTime*125f);
        }
        else if (inspected != null)
        {
            inspected.transform.SetParent(null);
            inspected.transform.position = Vector3.Lerp(inspected.transform.position, originalPos, 0.2f);
        }
        if (Input.GetKeyDown(KeyCode.G) && onInspect)
        {
            StartCoroutine(DropItem());
            onInspect = false;
        }

        IEnumerator PickUpItem()
        {
            
            playerScript.enabled = false;
            droptext.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            inspected.transform.SetParent(playerSocket);
        }

        IEnumerator DropItem()
        {
            inspected.transform.rotation = Quaternion.identity;
            droptext.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            playerScript.enabled = true;
            
        }
    }
}
