using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class PlatfomGenerator : MonoBehaviour
{

    [Inject(Id = "SpawnLocation")]
    Transform _spawnLocation;

    [Inject(Id = "KillLocation")]
    Transform _killLocation;

    [SerializeField]
    private Vector3 _directionMove;
    [SerializeField]
    private float _distanceMultiplier;
    [SerializeField]
    [Range(1, 40f)]
    private float _speed;
    [SerializeField]
    public float Speed() => _speed;
    private Vector3 _distanceUp;

    [SerializeField] private List<GameObject> _platformPrefabs;
    private List<GameObject> _platforms = new();
    private int _index;

    private void Awake()
    {
        PreloadPlatforms();      
    }
    private void Update()
    {
        UpdatePosition();
    }
    public void UpdatePosition()
    {
        for (int i = 0; i < _platforms.Count; i++)
        {

            _platforms[i].transform.position += _directionMove * _speed * Time.deltaTime;

            //_platforms[i].transform.position = Vector3.Lerp(_spawnLocation.position, _killLocation.position, a);
            if (_platforms[i].transform.position.x < _killLocation.position.x)
            {
                KillPlatform(_platforms[i]);
                SpawnPlatforms();
                continue;

            }
        }
    }
    private void PreloadPlatforms()
    {
        //_killLocation.position = (_spawnLocation.position) + _directionMove * _distanceMultiplier * _platformPrefabs.Count;
        for (int i = 0; i < _platformPrefabs.Count; i++)
        {
            GameObject platform = Instantiate(_platformPrefabs[i]);
            platform.transform.position = (_spawnLocation.position + _distanceUp) + _directionMove * _distanceMultiplier * i;
            _platforms.Add(platform);
        }
    }
    //private void ShufflePlatforms() => _platformPrefabs.OrderBy(x => Guid.NewGuid()).ToList();
    private void SpawnPlatforms()
    {
        GameObject platform = Instantiate(_platformPrefabs[UnityEngine.Random.Range(0, _platformPrefabs.Count)]);
        platform.transform.position = _spawnLocation.position + _distanceUp;
        _platforms.Add(platform);

    }
    private void KillPlatform(GameObject platform)
    {
        _platforms.Remove(platform);
        Destroy(platform);
    }

}