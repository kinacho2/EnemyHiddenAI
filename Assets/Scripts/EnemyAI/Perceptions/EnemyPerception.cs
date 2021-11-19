using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPerception : MonoBehaviour
{
    [SerializeField] EnemyView _EnemyView;
    [SerializeField] PlayerDistanceDetector _PlayerDistanceDetector;
    [SerializeField] EnvironmentPerception _EnvironmentPerception;
    [SerializeField] NavMeshDistanceChecker _NavMeshDistanceChecker;
    [SerializeField] HiddenPlacesChecker _HiddenPlacesChecker;

    public EnemyView EnemyView => _EnemyView;
    public PlayerDistanceDetector PlayerDistanceDetector => _PlayerDistanceDetector;
    public EnvironmentPerception EnvironmentPerception => _EnvironmentPerception;
    public NavMeshDistanceChecker NavMeshDistanceChecker => _NavMeshDistanceChecker;
    public HiddenPlacesChecker HiddenPlacesChecker => _HiddenPlacesChecker;


    private void Start()
    {
        StartCoroutine(PerceiveWallsCoroutine());
    }

    public void CustomUpdate()
    {
        _EnvironmentPerception.PerceiveEnviroment();
        _PlayerDistanceDetector.CheckDistance();

    }

    IEnumerator PerceiveWallsCoroutine()
    {
        var Wait = new WaitForSeconds(.3f);
        while (true)
        {
            yield return Wait;
            _EnvironmentPerception.GetSurroundingWalls();
        }
    }

}
