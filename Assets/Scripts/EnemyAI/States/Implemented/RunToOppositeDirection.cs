using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunToOppositeDirection : EnemyStateBase
{
    public override void CustomUpdate(float deltaTime)
    {

    }

    public override void OnEnter()
    {
        Debug.Log("Run Opposite");
    }

    public override void OnExit()
    {

    }
}
