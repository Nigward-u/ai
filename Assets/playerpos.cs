using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using static UnityEditor.PlayerSettings;

[System.Serializable]
public class playerpos : ActionNode
{
    public GameObject Player; // La position du player
    
    protected override void OnStart() {
        Player = GameObject.Find("Player");
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        blackboard.moveToPosition = Player.transform.position;
        return State.Success;

        
    }
}
