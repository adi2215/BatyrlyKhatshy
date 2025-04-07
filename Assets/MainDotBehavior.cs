using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.InputSystem;
using DG.Tweening;

public class MainDotBehavior : MonoBehaviour
{

    public AudioSource musicSource;
    [Inject] GameInputAction _input;
    private InputAction _spaceAction;



    private void Start()
    {
        musicSource = GetComponent<AudioSource>();
        _spaceAction = _input.PlayerInput.Rhythm;
        _spaceAction.performed += SpaceAction_performed;
        _spaceAction.Enable();
    }



    private void SpaceAction_performed(InputAction.CallbackContext obj)
    {
        transform.DOScale(4f, 0.4f).SetLoops(2, LoopType.Yoyo);
    }
}
