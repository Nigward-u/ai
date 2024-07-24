using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class NewActionNode : ActionNode
{
    public Vector3 min = Vector3.one * -10;
    public Vector3 max = Vector3.one * 10;

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        Vector3 pos = new Vector3();
        pos.x = Random.Range(min.x, max.x);
        pos.z = Random.Range(min.z, max.z);
        pos.y = 0.5f;
        blackboard.moveToPosition = pos;
        return State.Success;
    }
}
