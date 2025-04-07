using System;
using UnityEngine.InputSystem;
using Zenject;

public class BoneInput : IInitializable
{
    [Inject] private GameInputAction _input;
    private InputAction _boneAction;
    public Action OnBoneAction;

    public void Initialize()
    {
        _boneAction = _input.PlayerInput.BoneAction;

        _boneAction.performed += BoneActionPerformed;
    }

    private void BoneActionPerformed(InputAction.CallbackContext context)
    {
        OnBoneAction?.Invoke();
    }
}