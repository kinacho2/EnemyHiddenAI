using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindAllPosibleHiddenPlaces : EnemyStateBase
{
    [SerializeField] EnemyStateBase NextState;
    [SerializeField] float MinDistance = 4;

    public override void CustomUpdate(float deltaTime)
    {

    }

    public override void OnEnter()
    {
        Vector3 playerPos = Owner.Perception.EnemyView.PlayerLastPosition;
        var hiddenWalls = Owner.Perception.EnvironmentPerception.HiddenWalls;

        Owner.Perception.HiddenPlacesChecker.CheckHiddenPositions(hiddenWalls, playerPos, MinDistance, Owner.Perception.EnvironmentPerception.EnvironmentLayer);

        Owner.SetState(NextState);
    }

    public override void OnExit()
    {

    }
}
