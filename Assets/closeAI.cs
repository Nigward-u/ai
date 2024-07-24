using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeAI : MonoBehaviour
{
    public bool close= false;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            close= true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            close = false;
        }
    }
    
}
