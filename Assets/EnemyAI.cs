using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//INPUTS
//1) Player distance (check) DONE
//2) Field of view DONE
//3) Walls 
//4) walls distance (check)
//5) Distance to selected point (nav mesh)

//STATES
//1) Look around (idle)
//2) Run away (far from player)
//   2.0) search the best hiding place
//   2.1) find best path
//   2.2) run to intermediate point
//   2.3) run to end point
public class EnemyAI : MonoBehaviour
{
    [SerializeField] EnemyPerception _EnemyPerception;

    [SerializeField] EnemyStateBase Current;


    public EnemyPerception Perception => _EnemyPerception;
    

    public void SetState(EnemyStateBase state)
    {
        if(state!=null && state != Current)
        {
            Current.OnExit();
            Current = state;
            Current.OnEnter();
        }
    }

    private void Update()
    {
        //transform.Rotate(new Vector3(0, 45 * Time.deltaTime, 0), Space.Self);

        if (Current)
            Current.CustomUpdate(Time.deltaTime);

    }

}
