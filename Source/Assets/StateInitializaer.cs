using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateInitializaer : MonoBehaviour
{
    [SerializeField] EnemyAI EnemyAI;
    [SerializeField] EnemyStateBase[] States;

    private void Awake()
    {
        States = GetComponentsInChildren<EnemyStateBase>();
        foreach(var s in States)
        {
            s.Init(EnemyAI);
        }
    }
}
