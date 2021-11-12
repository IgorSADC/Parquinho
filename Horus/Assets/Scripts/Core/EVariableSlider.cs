using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class EVariableSlider : EVariable<float>
{
    Slider slider;


    private void Start() {
        slider = this.GetComponent<Slider>();
        slider.onValueChanged.AddListener(UpdateEV);
    }

    public void UpdateEV(float v){
        Value = v;

    }

}
