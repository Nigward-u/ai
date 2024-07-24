using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using UnityEngine.Animations;

[System.Serializable]
public class shoot : ActionNode
{
    //public GameObject bullet;
    public AIscript Ai;
     
    protected override void OnStart() {
       
        //bullet = GameObject.Find("bullet");
        Ai = GameObject.Find("ShootingEnemy").GetComponent<AIscript>();
        
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (blackboard.shoot == true)
        {
            Ai.shoot();
            blackboard.shoot = false;
            return State.Success;
        }
        else
        {
            return State.Failure;
        }
    }
}
