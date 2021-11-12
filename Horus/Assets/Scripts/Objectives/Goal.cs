using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public List<Todo> TodosForGoal;
    public int currentGoal = 0;

    public event Action<Goal> OnFinishGoal;
    public event Action OnCrossingTodo;
    public int order;

    public bool WaitForAll = false;

    // Start is called before the first frame update
    void Start()
    {
        var todos = GetComponentsInChildren<Todo>(true);
        TodosForGoal = new List<Todo>(todos);

        if(!WaitForAll){
            foreach (var item in TodosForGoal)
            {
                item.isActive = false;
                item.OnCompleteGoal += AdvanceTodo;
            }

            TodosForGoal[currentGoal].isActive = true;

        }
        else{
            foreach (var item in TodosForGoal)
            {
                item.isActive = true;
            }
        }
        
    }

    private void AdvanceTodo()
    {
        //TodosForGoal[currentGoal].isActive = false;
        currentGoal++;
        if(currentGoal >= TodosForGoal.Count){
            gameObject.SetActive(false);
            OnFinishGoal?.Invoke(this);
            return;
        }else{
            TodosForGoal[currentGoal].isActive = true;
            OnCrossingTodo?.Invoke();
        }
    }

    private void Update() {
        if(!WaitForAll) return;

        
        foreach (var item in TodosForGoal)
        {
            //check if all finished
            if(item.isActive) {        
                return;
            }
        }
        gameObject.SetActive(false);
        OnFinishGoal?.Invoke(this);
        
    }
    

}
