using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnvironmentPerception : MonoBehaviour
{
    [SerializeField] public LayerMask EnvironmentLayer;

    [SerializeField] Wall[] AllWalls;
    [SerializeField] List<Wall> _PerceivedWalls;
    [SerializeField] List<Wall> _HiddenWalls;
    [SerializeField] EnemyView EnemyView;


    public List<Wall> PerceivedWalls => _PerceivedWalls;
    public List<Wall> HiddenWalls => _HiddenWalls;

    private void Start()
    {
        GetSurroundingWalls();
    }

    public void PerceiveEnviroment()
    {
        foreach(var w in AllWalls)
        {
            if (!PerceivedWalls.Contains(w) && CanView(w, EnemyView.FieldOfView))
            {
                PerceivedWalls.Add(w);
            }
        }
        foreach (var w in PerceivedWalls)
        {
            if(!_HiddenWalls.Contains(w))
                w.Perceived(true);
        }
    }

    public void GetSurroundingWalls()
    {
        Collider[] result = new Collider[WallsCounter.Instance.Count];
        Physics.OverlapSphereNonAlloc(transform.position, EnemyView.ViewDistance, result, EnvironmentLayer);
        result = result.Where<Collider>(x => x != null).ToArray();
        AllWalls = (from collider in result select collider.GetComponent<Wall>()).ToArray();

    }

    private bool CanView(Wall wall, float _fieldOfView)
    {
        Vector3 dir = EnemyView.transform.forward;
        //float _fieldOfView = EnemyView.FieldOfView;
        float _viewDistance = EnemyView.ViewDistance;

        float angle = _fieldOfView / 2f * Mathf.Deg2Rad;

        Vector3 dir1 = new Vector3(
            dir.x * Mathf.Cos(angle) - dir.z * Mathf.Sin(angle),
            0,
            dir.z * Mathf.Cos(angle) + dir.x * Mathf.Sin(angle));
        dir1.Normalize();
        Vector3 dir2 = new Vector3(
            dir.x * Mathf.Cos(-angle) - dir.z * Mathf.Sin(-angle),
            0,
            dir.z * Mathf.Cos(-angle) + dir.x * Mathf.Sin(-angle));
        dir2.Normalize();
        Ray ray1 = new Ray();
        Ray ray2 = new Ray();
        ray1.origin = EnemyView.transform.position;
        ray1.direction = dir1;
        ray2.origin = EnemyView.transform.position;
        ray2.direction = dir2;

        Vector3 wdir = (wall.transform.position - EnemyView.transform.position).normalized;

        return Vector3.Dot(dir, wdir) > Mathf.Cos(_fieldOfView / 2f * Mathf.Deg2Rad)
        || wall.Collider.bounds.IntersectRay(ray1)
        || wall.Collider.bounds.IntersectRay(ray2);
    }

    public List<Wall> GetFrontWalls()
    {
        List<Wall> walls = new List<Wall>();

        foreach(var w in PerceivedWalls)
        {
            if (CanView(w, EnemyView.FieldOfView * 1.2f))
            {
                walls.Add(w);
            }
        }

        return walls;
    }

    public void CheckHiddenPlaces(Vector3 enemyPos, Vector3 playerPos, float maxDot)
    {
        _HiddenWalls.Clear();

        Vector3 playerDir = (playerPos - enemyPos).normalized;

        foreach(var wall in PerceivedWalls)
        {
            Vector3 dir = (wall.transform.position - enemyPos).normalized;

            //if (Vector3.Dot(playerDir, dir) < maxDot)
                _HiddenWalls.Add(wall);
        }
        //test
        foreach (var w in _HiddenWalls)
        {
            w.PossibleEscape(true);
        }
        //test
    }

}
