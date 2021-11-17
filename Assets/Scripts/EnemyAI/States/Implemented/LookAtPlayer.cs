using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : EnemyStateBase
{
    [SerializeField] EnemyStateBase DecideEscape;
    [SerializeField] float TimeForLook = .3f;
    float timer = 0;

    Quaternion CurrentRot;
    Quaternion decidedRot;

    public override void CustomUpdate(float deltaTime)
    {
        timer += deltaTime;
        
        Owner.transform.rotation = Quaternion.Lerp(CurrentRot, decidedRot, Mathf.Clamp01(timer / TimeForLook));

        if(timer >= TimeForLook)
        {
            Owner.SetState(DecideEscape);
        }

    }

    public override void OnEnter()
    {
        timer = 0;
        CurrentRot = Owner.transform.rotation;
        var playerDir = (Owner.Perception.PlayerDistanceDetector.PlayerLastPosition - Owner.transform.position).normalized;

        decidedRot = Quaternion.LookRotation(playerDir, Vector3.up);
    }

    public override void OnExit()
    {

    }

   
}
