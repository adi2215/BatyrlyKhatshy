using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    [Inject] private PlayerInput _input;
    [Inject] private CharacterController _controller;

    #region Gravity
    [Header("Gravity Config")]
    [SerializeField] private PlayerGravity _gravity;
    private Vector3 _velocity;
    #endregion
    #region Movement
    [Header("Moving Config")]
    [SerializeField] private float _movingSpeed;
    [Range(1f, 20f)][SerializeField] private float _airSmoothSpeed;
    [Range(1f, 20f)][SerializeField] private float _groundSmoothSpeed;
    private float _currentSmoothSpeed;
    private Vector3 _smoothMovementInput = new Vector3();
    //private bool _isMoving = false;
    #endregion
    private void FixedUpdate()
    {
        MoveLogic();
        GravityUpdate();
    }
    private void GravityUpdate()
    {
        if (!_controller.isGrounded)
        {
            _currentSmoothSpeed = _airSmoothSpeed;
            _gravity.UpdateVelocity();
        }
        else
        {
            _currentSmoothSpeed = _groundSmoothSpeed;
            _gravity.ResetVelocity();
        }
    }
    private void CalculateMovementSmoothing()
    {

        _smoothMovementInput = Vector3.Lerp(_smoothMovementInput, _input.GetMoveDirection, Time.fixedDeltaTime * _currentSmoothSpeed);
    }
    private void MoveLogic()
    {

        CalculateMovementSmoothing();
        //_smoothMovementInput = ;
        Vector3 moveDirection = transform.TransformDirection(_smoothMovementInput) ;
        Vector3 moving = ((moveDirection *_movingSpeed * Time.fixedDeltaTime) + _gravity.Velocity) ;

        _controller.Move(moving);

        if (_smoothMovementInput.magnitude <= 0.001f)
            _smoothMovementInput = Vector3.zero;

    }


}
[System.Serializable]
public class PlayerGravity
{
    [SerializeField] private Vector3 _gravityForce;
    [SerializeField] private Vector3 _isGroundedVelocity;
    [SerializeField] private float _gravityScale;

    private Vector3 _velocity = Vector3.zero;
    public Vector3 GravityForce => _gravityForce;
    public float GravityScale => _gravityScale;
    public void UpdateVelocity()
    {
        _velocity += _gravityForce * _gravityScale * Time.deltaTime;
    }
    public Vector3 Velocity
    {
        get { return _velocity; }
        set { _velocity = value; }
    }
    public void ResetVelocity()
    {
        _velocity = _isGroundedVelocity;
    }

}
