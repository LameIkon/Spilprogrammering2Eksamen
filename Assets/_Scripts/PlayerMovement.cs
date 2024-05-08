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
    
    private PlayerScore _playerScoreUI;
    
    [SyncVar(hook = nameof(DisplayPlayerScore))]
    public int playerScore = 0;
    
    private void Awake() 
    {
        _inputReader = ScriptableObject.CreateInstance<InputReader>();
        _rb = GetComponent<Rigidbody>();
        _playerScoreUI = FindObjectOfType<PlayerScore>();
    }

    private void DisplayPlayerScore(int oldScore, int newPlayerScore)
    {
        if (isLocalPlayer)
        {
            _playerScoreUI.DisplayPlayerScore(newPlayerScore);
        }
    }

    [Command(requiresAuthority = false)]
    private void CommandSetPlayerScore(int change) => playerScore += change; 
    
    private void OnTriggerEnter(Collider col)
    {
        // Updates the score by 1 everytime a coin is picked up
        if (col.CompareTag("Coin")) CommandSetPlayerScore(+1); 
    }

    
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
    
    // private void OnEnable() 
    // {
    //     _inputReader._OnMoveEvent += HandleMovement;
    // }
    //
    // private void OnDisable() 
    // {
    //     _inputReader._OnMoveEvent -= HandleMovement;
    // }
    
}
