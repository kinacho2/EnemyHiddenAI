using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDistanceDetector : MonoBehaviour
{
    [SerializeField] float MinDistance = 2;
    [SerializeField] PlayerContainer PlayerContainer;

    [SerializeField] bool _playerIsNear = false;
    public bool PlayerIsNear => _playerIsNear;

    private void Update()
    {
        _playerIsNear = (Vector3.Distance(transform.position, PlayerContainer.Player.transform.position) <= MinDistance);
        
    }

}
