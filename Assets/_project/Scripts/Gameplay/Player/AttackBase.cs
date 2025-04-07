using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Unity.VisualScripting;

public class AttackBase : MonoBehaviour
{

    [SerializeField] private Transform _attackTransform;
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private int _damage;


    public void ExecuteAttack()
    {
        RaycastHit[] hits = Physics.SphereCastAll(_attackTransform.position, _attackRange, Vector3.right, 0f, _layer);
        
        foreach (RaycastHit hit in hits)
        {
            Health health = hit.collider.GetComponent<Health>();
            if(health != null)
            health.GetDamage(_damage);

            Bullet bullet = hit.collider.GetComponent<Bullet>();
            if(bullet != null)
            Debug.Log("bullet");

        }
    }

    public void ExecuteAttackForEnemy()
    {
        RaycastHit[] hits = Physics.SphereCastAll(_attackTransform.position, _attackRange, Vector3.right, 0f, _layer);
        
        foreach (RaycastHit hit in hits)
        {
            Health health = hit.collider.GetComponent<Health>();
            if(health != null)
            {
                health.GetDamage(_damage);
            }
                

        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_attackTransform.position, _attackRange);
    }
}
