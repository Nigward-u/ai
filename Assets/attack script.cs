using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackscript : MonoBehaviour
{
    
    
    public void shoot(GameObject Bullet,Transform sp)
    {
        Instantiate(Bullet, sp.position, Quaternion.identity);
    }
    public void melee(Renderer render)
    {
       render.material.color= Color.black;
    }
}
