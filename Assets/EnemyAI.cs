using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] PlayerDistanceDetector PlayerDistanceDetector;
    [SerializeField] EnemyView EnemyView;

    [SerializeField] NavMeshDistanceChecker NavMeshDistanceChecker;
    

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

}
