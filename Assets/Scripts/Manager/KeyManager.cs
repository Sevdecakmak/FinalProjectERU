using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{

    //anahtar trigger alanına girdiği zaman anahtar elimize geçecek

    public bool isKeyObtained;
    public GameObject key;

    void Start()
    {
        isKeyObtained = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Key1")
        {
            isKeyObtained = true;
            Destroy(key);
        }
    }


}
