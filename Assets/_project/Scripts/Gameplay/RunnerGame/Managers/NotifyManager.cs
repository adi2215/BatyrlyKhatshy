using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class NotifyManager : MonoBehaviour
{
    [SerializeField] private GameObject _icon;
    [SerializeField] private Vector3 _toScale;
    [SerializeField] private float _timeDelay;
    [SerializeField] private float _duration;
    [SerializeField] private int _loops;

    public void Notify(Vector3 position)
    {
        var icon = Instantiate(_icon);

        icon.transform.position = ClampedVector(position);

        icon.transform.DOScale(_toScale, _duration)
            .SetEase(Ease.InOutSine)
            .SetLoops(_loops, LoopType.Yoyo)
            .OnComplete(() => Destroy(icon));
    }
    private Vector3 ClampedVector(Vector3 position) => 
        new Vector3(
            Mathf.Clamp(position.x, -5, 5),
            Mathf.Clamp(position.y, 1, 1),
            Mathf.Clamp(position.z, 0, 10)
            );

}
