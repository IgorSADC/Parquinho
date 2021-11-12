using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventSignal : MonoBehaviour
{
   [SerializeField] LevelState StateOfEvent;

    public int Part = -1;

    public UnityEvent Event;

    // Start is called before the first frame update
    void Awake()
    {
        GameManager.Instance.levelManager.RegisterToEvent(StateOfEvent, Part, () => Event?.Invoke());
        //this.gameObject.SetActive(false);
    }
}
