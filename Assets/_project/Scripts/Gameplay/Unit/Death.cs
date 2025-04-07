using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Health))]
public class Death : MonoBehaviour
{
    [Inject] RunnerGameManager _manager;
    [SerializeField] private PlatformsSetting _setting;
    [SerializeField] private float _jumpDuration;
    [SerializeField] private float _rotateDuration;

    public void Die()
    {
        StartCoroutine(StartDieOperation());
    }

    public IEnumerator StartDieOperation()
    {
        GetComponent<Collider>().enabled = false;

        //var anim = GetComponent<Animator>();
        var enemy = GetComponent<Enemy>();
        if(enemy != null)
            enemy.Die();

       // if(anim != null)
          //  anim.speed = 0.01f;

        transform.DORotate(new Vector3(0, 0, 45f), _rotateDuration, RotateMode.Fast);
        transform.DOJump(new Vector3(-20, 0, 0), 3f, 2, _jumpDuration, false)
        .OnComplete(() => Kill());
        yield return null;
    }
    private void Kill()
    {
        var o = GetComponent<PlayerMovement>();

        if(o == null)
        {
            Destroy(this.gameObject);
            return;
        }

        StartCoroutine(_manager.LoseEvent());
        
    }


}
