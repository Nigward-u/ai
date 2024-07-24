using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chasescript : MonoBehaviour
{
    public float vitesse_poursuite = 2.0f;
    public float vitesseRot_poursuite = 2.0f;
    public float precision_poursuite = 5.0f;
    public void chase(Transform Player)
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
}
