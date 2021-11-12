using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


[System.Serializable]
    public struct BVariable{
        public float value;
        public float initialValue;
        public Ease STransition;
        public TFunction TFunc;
        public SObjects Owner;

    }
