using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendListInstruction : MonoBehaviour, IPythonProcessing
{

    public string varName;
    public List<float> values;

    private UIPythonBinder uIPythonBinder;

    public Button button;

    public ProcessingDelegate ProcessingFunction => ProcessingFunctionImpl;

    public ProcessingType Type => ProcessingType.POST;

    private int c_index = 0;
    public float TimeToWait = .5f;

    private IEnumerator WaitRoutine;

    // Start is called before the first frame update
    void Start()
    {
        uIPythonBinder = FindObjectOfType<UIPythonBinder>();

        TryGetComponent<Button>(out button);
        if(button != null){
            button.onClick.AddListener(SendLInstruction);
        }
    }

    public string ProcessingFunctionImpl(string v){
        var c_value = values[c_index];
        return $"x={c_value}\n"+ v + "\n" + $"x={c_value}";
    }

    public void SendLInstruction(){
        if(WaitRoutine != null){
            StopCoroutine(WaitRoutine);
            c_index =0;
        }
        WaitRoutine = SendInstructions();
        StartCoroutine(WaitRoutine);

    }

    private IEnumerator SendInstructions(){
        for (int i = 0; i < values.Count; i++)
        {
            uIPythonBinder.SendInstruction();
            yield return new WaitForSeconds(TimeToWait);
            c_index++;
        }
        c_index=0;

    }



}
