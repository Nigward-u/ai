using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class seeplayer : ActionNode
{
    public GameObject Player; // La position du player
    public float fov_distance = 20.0f;// Champ de vision: distane
    public float fov_angle = 45.0f;//Champ de vision: angle
    Color fovColor = Color.red;

    public float fovDistance = 10f;
    protected override void OnStart() {
        Player = GameObject.Find("Player");
        
    }

    protected override void OnStop() {

    }

    protected override State OnUpdate() {
        Draw();
        Vector3 direction = Player.transform.position - context.gameObject.transform.position;
        float angle = Vector3.Angle(direction, context.gameObject.transform.forward);
        RaycastHit hit;
        if (
            Physics.Raycast(context.gameObject.transform.position, direction, out hit) && // je peux lancer rayon vers le Player ?
            hit.collider.gameObject.tag == "Player" &&// la collision est avec le player ?
            direction.magnitude < fov_distance &&// Le player est assez proche pour etre vu ?
            angle < fov_angle // Le palyer est dans mon champ de vision ?

            )
        {
            fovColor = Color.green;
            return State.Success;
        }
        else
        {
            fovColor = Color.red;
            return State.Failure;
        }
    }
    void Draw()
    {
        float halfFOV = fov_angle / 2f;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.up);

        Vector3 leftRayDirection = leftRayRotation * context.gameObject.transform.forward;
        Vector3 rightRayDirection = rightRayRotation * context.gameObject.transform.forward;

        Debug.DrawRay(context.gameObject.transform.position, leftRayDirection * fovDistance, fovColor);
        Debug.DrawRay(context.gameObject.transform.position, rightRayDirection * fovDistance, fovColor);
    }
}
