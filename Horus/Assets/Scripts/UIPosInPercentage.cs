using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UIPosInPercentage : MonoBehaviour, IAnimatable<float>
{

    private RectTransform rectTransform;


    [Range(0f,1f)]
    public float w;

    [Range(0f,1f)]
    public float h;


    [OnValueChanged("PositionOn")]
    public Vector2 leftLowerPos;
    [OnValueChanged("PositionOn")]
    public Vector2 rightUpperPos;

    public bool updateAnchors = true;

    [SerializeField] private bool animate;

    public event System.Action<IAnimatable<float>, bool, int> SaveState;

    private List<System.Tuple<System.Func<float>, System.Action<float>>> _getterSetterPairs;
    public List<System.Tuple<System.Func<float>, System.Action<float>>> GetterSetterPairs => _getterSetterPairs;

    public List<Tween> tween { get; set ; }

    [SerializeField] private float _duration = .2f;
    public float duration => _duration;

    public Ease AnimationEase => DG.Tweening.Ease.OutElastic;
    [SerializeField] private float initialDelay= .2f;
    private float target_trans;

    private void Awake() {
        
        if(animate)
        {
            target_trans = rightUpperPos.x;
            rightUpperPos.x = 0f;
            PositionOn();
            InitGSPair();
            Invoke("AnimateThing", initialDelay);

            //AnimateThing();
        }
    }

    private void AnimateThing()
    {   
        SaveState?.Invoke(this, true,0);
        rightUpperPos.x = target_trans;
        SaveState?.Invoke(this, false,0);
    }

    [Button]
    public void UpdateWH(){
        var rectTransform = this.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(.5f, .5f);
        rectTransform.anchorMax = new Vector2(.5f, .5f);
        rectTransform.anchoredPosition = Vector2.zero;

        var newWidth = w * Screen.currentResolution.width;
        var newHeight = h * Screen.currentResolution.height;
        rectTransform.sizeDelta = new Vector2(newWidth, newHeight);

        rectTransform.ForceUpdateRectTransforms();

        if(!updateAnchors) return;

        // rectTransform.anchorMin = new Vector2(w * 0.5f, h * 0.5f);
        // rectTransform.anchorMax = new Vector2(w * 1.5f, h* 1.5f);

    }

    [Button]
    public void UpdateWithAnchors(){
        var rectTransform = this.GetComponent<RectTransform>();
        rectTransform.sizeDelta = Vector2.one;
        rectTransform.anchorMin = new Vector2(w * 0.5f, h * 0.5f);
        rectTransform.anchorMax = new Vector2(w * 1.5f, h* 1.5f);
        rectTransform.anchoredPosition = new Vector2(0f, 0f);

    }

    [Button]
    public void PositionOn(){
        var rectTransform = this.GetComponent<RectTransform>();
        rectTransform.sizeDelta = Vector2.one;

        rectTransform.anchorMin = leftLowerPos;
        rectTransform.anchorMax = rightUpperPos;
        rectTransform.anchoredPosition = Vector2.zero;

    }


    private void InitGSPair(){
        _getterSetterPairs = new List<System.Tuple<System.Func<float>, System.Action<float>>>();
        _getterSetterPairs.Add(
            new System.Tuple<System.Func<float>, System.Action<float>>(() => rightUpperPos.x, x => {rightUpperPos.x = x;PositionOn();}) 

        );


    }
}
