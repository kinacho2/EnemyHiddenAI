using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookBack : EnemyStateBase
{
    [SerializeField] EnemyStateBase LookAround;
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
            Owner.SetState(LookAround);

        }

    }

    public override void OnEnter()
    {
        timer = 0;
        CurrentRot = Owner.transform.rotation;
        var playerDir = -Owner.transform.forward;

        //Owner.Controller.StartMovement(Owner.transform.position - playerDir * Knockback);

        decidedRot = Quaternion.LookRotation(playerDir, Vector3.up);
    }

    public override void OnExit()
    {

    }

   
}
