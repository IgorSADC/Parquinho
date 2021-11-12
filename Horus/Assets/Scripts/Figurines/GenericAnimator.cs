using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class GenericAnimator : MonoBehaviour
{
    private void Awake() {
        SubscribeToAnimatables();

    }
    const float placeholder = -75981f;
    public Dictionary<IAnimatable<float>, List<Tuple<float, float>>> AnimatableStatesFloat;
    public Dictionary<IAnimatable<float>, List<Tween>> AnimatablesTweensFloat;

    public Dictionary<IAnimatable<int>, List<Tuple<int, int>>> AnimatableStatesInt;
    public Dictionary<IAnimatable<int>, List<Tween>> AnimatablesTweensInt;
    [Button]
    public void SubscribeToAnimatables(){
        InitiateDict();
        var objects = FindObjectsOfType<MonoBehaviour>();
        var animatables = objects.OfType<IAnimatable<float>>();
        foreach (var a in animatables)
        {
            a.SaveState += SaveAnimatableState;
        }

        var animatablesInt = objects.OfType<IAnimatable<int>>();
        foreach (var a in animatablesInt)
        {
            a.SaveState += SaveAnimatableState;
        }

    }

    [Button]
    public void InitiateDict(){
        AnimatableStatesFloat = new Dictionary<IAnimatable<float>, List<Tuple<float, float>>>();
        AnimatablesTweensFloat = new Dictionary<IAnimatable<float>, List<Tween>>();

        AnimatableStatesInt = new Dictionary<IAnimatable<int>, List<Tuple<int, int>>>();
        AnimatablesTweensInt = new Dictionary<IAnimatable<int>, List<Tween>>();
    }

    private void SaveAnimatableState(IAnimatable<float> obj, bool rebuild, int index)
    {
        if(obj.GetterSetterPairs == null ){
            return;
        }
        
        if(rebuild){
            if(!AnimatableStatesFloat.Keys.Contains(obj)) AnimatableStatesFloat[obj] = new List<Tuple<float, float>>();

        
            if(AnimatableStatesFloat[obj].Capacity < index){ 
                AnimatableStatesFloat[obj].Capacity = index;
            }
            var pair = obj.GetterSetterPairs[index];
            var getter = pair.Item1;
            Debug.Log(index);
            AnimatableStatesFloat[obj].Insert(index, new Tuple<float, float>(getter(), placeholder));
                
 
            return; 
        }
        else{
            if(!AnimatablesTweensFloat.Keys.Contains(obj)) AnimatablesTweensFloat[obj] = new List<Tween>();
            if(AnimatablesTweensFloat[obj].Capacity < index){ 
                AnimatablesTweensFloat[obj].Capacity = index;
            }

            var i = index;
            var c_tuple = AnimatableStatesFloat[obj][i];
            var c_getter = obj.GetterSetterPairs[i].Item1;
            var new_value = c_getter();
            AnimatableStatesFloat[obj][i] = new Tuple<float, float>(c_tuple.Item1, new_value);
            var c_func = obj.GetterSetterPairs[i].Item2;
            c_func (c_tuple.Item1);
            var tw = DOTween.To(() =>c_getter(), (x)=> c_func(x), new_value, obj.duration);

            tw.SetEase(obj.AnimationEase);
            // if(AnimatablesTweensFloat[obj][index] != null){
            //     var temp_tween = AnimatablesTweensFloat[obj][index];
            //     temp_tween.Pause();
            // }
            AnimatablesTweensFloat[obj].Insert(index,tw);
            obj.tween = AnimatablesTweensFloat[obj];

        
        }
        

    }

    private void SaveAnimatableState(IAnimatable<int> obj, bool rebuild, int index)
    {
        if(obj.GetterSetterPairs == null ){
            return;
        }
        
        if(rebuild){
            if(!AnimatableStatesInt.Keys.Contains(obj)) AnimatableStatesInt[obj] = new List<Tuple<int, int>>(obj.GetterSetterPairs.Count());

            if(AnimatableStatesInt[obj].Capacity < index){ 
                AnimatableStatesInt[obj].Capacity = index;
            }
    
            var pair = obj.GetterSetterPairs[index];
            var getter = pair.Item1;
            AnimatableStatesInt[obj].Insert(index ,new Tuple<int, int>(getter(), -1));
                
            
            return; 
        }
        else{
            //AnimatablesTweensInt[obj] = new List<Tween>();
            if(!AnimatablesTweensInt.Keys.Contains(obj)) AnimatablesTweensInt[obj] = new List<Tween>(AnimatableStatesInt[obj].Count());

            if(AnimatablesTweensInt[obj].Capacity < index){ 
                AnimatablesTweensInt[obj].Capacity = index;
            }

            var i = index;
            var c_tuple = AnimatableStatesInt[obj][i];
            var c_getter = obj.GetterSetterPairs[i].Item1;
            var new_value = c_getter();
            AnimatableStatesInt[obj][i] = new Tuple<int, int>(c_tuple.Item1, new_value);
            var c_func = obj.GetterSetterPairs[i].Item2;
            c_func (c_tuple.Item1);
            var tw = DOTween.To(() =>c_getter(), (x)=> c_func(x), new_value, obj.duration);
            tw.SetEase(obj.AnimationEase);
            AnimatablesTweensInt[obj].Insert(index,tw);
            obj.tween = AnimatablesTweensInt[obj];

            

        }
        

    }
}
