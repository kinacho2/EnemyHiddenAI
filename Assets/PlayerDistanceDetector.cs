using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDistanceDetector : MonoBehaviour
{
    [SerializeField] float MinDistance = 2;
    [SerializeField] PlayerContainer PlayerContainer;
    [SerializeField] Vector3 _PlayerLastPosition;
    [SerializeField] bool _playerIsNear = false;

    public bool PlayerIsNear => _playerIsNear;
    public Vector3 PlayerLastPosition => _PlayerLastPosition;
    
    public void CheckDistance()
    {

        bool aux = (Vector3.Distance(transform.position, PlayerContainer.Player.transform.position) <= MinDistance);
        var playerDir = PlayerContainer.Player.transform.position - transform.position;
        RaycastHit hit;
        Physics.Raycast(transform.position, playerDir.normalized, out hit, MinDistance);
        if (hit.transform && hit.transform != PlayerContainer.Player.transform)
            aux = false;
        _playerIsNear = aux || (Vector3.Distance(transform.position, PlayerContainer.Player.transform.position) <= MinDistance/2f);
        if (_playerIsNear)
        {
            _PlayerLastPosition = PlayerContainer.Player.transform.position;
        }
    }

}
