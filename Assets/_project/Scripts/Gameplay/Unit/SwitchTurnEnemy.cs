using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class SwitchTurnEnemy : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    private PlayerMovement player;
    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();

    }
    private void Update()
    {
        if(!player) return;
        if(player.transform.position.x - transform.position.x < 0f)
        {
            _animator.SetBool("isRight", false);
        }
        else{
            _animator.SetBool("isRight", true);
        }

    }
}
