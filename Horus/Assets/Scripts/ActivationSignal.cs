using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationSignal : MonoBehaviour
{
    [SerializeField] LevelState StateOfEvent;

    public int Part = -1;

    public bool ActivateState;
    // Start is called before the first frame update
    void Awake()
    {
        
        GameManager.Instance.levelManager.RegisterToEvent(StateOfEvent, Part, () => this.gameObject.SetActive(ActivateState));
        //this.gameObject.SetActive(false);
    }
}
