using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FollowAttack : MonoBehaviour
{
    [SerializeField] private AttackBase _attack;
    [SerializeField] private float _reloadTime;
    [SerializeField] private Animator _slashAnim;
    private float time = 0f;
    private PlayerMovement player;
    private Reload reload;
    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        reload = GetComponent<Reload>();
    }
    private void Update()
    {
        time += Time.deltaTime;
        reload.GetSlider.value = time/_reloadTime;
        if (time > _reloadTime)
        {
            time = 0;
            SlashAnimation();
            Attack();
        }
    }
    private void SlashAnimation()
    {
        _slashAnim.Play("RedSlashAnimation");
        _slashAnim.Play("Idle");
    }

    private void Attack()
    {
        Vector3 shootDirection = (player.transform.position - _attack.transform.position).normalized;
        _attack.transform.rotation = Quaternion.LookRotation(shootDirection);
        Vector3 newRotation = _attack.transform.eulerAngles;
        //newRotation.x = 0;
        _attack.transform.eulerAngles = newRotation;
        _attack.ExecuteAttackForEnemy();
    }
}
