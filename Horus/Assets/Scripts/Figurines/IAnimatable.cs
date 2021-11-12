using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public interface IAnimatable<T>
{

    List<Tuple<Func<T>, Action<T>>> GetterSetterPairs {get;}

    public event Action<IAnimatable<T>, bool, int> SaveState;
    public List<Tween> tween{get; set;}
    public float duration{get;}
    public Ease AnimationEase { get; }

}
