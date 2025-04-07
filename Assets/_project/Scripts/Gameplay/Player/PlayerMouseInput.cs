
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerMouseInput : IInitializable, ITickable
{
    [Inject] private GameInputAction _input;
    private InputAction _attackAction, _mousePos;
    public Action OnAttack;
    private float _reloadTime = 1f;
    private bool _isReloaded = false;
    private float _time = 0;
    public float ReloadLeft() => _time;

    [Inject] CharacterController _character;
    private Reload reload;

    public void Initialize()
    {
        _attackAction = _input.PlayerInput.AttackAction;

        _attackAction.performed += AttackPerformed;

        _mousePos = _input.PlayerInput.MousePosition;
        reload = _character.GetComponent<Reload>();

    }

    public Vector2 MousePos => _mousePos.ReadValue<Vector2>();
    private void AttackPerformed(InputAction.CallbackContext context)
    {
        if(_isReloaded)
        {
            OnAttack?.Invoke();
            _time = 0;
            _isReloaded = false;
        }
    }

    public void Tick()
    {
        if(_isReloaded) return;
        reload.GetSlider.value = _time;

        _time += Time.deltaTime;
        if(_time > _reloadTime)
        {
            _isReloaded = true;
        }
    }
}
