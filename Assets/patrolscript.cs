using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrolscript : MonoBehaviour
{
    public float distance_patrouille = 10.0f;
    float attente_patrouille = 5.0f;
    float timing_patrouille = 0.0f;
    public Transform player;
    Vector3 dernierEmplacementVu;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void patrol()
    {
        timing_patrouille += Time.deltaTime;
        if (timing_patrouille > attente_patrouille)
        {
            timing_patrouille = 0.0f;
            dernierEmplacementVu = player.position;
            Vector3 pointDePatrouille = dernierEmplacementVu;

            //on genere un emplacement aleatoir à partir du dernier emplacement Vu
            float alleatoir = Random.Range(-distance_patrouille, distance_patrouille);
            pointDePatrouille += new Vector3(alleatoir, 0, alleatoir);

            //et maintenant on y va..
            this.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(pointDePatrouille);

        }
    }
}
