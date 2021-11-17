using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RunToEndPoint : EnemyStateBase
{
    [SerializeField] EnemyStateBase LookAround;
    [SerializeField] EnemyStateBase RunOpposite;
    //[SerializeField] float maxEscapeDot = 0;
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


        if (hiddenPlaces.Count > 0)
        {
            hiddenPlaces.Sort(comparer);
            var playerDir = (Owner.Perception.EnemyView.PlayerLastPosition - Owner.transform.position).normalized;
            Vector3 selectedPos = hiddenPlaces[0];
            Vector3 selectedDir = Owner.Controller.GetInitialDirectionToTarget(selectedPos);
            if (Vector3.Dot(playerDir, selectedDir) < Owner.EnemyParameters.EscapeMaxDot)
            {
                LastDecidedPosition = selectedPos;
                Owner.Controller.StartMovement(selectedPos);
            }
            else
            {
                Owner.SetState(RunOpposite);
            }
        }
        else
        {
            Owner.SetState(RunOpposite);
        }
    }


    public override void OnExit()
    {

    }

    public void OnDrawGizmos()
    {
        if(LastDecidedPosition.magnitude > 0)
        {

            UnityEngine.Gizmos.color = Color.green;
            UnityEngine.Gizmos.DrawSphere(LastDecidedPosition, .2f);
        }
    }

    public void DrawGizmos()
    {
        if (LastDecidedPosition.magnitude > 0)
        {

            Popcron.Gizmos.Sphere(LastDecidedPosition, .2f, Color.green, true);
        }
    }



    private void Update()
    {
        DrawGizmos();
    }

}
