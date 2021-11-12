using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IronPython.Hosting;
using TMPro;
using UnityEngine.Events;
using System.Linq;
using Microsoft.Scripting.Hosting;

public class UIPythonBinder : MonoBehaviour
{
    public TextAsset levelTemplate;
    public TMP_InputField InputField;
    public List<IBindable<float>> bindables;

    private ScriptEngine scriptEngine;
    private ScriptScope scriptScope;

    public List<IPythonProcessing> PreProcessing;
    public List<IPythonProcessing> PostProcessing;

    private void Awake() {
        var allObjects = FindObjectsOfType<MonoBehaviour>();

        bindables = allObjects.OfType<IBindable<float>>().ToList();
        foreach (var b in bindables)
        {
            b.InitData();
        }

        var allProcessing = allObjects.OfType<IPythonProcessing>();
        PreProcessing = allProcessing.Where(x => (x.Type == ProcessingType.PRE) ).ToList();
        PostProcessing = allProcessing.Where(x => (x.Type == ProcessingType.POST) ).ToList();


        InitPython();



    }
    private void Start() {
        InjectVariables();
    }

    private void InitPython()
    {
        scriptEngine = Python.CreateEngine();
        scriptScope = scriptEngine.CreateScope();

        
    }

    public void InjectVariables(){
        foreach (var b in bindables)
        {
            foreach (var k in b.BindData.Keys)
            {
                scriptScope.SetVariable(k, b.BindData[k].Item1());
                
            }
        }
    }

    public void GetVariables(){
        var pythonCode = InputField.text;

        foreach (var p in PostProcessing)
        {
            pythonCode = p.ProcessingFunction(pythonCode) ;
        }
        var source = scriptEngine.CreateScriptSourceFromString(pythonCode);
        source.Execute(scriptScope);

        foreach (var b in bindables)
        {
            foreach (var k in b.BindData.Keys)
            {
                var variable = scriptScope.GetVariable<float>(k);

                b.BindData[k].Item2(variable);
                
            }
        }
    }

    public void SendInstruction(){


        InjectVariables();
        GetVariables();

    }



    
}
