using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerCamera : IInitializable
{
    [Inject] private CharacterController _character;
    [Inject] private CinemachineVirtualCamera _camera;
    private bool _isFollowCamera;
    public PlayerCamera(bool isFollowCamera)
    {
        _isFollowCamera = isFollowCamera;
    }
    public void Initialize()
    {
        if (_isFollowCamera)
        {
            _camera.Follow = _character.transform;
            _camera.LookAt = _character.transform;
        }
        else
        {
            _camera.Follow = null;
            _camera.LookAt = null;
        }
        StartLevitation();
    }

    public float levitationDistance = 0.5f; // How far the camera moves up and down
    public float levitationDuration = 2f; 
    public void StartLevitation()
    {
        _camera.transform.DOMoveY(_camera.transform.position.y + levitationDistance, levitationDuration / 2)
            .SetEase(Ease.InOutSine) // Smooth sine easing for natural motion
            .SetLoops(-1, LoopType.Yoyo); // Infinite looping (Yoyo alternates up and down)
    }
    public void GentleShake()
    {
        Sequence shakeSequence = DOTween.Sequence();

        shakeSequence
            .Append(_camera.transform.DOShakePosition(0.2f, 1f, 3, 20f))
            .AppendInterval(0.1f)
            .Append(_camera.transform.DOShakePosition(0.3f, 0.1f, 3, 10f))
            .SetEase(Ease.OutQuad);
    }
}
