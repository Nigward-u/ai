using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class calulDis : ActionNode
{
    public bool shoot;
    protected override void OnStart() {
        
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        Vector3 playerpos = blackboard.moveToPosition;
        float dis = Vector3.Distance(playerpos, context.transform.position);
        if (dis > 10f)
        {
            blackboard.shoot =false ;
            return State.Success;
        }
        else
        {
            blackboard.shoot = true;
            return State.Failure;
        }

    }
}
