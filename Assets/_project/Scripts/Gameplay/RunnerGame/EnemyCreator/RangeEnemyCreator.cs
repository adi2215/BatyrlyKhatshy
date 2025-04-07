using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class RangeEnemyCreator : EnemyCreator
{
    [Inject] private CharacterController _character;
    [Inject] private NotifyManager _notifyManager;
    [Inject] private EnemyManager _enemyManager;
    [SerializeField] private List<Transform> _spawnPoints;

    [SerializeField] private List<WayPoints> _wayPoints;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private RangeEnemy _enemyPrefab;
    private int SelectRandomPoint() => UnityEngine.Random.Range(0, _spawnPoints.Count);

    public override void Create(int count)
    {
        StartCoroutine(SpawnEnemies(count));
    }
    private void Spawn()
    {
        var enemy = Instantiate(_enemyPrefab, _spawnPoints[SelectRandomPoint()].position, Quaternion.identity);
        enemy.Init(_character, _enemyManager);
        _notifyManager.Notify(enemy.transform.position);
        StartCoroutine(enemy.StartFollowPoints(_wayPoints[UnityEngine.Random.Range(0, _wayPoints.Count)].Points));
        StartCoroutine(enemy.StartShooting());

    }
    private IEnumerator SpawnEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Spawn();
            yield return new WaitForSeconds(_spawnDelay);

        }
    }

}

[Serializable]
public class WayPoints
{
    [SerializeField] public List<Transform> Points;
}
