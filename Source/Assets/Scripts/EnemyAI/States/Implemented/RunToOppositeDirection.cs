using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunToOppositeDirection : EnemyStateBase
{
    [SerializeField] EnemyStateBase LookBehind;
    [SerializeField] RunToEndPoint RunToEndPoint;
    [SerializeField] float maxDistance = 5;
    [SerializeField] float minDistance = 2;
    [SerializeField] LayerMask WallsLayer;
    [SerializeField] float CheckTime = 0.5f;
    float timer = 0;


    public override void CustomUpdate(float deltaTime)
    {
        timer += deltaTime;
        if(timer > CheckTime)
        {
            timer = 0;
            //CHECK SCAPE
            CheckEscape();
        }
        else if (Owner.Perception.NavMeshDistanceChecker.Arrive)
        {
            Owner.SetState(LookBehind);
        }
    }

    public override void OnEnter()
    {
        timer = 0;
        var playerPos = Owner.Perception.EnemyView.PlayerLastPosition;

        var playerDir = (playerPos - Owner.transform.position).normalized;
        var DecidedPos = Owner.transform.position - playerDir * maxDistance;
        RaycastHit hit;
        if (Physics.Raycast(Owner.transform.position, -playerDir, out hit, maxDistance, WallsLayer))
        {
            DecidedPos = hit.point;
            if(Vector3.Distance(Owner.transform.position, hit.point) < minDistance)
            {
                var otherDir = playerDir;
                otherDir.x = playerDir.y;
                otherDir.y = playerDir.x;

                if (Physics.Raycast(Owner.transform.position, otherDir, out hit, maxDistance, WallsLayer))
                {
                    DecidedPos = hit.point;
                    if (Vector3.Distance(Owner.transform.position, hit.point) < minDistance)
                    {
                        if (Physics.Raycast(Owner.transform.position, -otherDir, out hit, maxDistance, WallsLayer))
                        {
                            DecidedPos = hit.point;
                        }
                        else
                        {
                            DecidedPos = Owner.transform.position - otherDir * maxDistance;
                        }
                    }
                }
                else
                {
                    DecidedPos = Owner.transform.position + otherDir * maxDistance;
                }
            }
        }

        Owner.Controller.StartMovement(DecidedPos);


    }

    public override void OnExit()
    {

    }


    void CheckEscape()
    {
        Debug.Log("I need to run");
        Vector3 playerPos = Owner.Perception.EnemyView.PlayerLastPosition;
        Owner.Perception.EnvironmentPerception.CheckHiddenPlaces(Owner.transform.position, playerPos, Owner.EnemyParameters.EscapeMaxDot);

        var hiddenWalls = Owner.Perception.EnvironmentPerception.HiddenWalls;

        if (hiddenWalls.Count > 0)
        {

            Owner.Perception.HiddenPlacesChecker.CheckHiddenPositions(hiddenWalls, playerPos, 0, Owner.Perception.EnvironmentPerception.EnvironmentLayer);
            var hiddenPlaces = Owner.Perception.HiddenPlacesChecker.HiddenPositions;

            if (hiddenPlaces.Count > 0)
            {
                hiddenPlaces.Sort(comparer);
                var playerDir = (Owner.Perception.EnemyView.PlayerLastPosition - Owner.transform.position).normalized;
                Vector3 selectedPos = hiddenPlaces[0];
                Vector3 selectedDir = Owner.Controller.GetInitialDirectionToTarget(selectedPos);
                if (Vector3.Dot(playerDir, selectedDir) < Owner.EnemyParameters.EscapeMaxDot)
                {
                    //LastDecidedPosition = selectedPos;
                    //Owner.Controller.StartMovement(selectedPos);
                    Owner.Controller.StopMovement();
                    Owner.SetState(RunToEndPoint);
                }
               
            }
        }

    }
}
