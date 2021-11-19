using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [SerializeField] float _fieldOfView = 60;
    [SerializeField] float _viewDistance = 5;
    [SerializeField] PlayerContainer PlayerContainer;
    [SerializeField] bool _seePlayer;

    public Vector3 PlayerLastPosition { get; set; }
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
        DrawGizmos();
    }

   
    public void OnDrawGizmos()
    {
        //DRAW FIELD OF VIEW

        Vector3 dir = transform.forward;

        UnityEngine.Gizmos.color = Color.blue;
        UnityEngine.Gizmos.DrawLine(transform.position, transform.position + dir * _viewDistance);


        float angle = _fieldOfView / 2f * Mathf.Deg2Rad;

        Vector3 dir2 = new Vector3(
            dir.x * Mathf.Cos(angle) - dir.z * Mathf.Sin(angle), 
            0, 
            dir.z * Mathf.Cos(angle) + dir.x * Mathf.Sin(angle));
        dir2.Normalize();

        UnityEngine.Gizmos.color = Color.red;
        UnityEngine.Gizmos.DrawLine(transform.position, transform.position + dir2 * _viewDistance);


        Vector3 dir3 = new Vector3(
            dir.x * Mathf.Cos(-angle) - dir.z * Mathf.Sin(-angle),
            0,
            dir.z * Mathf.Cos(-angle) + dir.x * Mathf.Sin(-angle));
        dir3.Normalize();

        UnityEngine.Gizmos.DrawLine(transform.position, transform.position + dir3 * _viewDistance);

   
        UnityEngine.Gizmos.color = Color.green*Color.grey;
        UnityEngine.Gizmos.DrawSphere(PlayerLastPosition, .1f);

   }


    public void DrawGizmos()
    {
        //DRAW FIELD OF VIEW

        Vector3 dir = transform.forward;


        Popcron.Gizmos.Line(transform.position, transform.position + dir * _viewDistance, Color.blue);

        float angle = _fieldOfView / 2f * Mathf.Deg2Rad;

        Vector3 dir2 = new Vector3(
            dir.x * Mathf.Cos(angle) - dir.z * Mathf.Sin(angle),
            0,
            dir.z * Mathf.Cos(angle) + dir.x * Mathf.Sin(angle));
        dir2.Normalize();


        Popcron.Gizmos.Line(transform.position, transform.position + dir2 * _viewDistance, Color.red);


        Vector3 dir3 = new Vector3(
            dir.x * Mathf.Cos(-angle) - dir.z * Mathf.Sin(-angle),
            0,
            dir.z * Mathf.Cos(-angle) + dir.x * Mathf.Sin(-angle));
        dir3.Normalize();


        Popcron.Gizmos.Line(transform.position, transform.position + dir3 * _viewDistance, Color.red);

        //DRAW PLAYER LAST POSITION
        Popcron.Gizmos.Sphere(PlayerLastPosition, .1f, Color.green * Color.grey);
    }
}
