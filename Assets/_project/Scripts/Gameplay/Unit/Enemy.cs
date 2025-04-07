using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public bool alife = true;
    public virtual void Die()
    {
        alife = false;
    }

}
