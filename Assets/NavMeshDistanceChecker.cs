using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshDistanceChecker : MonoBehaviour
{

    [SerializeField] NavMeshController NavMeshController;

    public bool IsMoving => NavMeshController.IsMoving;

    public bool Arrive => NavMeshController.Arrive;
}
