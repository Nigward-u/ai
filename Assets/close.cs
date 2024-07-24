using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class close : ActionNode
{
    
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        bool closeai = context.gameObject.GetComponent<closeAI>().close;
        if (closeai)
        {
            return State.Success;
        }
        else { return State.Failure; }
    }
    
}
