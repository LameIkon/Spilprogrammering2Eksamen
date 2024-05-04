using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "InputReader")]
public sealed class InputReader : ScriptableObject, PlayerControls.IGameplayActions
{

    private PlayerControls _playerControls;

    private void OnEnable() 
    {
        if (_playerControls == null) 
        {
            _playerControls = new PlayerControls();

            _playerControls.Gameplay.SetCallbacks(this);
        }
    }


    public event Action<Vector2> _OnMoveEvent;

    public void OnMove(InputAction.CallbackContext context)
    {
        _OnMoveEvent?.Invoke(context.ReadValue<Vector2>());
    }
}
