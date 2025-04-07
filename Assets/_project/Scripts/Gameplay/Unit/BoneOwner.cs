using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BoneOwner : MonoBehaviour
{
    ///1 hand
    ///2 throw
    ///3 go away <summary>
    [Inject] private PlayerMouseInput _mouseInput;
    [Inject] private BoneInput _boneInput;
    [Inject] private Camera _camera;
    [SerializeField] private Dog _dog;
    [SerializeField] private float _speedRotation;
    [SerializeField] private float _flyTime;   
    [SerializeField] private Transform _boneBody; 
    [SerializeField] private Transform _bonePosition;
    private bool _isDropped; 
    private bool _isHanded; 
    
    private void Start()
    {
        _boneInput.OnBoneAction += Throw;
        LockBone();
    }
    public void LockBone()
    {
        _boneBody.SetParent(_bonePosition, false);
        _boneBody.position = _bonePosition.position;
        _boneBody.GetComponent<CapsuleCollider>().enabled = false;

        _isHanded = true;
        _isDropped = false;
    }

    
    private void Throw()
    {
        if(!_isHanded) return;
        _boneBody.SetParent(null, false);
        _isHanded = false;
        _boneBody.GetComponent<CapsuleCollider>().enabled = true;
        StartCoroutine(ThrowBone());
        StartCoroutine(SpinBone());
    }
    private Vector3 GetGroundPosition()
    {   
        Ray ray = _camera.ScreenPointToRay(_mouseInput.MousePos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return new Vector3(hit.point.x, 0, hit.point.z);
        }
        return Vector3.zero;

    }
    private IEnumerator ThrowBone()
    {
        float time = 0f;
        Vector3 groundPoint = GetGroundPosition();

        while(time < _flyTime)
        {
            time += Time.deltaTime;
            _boneBody.transform.position = Vector3.Lerp(_boneBody.transform.position, groundPoint, time / _flyTime);
            yield return null;
        }

        _isDropped = true;
        
    }
    private IEnumerator SpinBone()
    {
        while(!_isDropped)
        {
            _boneBody.transform.Rotate(0, 0, _speedRotation * Time.deltaTime);
            yield return null;
        }
        StartCoroutine(GoAway());

    }
    public IEnumerator GoAway()
    {
        //yield return new WaitForSeconds(_timeDelay);
        _boneBody.GetComponent<CapsuleCollider>().enabled = false;

        float t = 0;
        while(t < 1f)
        {
            t += Time.deltaTime;
            _boneBody.position += 25f * Time.deltaTime * Vector3.left;
            yield return null;
        }
        _dog.BringBone(_boneBody);
    }

}
