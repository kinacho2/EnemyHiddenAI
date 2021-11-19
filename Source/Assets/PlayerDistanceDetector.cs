using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDistanceDetector : MonoBehaviour
{
    [SerializeField] float MinDistance = 2;
    [SerializeField] EnemyView EnemyView;
    [SerializeField] PlayerContainer PlayerContainer;
    [SerializeField] Vector3 _PlayerLastPosition;
    [SerializeField] bool _playerIsNear = false;
    [SerializeField] bool _earSomethingTooNear = false;

    public bool PlayerIsNear => _playerIsNear;
    public bool EarSomethingTooNear => _earSomethingTooNear;
    public Vector3 PlayerLastPosition => _PlayerLastPosition;
    
    public void CheckDistance()
    {

        bool aux = (Vector3.Distance(transform.position, PlayerContainer.Player.transform.position) <= MinDistance);
        bool aux2 = (Vector3.Distance(transform.position, PlayerContainer.Player.transform.position) <= MinDistance / 2f);
        var playerDir = PlayerContainer.Player.transform.position - transform.position;
        RaycastHit hit;
        Physics.Raycast(transform.position, playerDir.normalized, out hit, MinDistance);
        if (hit.transform && hit.transform != PlayerContainer.Player.transform)
            aux = false;
        _playerIsNear = aux || aux2;
        _earSomethingTooNear = aux2;
        if (_playerIsNear)
        {
            _PlayerLastPosition = PlayerContainer.Player.transform.position;
            if (_earSomethingTooNear)
            {
                EnemyView.PlayerLastPosition = PlayerContainer.Player.transform.position;

            }
        }
    }

}
