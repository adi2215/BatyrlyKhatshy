using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FollowEnemy : Enemy
{
    [SerializeField] private float _speed;
    [SerializeField] private CharacterController _controller;
    private EnemyManager _enemyManager;
    public void Init(EnemyManager manager)
    {
        _enemyManager = manager;
        _enemyManager.AddEnemy();
        alife = true;
    }
    public override void Die()
    {
        base.Die();
        _enemyManager.RemoveEnemy();
    } 
    public IEnumerator StartFollow(CharacterController player)
    {
        while(alife)
        {
            if(_controller)
            _controller.Move(-(transform.position - player.transform.position).normalized * Time.deltaTime * _speed);
            yield return null;
        }       
    }
}
