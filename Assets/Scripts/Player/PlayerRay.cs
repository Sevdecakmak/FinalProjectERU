using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRay : MonoBehaviour
{

    public static float distanceFromTarget; //birçok nesnenin uzaklığını hesaplamak içn static
    public float toTarget; //gördüğüm her nesneye olan uzaklık

    
  

    
    void Update()
    {
        RaycastHit hit; //ışın demeti
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {//eğer ışınım bir nesneye çarparsa
            toTarget = hit.distance;
            distanceFromTarget = toTarget; 

        }  //nesneye çarptığımız varsayılır
    }
}
