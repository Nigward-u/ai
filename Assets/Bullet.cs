using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;
    public Vector3 dir;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }
    void Update()
    {
       
        dir =-( transform.position - player.position);
        transform.Translate(dir.normalized * speed * Time.deltaTime);    
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag==("Player ")) {
            Destroy(gameObject);
        }
    }
}
