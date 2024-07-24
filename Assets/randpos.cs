using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class randpos : ActionNode
{
    public float distance_patrouille = 10.0f;
    float attente_patrouille = 5.0f;
    float timing_patrouille = 0.0f;

    
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        Patrouiller();
        return State.Running;
    }
    void Patrouiller()
    {
        timing_patrouille += Time.deltaTime;
        if (timing_patrouille > attente_patrouille)
        {
            timing_patrouille = 0.0f;
            

            //on genere un emplacement aleatoir à partir du dernier emplacement Vu
            float alleatoir = Random.Range(-distance_patrouille, distance_patrouille);
            Vector3 pointDePatrouille = new Vector3(alleatoir, context.transform.position.y, alleatoir);

            //et maintenant on y va..
            context.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(pointDePatrouille);
        }
    }

}
