using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPerception : MonoBehaviour
{
    [SerializeField] EnemyView _EnemyView;
    [SerializeField] PlayerDistanceDetector _PlayerDistanceDetector;
    [SerializeField] EnvironmentPerception _EnvironmentPerception;

    public EnemyView EnemyView => _EnemyView;
    public PlayerDistanceDetector PlayerDistanceDetector => _PlayerDistanceDetector;
    public EnvironmentPerception EnvironmentPerception => _EnvironmentPerception;



    private void Update()
    {
        _EnvironmentPerception.PerceiveEnviroment();
    }

}
