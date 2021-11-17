using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecideEscape : EnemyStateBase
{

    [SerializeField] EnemyStateBase FindHiddenPlaces;
    [SerializeField] EnemyStateBase RunOppositeDirection;
    public override void CustomUpdate(float deltaTime)
    {
        
    }

    public override void OnEnter()
    {
        Debug.Log("I need to run");
        Vector3 playerPos = Owner.Perception.EnemyView.PlayerLastPosition;
        Owner.Perception.EnvironmentPerception.CheckHiddenPlaces(Owner.transform.position, playerPos, Owner.EnemyParameters.EscapeMaxDot);

        var hiddenWalls = Owner.Perception.EnvironmentPerception.HiddenWalls;

        if(hiddenWalls.Count > 0)
        {
            Owner.SetState(FindHiddenPlaces);
        }
        else
        {
            Owner.SetState(RunOppositeDirection);
        }
    }

    public override void OnExit()
    {

    }
}
