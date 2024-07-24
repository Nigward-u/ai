using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIscript : MonoBehaviour
{
    public GameObject bullet;
    public Transform SP;
    public void shoot()
    {
        Instantiate(bullet,SP.position,Quaternion.identity); 
    }
}
