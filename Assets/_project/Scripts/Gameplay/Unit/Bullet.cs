using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _timeAlive;

    public IEnumerator StartShoot(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        direction.y = 0;
        float time = 0;
        while(time < _timeAlive)
        {
            time += Time.deltaTime;
            if(this != null)
                transform.position += direction * Time.deltaTime * _speed;
            else
                yield break;
            yield return null;
        }
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.GetComponent<Health>().GetDamage(1);
            Destroy(this.gameObject);
        }
    }
}
