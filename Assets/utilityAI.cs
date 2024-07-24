using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class utilityAI : MonoBehaviour
{
    public Transform player;

    public chasescript chase;
    public patrolscript patrol;
    public attackscript attack;
    public float fovDistance;
    public float fovAngle;
    public float chaseutility, patrolutility, attackutility;
    public GameObject bullet;
    public Transform sp;
    public Renderer render;
    bool readytoshoot= true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (pickUtility())
        {
            case 0:patrol.patrol();
                render.material.color = Color.yellow;
                
                break;
            case 1:
                if(Vector3.Distance(transform.position, player.position) < 1.5f)
                {
                    attack.melee(render);
                }
                else if(readytoshoot) { attack.shoot(bullet, sp);
                    render.material.color = Color.white;
                    readytoshoot = false;
                    Invoke("reload", 2f);
                }
                
                break;
            case 2:chase.chase(player);
                render.material.color = Color.red;
                break;
        }
        patrolutility = calculpatrol();
        chaseutility = calculchase();
        attackutility = calculattack();


    }
        public bool ICanSee(Transform player)
        {
            Vector3 direction = player.position - this.transform.position;
            float angle = Vector3.Angle(direction, this.transform.forward);
            RaycastHit hit;
            if (
                Physics.Raycast(this.transform.position, direction, out hit) && // je peux lancer rayon vers le Player ?
                hit.collider.gameObject.tag == "Player" &&// la collision est avec le player ?
                direction.magnitude < fovDistance &&// Le player est assez proche pour etre vu ?
                angle < fovAngle // Le palyer est dans mon champ de vision ?

                )
            {

                return true;// je vois le Player !

            }


            return false; // je le vois pas 
        }

        void DrawFOV()
        {
            float halfFOV = fovAngle / 2.0f;
            Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.up);
            Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.up);

            Vector3 leftRayDirection = leftRayRotation * transform.forward;
            Vector3 rightRayDirection = rightRayRotation * transform.forward;

            Color fovColor = ICanSee(player) ? Color.green : Color.red;

            Debug.DrawRay(transform.position, leftRayDirection * fovDistance, fovColor);
            Debug.DrawRay(transform.position, rightRayDirection * fovDistance, fovColor);

            // Draw lines connecting the ends of the FOV to the guard position
            Debug.DrawLine(transform.position, transform.position + leftRayDirection * fovDistance, fovColor);
            Debug.DrawLine(transform.position, transform.position + rightRayDirection * fovDistance, fovColor);
        }
        int pickUtility()
        {
            float picked = Math.Max(chaseutility, Math.Max(patrolutility, attackutility));
            if (picked == attackutility) {
                return 1;

            } else if (picked == chaseutility)
            {
                return 2;
            }
            else { return 0; }
        }
        float calculpatrol()
        {
        if(ICanSee(player)==false) {
            return 1f;
        }else return 0f;
        }
    float calculchase()
    {
        float x=Vector3.Distance(transform.position,player.position);
        if (ICanSee(player))
        {
            return Math.Clamp(0.1f *MathF.Exp(0.24f*x)-0.1f,0,1) ;
        }
        else return 0f;
    }
    float calculattack()
    {
        float x = Vector3.Distance(transform.position, player.position);
        if (ICanSee(player))
        {
            return Math.Clamp( Mathf.Pow(10-x,3)/ Mathf.Pow(10f,3f),0,1);
        }
        else return 0f;

    }
    void reload()
    {
        readytoshoot = true;
    }

}

