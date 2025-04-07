using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem.Composites;
using Zenject;

public class Bone : MonoBehaviour
{   
    [Inject] private Dog _dog;
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.TryGetComponent(out Health health) && collider.tag != "Player")
        {
            health.GetDamage(1);
            this.enabled = false;
            StartCoroutine(_dog.EatHuman(health));            
        }
    }

}
