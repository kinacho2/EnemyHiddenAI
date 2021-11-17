using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStateBase : MonoBehaviour
{
    protected EnemyAI Owner;

    public virtual void Init(EnemyAI owner)
    {
        this.Owner = owner;
    }

    public abstract void OnEnter();

    public abstract void OnExit();

    public abstract void CustomUpdate(float deltaTime);


    protected int comparer(Vector3 x, Vector3 y)
    {
        var playerPos = Owner.Perception.EnemyView.PlayerLastPosition;
        var playerDir = (playerPos - Owner.transform.position).normalized;

        var xDir = (x - Owner.transform.position).normalized;
        var yDir = (y - Owner.transform.position).normalized;

        var escapeDirX = Owner.Controller.GetInitialDirectionToTarget(x);
        var escapeDirY = Owner.Controller.GetInitialDirectionToTarget(y);

        var dotX = 
            Vector3.Dot(playerDir, xDir) + 
            Vector3.Dot(playerDir, escapeDirX);
        var dotY = 
            Vector3.Dot(playerDir, yDir) + 
            Vector3.Dot(playerDir, escapeDirY);

        if (dotX < dotY)
            return -1;
        else if (dotX > dotY)
            return 1;
        else
            return 0;
    }

}
