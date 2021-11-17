using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecideEscape : EnemyStateBase
{
    public override void CustomUpdate(float deltaTime)
    {

    }

    public override void OnEnter()
    {
        Debug.Log("I need to run");
    }

    public override void OnExit()
    {

    }
}
