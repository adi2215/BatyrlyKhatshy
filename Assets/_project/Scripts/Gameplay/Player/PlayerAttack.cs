using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Zenject;

public class PlayerAttack : MonoBehaviour
{
    [Inject] private PlayerMouseInput _mouseInput;
    [Inject] Camera _camera;
    [SerializeField] private AttackBase _attack;
    [SerializeField] private Animator _slashAnim;
    private void Start()
    {   
        _mouseInput.OnAttack += RotateToMouse;
        _mouseInput.OnAttack += _attack.ExecuteAttack;
        _mouseInput.OnAttack += SlashAnimation;
    }
    private void SlashAnimation()
    {
        _slashAnim.Play("SlashAttack");
        _slashAnim.Play("Idle");
    }

    private void RotateToMouse()
    {
        Ray ray = _camera.ScreenPointToRay(_mouseInput.MousePos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 shootDirection = (hit.point - _attack.transform.position).normalized;
            _attack.transform.rotation = Quaternion.LookRotation(shootDirection);
            Vector3 newRotation = _attack.transform.eulerAngles;
            newRotation.x = 0;
            _attack.transform.eulerAngles = newRotation;

        }

    }
    
}
