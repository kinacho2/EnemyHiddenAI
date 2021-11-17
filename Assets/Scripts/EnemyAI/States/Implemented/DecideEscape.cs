using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecideEscape : EnemyStateBase
{

    [SerializeField] EnemyStateBase FindHiddenPlaces;
    [SerializeField] EnemyStateBase RunOppositeDirection;
    [SerializeField] float maxDot = .4f;
    public override void CustomUpdate(float deltaTime)
    {
        
    }

    public override void OnEnter()
    {
        Debug.Log("I need to run");
        Vector3 playerPos = Owner.Perception.EnemyView.PlayerLastPosition;
        Owner.Perception.EnvironmentPerception.CheckHiddenPlaces(Owner.transform.position, playerPos, maxDot);

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
