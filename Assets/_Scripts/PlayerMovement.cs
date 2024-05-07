using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NetworkTransformUnreliable), typeof(NetworkIdentity), typeof(Rigidbody))]
public class PlayerMovement : NetworkBehaviour
{

    public InputReader _inputReader;
    private Rigidbody _rb;


    public int speed = 60;
    private Vector2 _direction;


    private void Awake() 
    {
        _inputReader = ScriptableObject.CreateInstance<InputReader>();
        _rb = GetComponent<Rigidbody>();
    }

    //private void OnEnable() 
    //{
    //    _inputReader._OnMoveEvent += HandleMovement;
    //}


    //private void OnDisable() 
    //{
    //    _inputReader._OnMoveEvent -= HandleMovement;
    //}

    private void FixedUpdate()
    {
        //_inputReader.Move();
        
        _rb.AddForce(new Vector3(speed*_inputReader.Move().x, 0, speed*_inputReader.Move().y));
    }

    private void HandleMovement(Vector2 dir) 
    {
        _direction = dir;
        Debug.Log(dir);
    }
}
