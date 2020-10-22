using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimasyonScale : Animasyonbase
{
    public Vector3 scale;
    public bool Otomoaticscale = false;
    public bool IsBusy;
  

    private void Awake()
    {
       
    }
    public override void PlayAnim()
    {
        if (IsBusy) return;
        base.PlayAnim();

        
        OnStartAnim?.Invoke();
        IsBusy = true;

        transform.DOScale(transform.localScale * 2,.2f).OnComplete(() =>
        {
            OnFinishAnim?.Invoke();
            if (Otomoaticscale)
            {
                transform.DOScale(transform.localScale / 2, .2f).OnComplete(()=> { IsBusy = false; });
            }
            else
            {
                IsBusy = false;
            }
           
        });
       
        
      



    }
}
