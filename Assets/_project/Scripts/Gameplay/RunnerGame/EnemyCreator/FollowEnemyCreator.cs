using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FollowEnemyCreator : EnemyCreator
{
    [Inject] private NotifyManager _notifyManager;
    [Inject] private CharacterController _character;
    [Inject] private EnemyManager _enemyManager;
    [SerializeField] private float _spawnDelay;

    [SerializeField] private List<Transform> _spawnPoints;

    [SerializeField] private FollowEnemy _enemyPrefab;


    public override void Create(int count)
    {    
         StartCoroutine(SpawnEnemies(count));
    }
    private int SelectRandomPoint() => Random.Range(0, _spawnPoints.Count);
    
    private void Spawn()
    {
       var enemy = Instantiate(_enemyPrefab, _spawnPoints[SelectRandomPoint()].position, Quaternion.identity);
       enemy.Init(_enemyManager);
        StartCoroutine(enemy.StartFollow(_character));
       _notifyManager.Notify(enemy.transform.position);
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
