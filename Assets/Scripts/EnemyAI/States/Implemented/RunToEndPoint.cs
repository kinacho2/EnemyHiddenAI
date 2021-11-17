using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RunToEndPoint : EnemyStateBase
{
    [SerializeField] EnemyStateBase LookAround;

    Vector3 LastDecidedPosition = Vector3.zero;

    public override void CustomUpdate(float deltaTime)
    {
        if (Owner.Perception.NavMeshDistanceChecker.Arrive)
        {
            Owner.Controller.StopMovement();
            Owner.SetState(LookAround);
        }
    }

    

    public override void OnEnter()
    {
        var hiddenPlaces = Owner.Perception.HiddenPlacesChecker.HiddenPositions;

        hiddenPlaces.Sort(comparer);

        Vector3 selectedPos = hiddenPlaces[0];

        LastDecidedPosition = selectedPos;
        Owner.Controller.StartMovement(selectedPos);

    }

    private int comparer(Vector3 x, Vector3 y)
    {
        var playerPos = Owner.Perception.EnemyView.PlayerLastPosition;
        var playerDir = (playerPos - Owner.transform.position).normalized;

        var xDir = (x - Owner.transform.position).normalized;
        var yDir = (y - Owner.transform.position).normalized;

        var dotX = Vector3.Dot(playerDir, xDir);
        var dotY = Vector3.Dot(playerDir, yDir);

        if (dotX < dotY)
            return -1;
        else if (dotX > dotY)
            return 1;
        else 
            return 0;
    }

    public override void OnExit()
    {

    }

    private void OnDrawGizmos()
    {
        if(LastDecidedPosition.magnitude > 0)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(LastDecidedPosition, .2f);
        }
    }
}
