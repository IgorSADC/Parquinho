using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Todo : MonoBehaviour
{

    public event Action OnCompleteGoal;
    
    [Range(0f, 3f)]
    public float timeToSpan =0f;

    public float reactivateOn = -1f;

    private float timeElapsed = 0f;
    
    public virtual bool CheckForCompletion(){
        return true;
    }
    public bool isActive;

    

    protected virtual void Update() {
        timeElapsed += Time.deltaTime;
        if(isActive){
            if(CheckForCompletion()){
                if(timeElapsed >= timeToSpan){
                    timeElapsed = 0f;
                    FinishStep();
                }
            }
            else{
                timeElapsed = 0f;
            }

        }else{
            if(reactivateOn <= 0) return;
            if(timeElapsed >= reactivateOn){
                Reactivate();
            }
        }


        
    }
    
    protected virtual void FinishStep(){
        //this.gameObject.SetActive(false);
        this.Disable();
        isActive = false;
        OnCompleteGoal?.Invoke();
    }
    protected virtual void Reactivate(){
        timeElapsed = 0;
        this.isActive = true;
    }
    protected virtual void Disable(){
        this.gameObject.SetActive(false);
    }
}
