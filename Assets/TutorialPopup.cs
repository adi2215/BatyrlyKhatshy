using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TutorialPopup : MonoBehaviour
{
    private void Start()
    {
        transform.localScale = new Vector3(0, 0, 0);
    }
    public void HidePopup()
    {
        Time.timeScale = 1f;
        transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack);
    }}
