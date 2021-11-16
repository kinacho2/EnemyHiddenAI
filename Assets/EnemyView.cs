using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [SerializeField] float FieldOfView = 60;
    [SerializeField] float ViewDistance = 5;
    [SerializeField] PlayerContainer PlayerContainer;
    [SerializeField] bool _seePlayer;

    public Vector3 PlayerLastPosition { get; protected set; }

    
    public bool SeePlayer => _seePlayer;

    void Update()
    {
        Vector3 forward = transform.forward;
        Vector3 playerPos = PlayerContainer.Player.transform.position;

        Vector3 dirToPlayer = (PlayerContainer.Player.transform.position - transform.position);

        if(dirToPlayer.magnitude < ViewDistance && Vector3.Dot(forward, dirToPlayer.normalized) > Mathf.Cos(FieldOfView/2f*Mathf.Deg2Rad))
        {
            
            RaycastHit hit;
            
            Physics.Raycast(transform.position, dirToPlayer, out hit, ViewDistance);

            if (hit.transform == PlayerContainer.Player.transform)
            {
                Debug.Log("I See Player");
                _seePlayer = true;
                PlayerLastPosition = PlayerContainer.Player.transform.position;
            }
            else
            {
                Debug.Log("I See Nothing");
                _seePlayer = false;
            }
        }
        else
        {
            Debug.Log("I See Nothing");
            _seePlayer = false;
        }

    }


    private void OnDrawGizmos()
    {
        //DRAW FIELD OF VIEW
        Gizmos.color = Color.blue;

        Vector3 dir = transform.forward;
        //Gizmos.DrawFrustum(transform.position, FieldOfView, ViewDistance, .01f, 1);
        Gizmos.DrawLine(transform.position, transform.position + dir * ViewDistance);

        Gizmos.color = Color.red;

        float angle = FieldOfView / 2f * Mathf.Deg2Rad;

        Vector3 dir2 = new Vector3(
            dir.x * Mathf.Cos(angle) + dir.z * Mathf.Sin(angle), 
            dir.y, 
            dir.z * Mathf.Cos(angle) + dir.x * Mathf.Sin(angle));

        Gizmos.DrawLine(transform.position, transform.position + dir2 * ViewDistance);

        Vector3 dir3 = new Vector3(
            dir.x * Mathf.Cos(-angle) + dir.z * Mathf.Sin(-angle),
            dir.y,
            dir.z * Mathf.Cos(-angle) + dir.x * Mathf.Sin(-angle));

        Gizmos.DrawLine(transform.position, transform.position + dir3 * ViewDistance);

        //DRAW PLAYER LAST POSITION

        Gizmos.color = Color.green*Color.grey;
        Gizmos.DrawSphere(PlayerLastPosition, .1f);
    }

}
