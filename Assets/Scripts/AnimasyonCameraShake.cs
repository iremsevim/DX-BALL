using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimasyonCameraShake : Animasyonbase
{
    public float shakeduration;
    public override void PlayAnim()
    {
        base.PlayAnim();
        OnStartAnim?.Invoke();
        Camera.main.DOShakePosition(shakeduration).OnComplete(()=> { OnFinishAnim?.Invoke();});
    }
}
