//#define DebugPlayerVisionTest

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem.Composites;
using UnityEngine.AI;

public class GuardControl : MonoBehaviour
{
    //-----------POUR LA FSM-------------------------
    enum Etats { Patrouiller, Chercher, Poursuivre };
    Etats etatActuel = Etats.Patrouiller;

    // --------Le Garde ----------------------
    public Transform Player; // La position du player
    public float fov_distance = 20.0f;// Champ de vision: distane
    public float fov_angle = 45.0f;//Champ de vision: angle

    //  --------- pour la poursuite -----------------------
    public float vitesse_poursuite = 2.0f;
    public float vitesseRot_poursuite = 2.0f;
    public float precision_poursuite = 5.0f;
    //-------------- pour la patrouille --------------------
    public float distance_patrouille = 10.0f;
    float attente_patrouille = 5.0f;
    float timing_patrouille = 0.0f;
    //------------------------------------------------------

    //
    Vector3 dernierEmplacementVu; //le dernier emplacement ou la player e été vu
    //-------------------------------------



    void Start()
    {
        timing_patrouille = attente_patrouille;
        dernierEmplacementVu = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Etats etatTemporaire = etatActuel;


#if DebugPlayerVisionTest
        if (ICanSee(Player))
        {
            Debug.Log("Player localisé à " + Player.position);
        }
        else
        {
            Debug.Log("RAS...");
        }
#endif
        if (ICanSee(Player))
        {
            etatActuel = Etats.Poursuivre; //je le vois donc je le poursuit
            dernierEmplacementVu = Player.position;// je le vois donc j'enregistre sa position
        }
        else
        {
            if (etatActuel == Etats.Poursuivre)
            {
                etatActuel = Etats.Chercher;
            }
        }

        switch (etatActuel)
        {
            case Etats.Patrouiller:
                Patrouiller();
                break;
            case Etats.Chercher:
                Chercher();
                break;
            case Etats.Poursuivre:
                Poursuivre(Player);
                break;
        }

        if (etatTemporaire != etatActuel)
            Debug.Log("Etat du garde:" + etatActuel);
        DrawFOV();
    }
    public bool ICanSee(Transform player)
    {
        Vector3 direction = Player.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);
        RaycastHit hit;
        if (
            Physics.Raycast(this.transform.position, direction, out hit) && // je peux lancer rayon vers le Player ?
            hit.collider.gameObject.tag == "Player" &&// la collision est avec le player ?
            direction.magnitude < fov_distance &&// Le player est assez proche pour etre vu ?
            angle < fov_angle // Le palyer est dans mon champ de vision ?

            )
        {

            return true;// je vois le Player !

        }


        return false; // je le vois pas 


    }

    public void Poursuivre(Transform Player)
    {
        this.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
        this.GetComponent<UnityEngine.AI.NavMeshAgent>().ResetPath();
        Vector3 direction = Player.position - this.transform.position;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.
         LookRotation(direction), Time.deltaTime * this.vitesseRot_poursuite);

        if (direction.magnitude > this.precision_poursuite)
        {
            this.transform.Translate(0, 0, Time.deltaTime * this.vitesse_poursuite);
            //ici chisir le bon emplacement sur votre map !
        }

    }

    public void Chercher()
    {
        //le garde doit aller au dernier endroit ou le player a été vu et 
        //il doit patrouiller à cet endroit
        if (Vector3.Distance(transform.position, dernierEmplacementVu) < 0.5f)
        {
            etatActuel = Etats.Patrouiller;
        }
        else
        {
            this.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(dernierEmplacementVu);
            Debug.Log("Etat du Garde: " + etatActuel + " point " + dernierEmplacementVu);
        }

    }

    public void Patrouiller()
    {
        timing_patrouille += Time.deltaTime;
        if (timing_patrouille > attente_patrouille)
        {
            timing_patrouille = 0.0f;
            Vector3 pointDePatrouille = dernierEmplacementVu;

            //on genere un emplacement aleatoir à partir du dernier emplacement Vu
            float alleatoir = Random.Range(-distance_patrouille, distance_patrouille);
            pointDePatrouille += new Vector3(alleatoir, 0, alleatoir);

            //et maintenant on y va..
            this.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(pointDePatrouille);
        }
    }

    void DrawFOV()
    {
        float halfFOV = fov_angle / 2.0f;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.up);

        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;

        Color fovColor = ICanSee(Player) ? Color.green : Color.red;

        Debug.DrawRay(transform.position, leftRayDirection * fov_distance, fovColor);
        Debug.DrawRay(transform.position, rightRayDirection * fov_distance, fovColor);

        // Draw lines connecting the ends of the FOV to the guard position
        Debug.DrawLine(transform.position, transform.position + leftRayDirection * fov_distance, fovColor);
        Debug.DrawLine(transform.position, transform.position + rightRayDirection * fov_distance, fovColor);
    }


}