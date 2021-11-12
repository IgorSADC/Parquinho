using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class PyPoint : MonoBehaviour, IBindable<float>, IAnimatable<float>
{
    Dictionary<string, Tuple<Func<float> ,Action<float> >> _bindData;
    public Dictionary<string, Tuple<Func<float> ,Action<float> >> BindData => _bindData;

    private List<Tuple<Func<float>, Action<float>>> _getterSetterPairs;
    public List<Tuple<Func<float>, Action<float>>> GetterSetterPairs => _getterSetterPairs;

    public List<Tween> tween { get; set; }


    [SerializeField] private float _duration = .5f;
    public float duration => _duration;

    [SerializeField]
    private Ease transition = Ease.Linear;
    public Ease AnimationEase => transition;

    public event Action<IAnimatable<float>, bool, int> SaveState;


    private void UpdateXTransform(float x){

        var temp = transform.localPosition; 
        temp.x = x;
        transform.localPosition = temp;
    }

    private void UpdateYTransform(float y){

        var temp = transform.localPosition; 
        temp.y = y;
        transform.localPosition = temp;

    }

    private void UpdateAnimatedXTransform(float x){
        if(tween !=null && tween.Count > 1) tween[1].WaitForCompletion();

        SaveState?.Invoke(this, true,  1);
        UpdateXTransform(x);
        SaveState?.Invoke(this, false, 1);
    }

    private void UpdateAnimatedYTransform(float y){
        if(tween !=null && tween.Count > 0) tween[0].WaitForCompletion();

        SaveState?.Invoke(this, true,  0);
        UpdateYTransform(y);
        SaveState?.Invoke(this, false, 0);
    }
    public void InitData()
    {

        _bindData = new Dictionary<string, Tuple<Func<float>,Action<float> >>();

        _bindData.Add(
            "y", 
            new Tuple<Func<float> ,Action<float> >(
            () => transform.localPosition.y,
            UpdateAnimatedYTransform
            )
        );
        _bindData.Add(
            "x", 
            new Tuple<Func<float> ,Action<float> >(
            () => transform.localPosition.x,
            UpdateAnimatedXTransform
            )
        );        
            
        _getterSetterPairs = new List<Tuple<Func<float>, Action<float>>>();
        _getterSetterPairs.Add(
            new Tuple<Func<float>, Action<float>>(
            () => transform.localPosition.y,
            UpdateYTransform
            )
        );

        _getterSetterPairs.Add(
            new Tuple<Func<float>, Action<float>>(
            () => transform.localPosition.x,
            UpdateXTransform
            )
        );

        
    }


}
