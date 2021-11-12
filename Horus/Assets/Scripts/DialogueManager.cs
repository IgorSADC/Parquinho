using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using NaughtyAttributes;
using System;

[Serializable]
public class DialogueCol{
    public List<TextSO> texts;
}
public class DialogueManager : MonoBehaviour
{
    public List<DialogueCol> texts;
    public AnimatedText textDisplay;
    [ReadOnly][SerializeField] private int bigI = -1;

    [ReadOnly][SerializeField] private int currentIndex;

    [SerializeField][ReadOnly]
    public bool isActive = true;
    private void Awake() {
        GameManager.Instance.levelManager.RegisterToEvent(LevelState.DIALOGUE, UpdateBigI);
    }

    private void UpdateBigI(){
        bigI++;
        currentIndex = 0;
        isActive = true;
        RequestChangeOnText();
    }
    public void RequestChangeOnText(){
        if(currentIndex >= texts[bigI].texts.Count){
            textDisplay.TargetText = "";
            GameManager.Instance.levelManager.UpdateState();
            isActive = false;
        }else{
            textDisplay.TargetText = texts[bigI].texts[currentIndex].Text;
        }
        textDisplay.PrintText();
        currentIndex++;


    }


    private void Update() {

        if(isActive && Keyboard.current.enterKey.wasPressedThisFrame){
            RequestChangeOnText();
        }
    }
}
