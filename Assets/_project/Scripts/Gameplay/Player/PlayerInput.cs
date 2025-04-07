using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerInput : IInitializable
{
    [Inject] GameInputAction _input;

    private InputAction _moveAction;

    public void Initialize()
    {

        _moveAction = _input.PlayerInput.MoveAction;

        //_moveAction.performed += movePerf;

        _input.Enable();
    }

    private void movePerf(InputAction.CallbackContext context)
    {
        Debug.Log(GetMoveDirection);

    }
    public Vector3 GetMoveDirection => new Vector3(_moveAction.ReadValue<Vector2>().x, 0, _moveAction.ReadValue<Vector2>().y);

}
