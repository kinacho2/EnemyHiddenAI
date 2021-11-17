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
    private void Update()
    {
        _playerIsNear = (Vector3.Distance(transform.position, PlayerContainer.Player.transform.position) <= MinDistance);
        if (_playerIsNear)
            _PlayerLastPosition = PlayerContainer.Player.transform.position;
    }

}
