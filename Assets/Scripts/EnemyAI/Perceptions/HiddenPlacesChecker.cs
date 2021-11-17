using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenPlacesChecker : MonoBehaviour
{
    
    [SerializeField] List<Vector3> _HiddenPositions;


    public List<Vector3> HiddenPositions => _HiddenPositions;


    public void CheckHiddenPositions(List<Wall> walls, Vector3 playerPos, float minDistance, LayerMask enviromentLayer)
    {
        _HiddenPositions.Clear();
        foreach (var w in walls)
        {
            var dirVector = -(playerPos - w.transform.position);

            var initPos = playerPos + dirVector * 1.5f;

            RaycastHit hit;

            Physics.Raycast(initPos, -dirVector.normalized, out hit, dirVector.magnitude, enviromentLayer);

            if (hit.transform && Vector3.Distance(playerPos, hit.point) > minDistance)
            {
                _HiddenPositions.Add(hit.point);
            }
        }
    }

    private void Update()
    {
        DrawGizmos();
    }

    public void DrawGizmos()
    {
        foreach(var v in HiddenPositions)
        {
            
            Popcron.Gizmos.Sphere(v, .1f, Color.yellow, true, 160);
        }
    }

    public void OnDrawGizmos()
    {
        UnityEngine.Gizmos.color = Color.yellow;

        foreach (var v in HiddenPositions)
        {
            UnityEngine.Gizmos.DrawSphere(v, .1f);
        }
    }
}
