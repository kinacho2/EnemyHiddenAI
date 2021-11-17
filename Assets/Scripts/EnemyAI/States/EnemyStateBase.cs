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

}
