using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody Rigidbody;
    [SerializeField] float Speed = 2;
    [SerializeField] Vector3 Direction = Vector3.zero;

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Mathf.Abs(x) > 0.01f)
            Direction.x = x;
        else
            Direction.x = 0;

        if (Mathf.Abs(z) > 0.01f)
            Direction.z = z;
        else
            Direction.z = 0;
    }

    private void FixedUpdate()
    {
        if(Direction.magnitude > .1f)
            Rigidbody.MovePosition(transform.position + Speed * Direction * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var enemy = collision.collider.GetComponent<EnemyAI>();
        if (enemy)
        {
            SceneManager.LoadScene("Win");
        }
    }

}

