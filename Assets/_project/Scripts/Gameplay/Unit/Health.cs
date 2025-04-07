using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

[RequireComponent(typeof(VisualImpact))]
public class Health : MonoBehaviour
{
    [SerializeField] private int _health;
    private VisualImpact _vImpact;
    private void Start()
    {
        _vImpact = GetComponent<VisualImpact>(); 
    }
    public void GetDamage(int damage)
    {
        _vImpact.DamageImpact();
        _health -= damage;
        if( _health < 1)
        {
            GetComponent<Death>().Die();
            return;
        }
    }



    







}
