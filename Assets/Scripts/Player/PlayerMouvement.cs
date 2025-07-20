using System;
using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    public float speed = 5f;
    public KeyCode leftKey = KeyCode.Q;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode upKey = KeyCode.Z;
    public KeyCode downKey = KeyCode.S;
    
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // On ne peut pas bouger le temps que la game n'a pas démarré
        if (!GameManager.Instance._gameStarted)
            return;
        
        Vector3 direction = Vector3.zero;

        if (Input.GetKey(leftKey) )
            direction += Vector3.left;

        if (Input.GetKey(rightKey))
            direction += Vector3.right;

        if (Input.GetKey(upKey ))
            direction += Vector3.forward;

        if (Input.GetKey(downKey))
            direction += Vector3.back;
        
        Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;
        if(newPosition.x < -75 || newPosition.z > 50 || newPosition.x > 75 || newPosition.z < -50)
            return;
        
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        transform.GetChild(0).LookAt(transform.position + direction, Vector3.up);
        _animator.SetFloat("Speed", direction.magnitude);
    }

    public void PlayPushAnimation()
    {
        _animator.SetTrigger("Bump");
    }
}

   
