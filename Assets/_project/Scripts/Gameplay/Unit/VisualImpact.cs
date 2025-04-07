using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class VisualImpact : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Color _damageColor;
    [SerializeField] private float _duratuion;
    private Color _myColor = new(255, 255, 255);

    private bool _isPlayer;
    private void Start()
    {
        var o = GetComponent<PlayerMovement>();

        if(o)
        {
            _isPlayer = true;
            return;
        }
        _isPlayer = false;
    }
    [Inject] private PlayerCamera _camera;
    public void DamageImpact()
    {
        ColorDamage();
        ScaleImpact();
        if(_isPlayer)
        {
            _camera.GentleShake();
        }
    }
    public void ColorDamage()
    {
        _sprite.DOColor(_damageColor, _duratuion/2)
            .OnComplete(() => _sprite.DOColor(Color.white, _duratuion/2));
    }
    public void ScaleImpact()
    {
        _sprite.transform.DOScale(_sprite.transform.localScale * 1.3f, _duratuion)
        .SetLoops(2, LoopType.Yoyo);
    }
}
