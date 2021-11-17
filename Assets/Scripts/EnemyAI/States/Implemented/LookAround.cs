using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAround : EnemyStateBase
{
    [SerializeField] EnemyStateBase LookAtPlayer;
    [SerializeField] EnemyStateBase DecideEscape;

    [SerializeField] float AngleSpeed = 20;
    
    public override void OnEnter()
    {

    }

    public override void OnExit()
    {

    }

    public override void CustomUpdate(float deltaTime)
    {

        Owner.transform.Rotate(new Vector3(0, AngleSpeed * deltaTime, 0), Space.World);

        if (Owner.Perception.EnemyView.SeePlayer)
        {
            Owner.SetState(DecideEscape);
        }
        else if (Owner.Perception.PlayerDistanceDetector.PlayerIsNear)
        {
            Owner.SetState(LookAtPlayer);
        }

    }
}
