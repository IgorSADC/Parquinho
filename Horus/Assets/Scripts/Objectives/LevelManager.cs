using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;


public enum LevelState
{
    PLAYING,
    DIALOGUE
    
}
public class LevelManager : MonoBehaviour
{
    public Dictionary<Tuple<LevelState, int>, List<System.Action>> LevelStateEvents;
    [ReadOnly][SerializeField] public LevelState currentState = LevelState.PLAYING;
    [ReadOnly][SerializeField] private int currentStatePart = -1;
    // Start is called before the first frame update
    void Awake()
    {
        LevelStateEvents = new Dictionary<Tuple<LevelState, int>, List<Action>>();

    }

    private void Start() {
        UpdateState();
    }


    public void RegisterToEvent(LevelState ls,int i, Action func){
        var t =new Tuple<LevelState, int>(ls, i);
        if(!LevelStateEvents.ContainsKey(t) || LevelStateEvents[t] == null){
            LevelStateEvents[t] = new List<Action>();
        }

        LevelStateEvents[t].Add(func);
    }

    public void RegisterToEvent(LevelState ls, Action func){
        //Event occurs every time the state happens. EX: Disable input on dialogue.
        var t =new Tuple<LevelState, int>(ls, -1);
        if(!LevelStateEvents.ContainsKey(t)|| LevelStateEvents[t] == null){
            LevelStateEvents[t] = new List<Action>();
        }

        LevelStateEvents[t].Add(func);
    }


    public void UpdateState(){

        if(currentState == LevelState.DIALOGUE){
            currentState = LevelState.PLAYING;
           
        }else{
            currentState = LevelState.DIALOGUE;
            currentStatePart++;
        }

        UpdateKey();
        


    }


    private void UpdateKey(){
        var t =new Tuple<LevelState, int>(currentState, currentStatePart);

        if(LevelStateEvents.ContainsKey(t)){
            var currentList = LevelStateEvents[t];
            if(currentList == null) return;

            foreach (var item in currentList)
            {
                item();
            }

        }

        t = new Tuple<LevelState, int>(currentState, -1);
        if(LevelStateEvents.ContainsKey(t)){
            var currentList = LevelStateEvents[t];
            if(currentList == null) return;

            foreach (var item in currentList)
            {
                item();
            }

        }
    }
}
