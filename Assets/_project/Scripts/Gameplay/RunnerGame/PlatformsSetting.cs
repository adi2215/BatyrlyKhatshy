using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlatformsSetting
{

    [SerializeField]
    public Vector3 directionMove;
    [SerializeField]
    public float distanceMultiplier;
    [SerializeField]
    [Range(0, 40f)]
    public float speed;
    [SerializeField]
    public Vector3 distanceUp;

}
