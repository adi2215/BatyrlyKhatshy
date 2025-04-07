using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyManager
{
    private int _countAlife = 0;
    [Inject] RunnerGameManager _gameManager;
    public void AddEnemy()
    {
        _countAlife++;
        Debug.Log(_countAlife + "Enemy Manager ADD");
    }
    public void RemoveEnemy()
    {
        _countAlife--;
        Debug.Log(_countAlife + "Enemy Manager REMOVE");

        if( _countAlife < 1 )
        {
            Debug.Log("Next Level from manager");
            _gameManager.EndEvent();
        }
    }
}
