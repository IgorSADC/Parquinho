using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

public class STimeline : MonoBehaviour
{
    // public List<Sequence> Animations;

    public int Frames;
    public float Duration;

    public Dictionary<SObjects, Sequence> Animations;

    [Button]
    public void StartAnimation(){
        Animations = new Dictionary<SObjects, Sequence>();
        var SObjects = FindObjectsOfType<SObjects>();

        for (int i = 0; i < SObjects.Length; i++)
        {
            //Sequence do objeto
            var sq = GenerateTweens(SObjects[i]);

            Animations.Add(SObjects[i], sq);
            sq.SetLoops(-1);
            sq.Play();

        }

    }

    private Sequence GenerateTweens(SObjects SObject)
    {
        Sequence sq = DOTween.Sequence();
        for (int j = 0; j < Frames; j++)
        {
            var t = SObject.Tick(Duration, j);
            sq.Append(t);

        }
        return sq;
    }

    public void PauseAnimation(){
        foreach (var s in Animations)
        {
            s.Value.Pause();
        }
    }

    [Button]
    public void UpdateAnimation(){
        var newAnimations = new Dictionary<SObjects, Sequence>();
        foreach (var s in Animations)
        {
            s.Value.Pause();
            var goToPoint = s.Value.Elapsed(false);


            var sq = GenerateTweens(s.Key);
            sq.Goto(goToPoint);
            newAnimations[s.Key] = sq;
            sq.SetLoops(-1);
            sq.Play();

        }

        Animations = newAnimations;
            
        
    }


}
