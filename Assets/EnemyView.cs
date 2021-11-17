using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [SerializeField] float _fieldOfView = 60;
    [SerializeField] float _viewDistance = 5;
    [SerializeField] PlayerContainer PlayerContainer;
    [SerializeField] bool _seePlayer;

    public Vector3 PlayerLastPosition { get; protected set; }
    public float ViewDistance => _viewDistance;
    public float FieldOfView => _fieldOfView;
    
    public bool SeePlayer => _seePlayer;

    void Update()
    {
        Vector3 forward = transform.forward;
        Vector3 playerPos = PlayerContainer.Player.transform.position;

        Vector3 dirToPlayer = (PlayerContainer.Player.transform.position - transform.position);

        if(dirToPlayer.magnitude < _viewDistance && Vector3.Dot(forward, dirToPlayer.normalized) > Mathf.Cos(_fieldOfView/2f*Mathf.Deg2Rad))
        {
            
            RaycastHit hit;
            
            Physics.Raycast(transform.position, dirToPlayer, out hit, _viewDistance);

            if (hit.transform == PlayerContainer.Player.transform)
            {
                _seePlayer = true;
                PlayerLastPosition = PlayerContainer.Player.transform.position;
            }
            else
            {
                _seePlayer = false;
            }
        }
        else
        {
            _seePlayer = false;
        }

    }


    private void OnDrawGizmos()
    {
        //DRAW FIELD OF VIEW
        Gizmos.color = Color.blue;

        Vector3 dir = transform.forward;
        //Gizmos.DrawFrustum(transform.position, FieldOfView, ViewDistance, .01f, 1);
        Gizmos.DrawLine(transform.position, transform.position + dir * _viewDistance);

        Gizmos.color = Color.red;

        float angle = _fieldOfView / 2f * Mathf.Deg2Rad;

        Vector3 dir2 = new Vector3(
            dir.x * Mathf.Cos(angle) - dir.z * Mathf.Sin(angle), 
            0, 
            dir.z * Mathf.Cos(angle) + dir.x * Mathf.Sin(angle));
        dir2.Normalize();
        Gizmos.DrawLine(transform.position, transform.position + dir2 * _viewDistance);

        Vector3 dir3 = new Vector3(
            dir.x * Mathf.Cos(-angle) - dir.z * Mathf.Sin(-angle),
            0,
            dir.z * Mathf.Cos(-angle) + dir.x * Mathf.Sin(-angle));
        dir3.Normalize();
        Gizmos.DrawLine(transform.position, transform.position + dir3 * _viewDistance);

        //DRAW PLAYER LAST POSITION

        Gizmos.color = Color.green*Color.grey;
        Gizmos.DrawSphere(PlayerLastPosition, .1f);
    }

}
