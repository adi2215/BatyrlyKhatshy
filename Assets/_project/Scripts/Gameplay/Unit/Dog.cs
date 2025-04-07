using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.TextCore.Text;
using Zenject;

public class Dog : MonoBehaviour
{
    [Inject] CharacterController _character;
    [Inject] Bone _bone;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _bonePosition;

    public void BringBone(Transform bonePos)
    {
        StartCoroutine(GoToPoint(bonePos));
    }
    private IEnumerator GoToPoint(Transform bone)
    {
        while((transform.position - bone.position).magnitude > 0.5f)
        {
            transform.position += (bone.position - transform.position).normalized * _speed * Time.deltaTime;
            yield return null;
        }
        LockBone(bone);
        while((transform.position - _character.transform.position).magnitude > 0.5f)
        {
            transform.position += (_character.transform.position - transform.position).normalized * _speed * Time.deltaTime;
            yield return null;
        }
        UnlockBone(bone);

        _character.GetComponent<BoneOwner>().LockBone();
    }
    private void UnlockBone(Transform bone)
    {
        bone.SetParent(null, false);
    }

    private void LockBone(Transform bone)
    {
        bone.SetParent(_bonePosition, true);
        bone.position = _bonePosition.position;
    }
    public IEnumerator EatHuman(Health health)
    {
        while((transform.position - health.transform.position).magnitude > 0.5f)
        {
            transform.position += (health.transform.position - transform.position).normalized * _speed * Time.deltaTime;
            yield return null;
        }
        health.GetDamage(1);
            yield return null;

        BringBone(_bone.transform);
    }

}
