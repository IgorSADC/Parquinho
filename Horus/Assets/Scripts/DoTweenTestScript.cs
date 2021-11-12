using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class DoTweenTestScript : MonoBehaviour
{

    public Sequence sequence;

    public Vector3 vectorToBlendTo;
    public Vector3 nextVectorToBelndTo;
    public int timeToBlend;


    public Tween cTween;

    [ReadOnly]
    [SerializeField]
    private Vector3 initialValue;

    [SerializeField]
    Ease AnimEase;

    [SerializeField]
    LoopType loop;


    public float BVariable {get; set;}

    private void Awake() {
        sequence = DOTween.Sequence();
        BVariable = 0;


        
    }
    // Start is called before the first frame update
    void Start()
    {
        initialValue = transform.localScale;
        
        cTween = transform.DOScale(vectorToBlendTo, timeToBlend).SetEase(AnimEase);
        sequence.Append(cTween);

        cTween = transform.DOScale(nextVectorToBelndTo, timeToBlend).SetEase(AnimEase);
        sequence.Append(cTween);

        sequence.SetLoops(-1, loop);


        DOTween.To(() => BVariable, 
                (v) => BVariable = v, 
                5, 10).Play();
    }

    [Button("UpdateAnimation")]
    void UpdateAnimation(){
        StopAnimation();

        // var gotoPoint = transform.localScale;
        transform.localScale = initialValue;
        var gotoPoint = sequence.Elapsed(false);
        cTween = transform.DOScale(vectorToBlendTo, timeToBlend).SetEase(AnimEase);
        sequence = DOTween.Sequence();
        sequence.Append(cTween);
        cTween = transform.DOScale(nextVectorToBelndTo, timeToBlend).SetEase(AnimEase);
        sequence.Append(cTween);

        sequence.SetLoops(-1, loop);
        sequence.Goto(gotoPoint);
        StartAnimation();



    }

    [Button("StartAnimation")]
    void StartAnimation(){
        sequence.Play();

    }

    [Button("StopAnimation")]
    void StopAnimation(){
        sequence.Pause();
    }

    private void Update() {
        transform.position = new Vector3(BVariable, 0, 0);
    }
}
