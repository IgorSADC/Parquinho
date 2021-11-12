using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using NaughtyAttributes;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private void Awake() {
        if(_instance !=null){
            Destroy(this);
            return;
        }
        _instance = this;
        var signals = FindObjectsOfType<ActivationSignal>();
        foreach (var item in signals)
        {
            item.gameObject.SetActive(true);

        }

        var goals = FindObjectsOfType<Goal>();
        LevelGoals = new List<Goal>(goals).OrderBy(g => g.order).ToList();
        

        levelManager = this.gameObject.AddComponent<LevelManager>();
        levelManager.RegisterToEvent(LevelState.PLAYING, SubscribeToLevel);

        this.gameObject.AddComponent<GenericAnimator>();
        dialogueManager = this.gameObject.GetComponent<DialogueManager>();

    }

    static GameManager _instance;


    public static GameManager Instance {get => _instance;}


    public LevelManager levelManager;

    public List<Goal> LevelGoals;
    public DialogueManager dialogueManager;
    [SerializeField] private int currentGoal =0;


    private void SubscribeToLevel()
    {
        if(currentGoal >= LevelGoals.Count){
            return;
        }
        
        var ActiveGoal = LevelGoals[currentGoal];
        ActiveGoal.OnFinishGoal += AdvanceLevel;
    }


    private Goal finishedGoal;
    private void AdvanceLevel(Goal lastGoal)
    {
        if(lastGoal == finishedGoal) return;

        finishedGoal = lastGoal;
        var ActiveGoal = LevelGoals[currentGoal];
        Debug.Log(lastGoal);
        ActiveGoal.OnFinishGoal -= AdvanceLevel;

        levelManager.UpdateState();

        currentGoal++;

        if(currentGoal >= LevelGoals.Count){
            LoadScene(0);
            return;
        }
        ActiveGoal = LevelGoals[currentGoal];
        ActiveGoal.OnFinishGoal += AdvanceLevel;
    }

    [Button]
    public void AdvanceState(){
        dialogueManager.textDisplay.TargetText = "";
        dialogueManager.textDisplay.PrintText();
        if(levelManager.currentState ==LevelState.PLAYING){
            currentGoal++;
        } 
        levelManager.UpdateState();

        dialogueManager.isActive = levelManager.currentState == LevelState.DIALOGUE;




    }

    static public void LoadScene(int id){
        SceneManager.LoadScene(id);
    }

    private void Update() {
        if(Keyboard.current.escapeKey.wasPressedThisFrame){
            if(SceneManager.GetActiveScene().buildIndex ==0){
                Application.Quit();
            }
            else{
                LoadScene(0);
            }
        }
    }
}
