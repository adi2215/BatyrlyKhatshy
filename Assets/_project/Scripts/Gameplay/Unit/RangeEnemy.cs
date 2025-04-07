using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Lumin;
using Zenject;

public class RangeEnemy : Enemy 
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _gunPosition;
    [SerializeField] private float _timeReload;
    [SerializeField] private float _speed;
    private Health _health;
    private Reload _reload; 
    private CharacterController _character;
    private EnemyManager _enemyManager;
    public void Init(CharacterController character, EnemyManager manager)
    {   
        _character = character;
        _enemyManager = manager;
        _enemyManager.AddEnemy();
        _health = GetComponent<Health>();
        _reload = GetComponent<Reload>();
        alife = true;
    }
    public override void Die()
    {
        base.Die();
        _enemyManager.RemoveEnemy();
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(_bulletPrefab, _gunPosition.position, Quaternion.identity);
        bullet.transform.LookAt(_character.transform);
        StartCoroutine(bullet.StartShoot(_character.transform.position));
    }
    public IEnumerator StartShooting()
    {
        float time = 0;
        while(alife)
        {
            time += Time.deltaTime;
            _reload.GetSlider.value = time/_timeReload;
            if(time >= _timeReload)
            {
                Shoot();
                time = 0;
            }
            yield return null;
        }
    }
    public IEnumerator StartFollowPoints(List<Transform> points)
    {
        int i = 0;
            while(alife)
        {
            transform.position += (points[i % points.Count].position - transform.position).normalized * Time.deltaTime * _speed;

            if((transform.position - points[i % points.Count].position).magnitude <= 0.1f)
            {
                i++;
            }
            yield return null;
        }
    }

}
