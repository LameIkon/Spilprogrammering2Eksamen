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
    private GlobalScore _globalScoreUI;
    private EnemyScore _enemyScoreUI;

    [SyncVar(hook = nameof(DisplayPlayerScore))]
    public int playerScore = 0;
    public string _Name;

    public SaveLoadManager _saveLoadManager;
    private GameData _gameData;

    private void Awake()
    {
        _inputReader = ScriptableObject.CreateInstance<InputReader>();
        _rb = GetComponent<Rigidbody>();
        _playerScoreUI = FindObjectOfType<PlayerScore>();
        _globalScoreUI = FindObjectOfType<GlobalScore>();
        _Name = DataBaseDataStore._Instance._Name;

    }

    private void DisplayPlayerScore(int oldScore, int newPlayerScore)
    {
        if (isLocalPlayer)
        {
            _playerScoreUI.DisplayPlayerScore(newPlayerScore);
        }
    }

    [Command(requiresAuthority = false)]
    public void CommandSetPlayerScore(int change) 
    {
        playerScore += change;
        CommandChangeScore(change);
        _saveLoadManager.SaveGame();
    }

    [Command(requiresAuthority = false)]
    private void CommandChangeScore(int score)
    { 
        _globalScoreUI.globalScoreValue += score;
    }
    
    private void FixedUpdate()
    {
        //_inputReader.Move();
        if (isLocalPlayer)
        {
            _rb.AddForce(new Vector3(speed * _inputReader.Move().x, 0, speed * _inputReader.Move().y));
        }

    }
}
