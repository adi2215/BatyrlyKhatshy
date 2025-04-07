using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class ObstacleGenerator : MonoBehaviour
{
/*    [SerializeField]
    private PlatformsData _platformData;*/
    [Inject] private NotifyManager _notifyManager;

    [Inject(Id = "SpawnLocation")]
    Transform _spawnLocation;
    [Inject(Id = "KillLocation")]
    Transform _killLocation;

    [Inject(Id = "FarLocation")]
    Transform _farLocation;
    [Inject(Id = "NearLocation")]
    Transform _nearLocation;
    [SerializeField]
    private PlatformsSetting _platformData;



    [SerializeField] private List<GameObject> _prefabs;
    //private List<GameObject> _platforms = new();
    private int _index;


    private Vector3 RandomPosition() => Vector3.Lerp(_nearLocation.position, _farLocation.position, UnityEngine.Random.value);
    private int RandomIndex() => UnityEngine.Random.Range(0, _prefabs.Count);
    public void SpawnObject()
    {
        GameObject prefab = Instantiate(_prefabs[RandomIndex()]);
        prefab.transform.position = RandomPosition() + _platformData.distanceUp;
        _notifyManager.Notify(prefab.transform.position);

        StartCoroutine(MoveObstacle(prefab));
    }
    private void KillObject(GameObject obstacle)
    {
        Destroy(obstacle);
    }

    private IEnumerator MoveObstacle(GameObject obstacle)
    {
        while(obstacle.transform.position.x > _killLocation.position.x)
        {
            obstacle.transform.position += _platformData.directionMove * _platformData.speed * Time.deltaTime;
            yield return null;
        }
        KillObject(obstacle);
    }
}
