using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;
using DG.Tweening;

[RequireComponent(typeof(TextMeshProUGUI))]
public class AnimatedText : MonoBehaviour, IAnimatable<int>
{
    private void Awake() {
        RegisterFunctions();
        text = this.GetComponent<TextMeshProUGUI>();

    }

    private TextMeshProUGUI text;
    private List<Tuple<Func<int>, Action<int>>> _getterSetterPairs;
    public List<Tuple<Func<int>, Action<int>>> GetterSetterPairs => _getterSetterPairs;

    public event Action<IAnimatable<int>, bool, int> SaveState;


    [SerializeField]
    private string _targetText;
    public string TargetText{get => _targetText; 
    set {
        currentChar = 0;
        _targetText = value;
        tween?[0].Pause();
    }}
    private int _currentChar;
    public int currentChar{get =>_currentChar;
    set
    {
        if (currentChar > TargetText.Length){
            _currentChar = 0;
            // return;
        }
        text.text = TargetText.Substring(0, value);
        _currentChar = value;

    }}

    public List<Tween> tween { get; set; }

    public float duration => 1f;

    public Ease AnimationEase => Ease.InOutSine;

    [Button]
    public void PrintText(){
        if(TargetText.Length == 0) return;
        currentChar = 0;
        SaveState?.Invoke(this, true,0);
        currentChar = TargetText.Length;
        SaveState?.Invoke(this, false,0);

    }

    public void RegisterFunctions(){
        _getterSetterPairs = new List<Tuple<Func<int>, Action<int>>>();

        _getterSetterPairs.Add(
            new Tuple<Func<int>, Action<int>>(
                () => currentChar,
                (c) => currentChar = c

            )
        );
    }

    
}
