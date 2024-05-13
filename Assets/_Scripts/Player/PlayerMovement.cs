using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NetworkTransformUnreliable), typeof(NetworkIdentity), typeof(Rigidbody))]
public class PlayerMovement : NetworkBehaviour
{

    public InputReader _inputReader;
    public Rigidbody _rb;

    public int speed = 60;
    private Vector2 _direction;

    private PlayerScore _playerScoreUI;
    private GlobalScore _globalScoreUI;

    [SyncVar(hook = nameof(DisplayPlayerScore))]
    public int playerScore = 0;

    [SyncVar]
    public string _Name = "";

    private Database _dataBase;

    private void Awake()
    {
        _inputReader = ScriptableObject.CreateInstance<InputReader>();
        _rb = GetComponent<Rigidbody>();
        _playerScoreUI = FindObjectOfType<PlayerScore>();
        _globalScoreUI = FindObjectOfType<GlobalScore>();
        _dataBase = FindObjectOfType<Database>();
    }


    private void Start()
    {
        if (isLocalPlayer)
        {
            _Name = PlayerName.GetLocalName(); // Get the name you set at the beginning
            _dataBase.Save(_Name, playerScore, out playerScore); // Insert the players initial data into the table. Get the score out from the database and store it in playerScore
            _playerScoreUI.DisplayPlayerScore(playerScore); // Update player score 
            CommandChangeScore(playerScore); // Update the global score
        }
        
    }

    private void DisplayPlayerScore(int oldScore, int newPlayerScore)
    {
        if (isLocalPlayer)
        {
            _playerScoreUI.DisplayPlayerScore(newPlayerScore);
            _dataBase.UpdateScore(_Name, playerScore);
        }
    }

    [Command(requiresAuthority = false)]
    public void CommandSetPlayerScore(int change) 
    {
            playerScore += change;
            CommandChangeScore(change); 
    }

    [Command(requiresAuthority = false)]
    private void CommandChangeScore(int score)
    { 
        _globalScoreUI.globalScoreValue += score;
    }
    
    private void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            _rb.AddForce(new Vector3(speed * _inputReader.Move().x, 0, speed * _inputReader.Move().y));
        }   
    }
}