using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;
using System.IO;
using System.Text;
using IronPython.Hosting;

public class TestObject : SObjects
{
    public BVariable scaleX;

    [SerializeField] string fileName = "TestObject.py";

    Microsoft.Scripting.Hosting.ScriptEngine pythonEngine; //extrair isso depois

    Microsoft.Scripting.Hosting.ScriptScope pythonScope;


    string finalName{get => $"{Application.persistentDataPath}\\PythonScripts\\{fileName}";} 


    private void Start() {
        scaleX.TFunc = TickFunction;
        scaleX.value = this.transform.localScale.x;
        scaleX.initialValue = this.transform.localScale.x;
        scaleX.Owner = this;
        pythonEngine = Python.CreateEngine();
        pythonScope = pythonEngine.CreateScope();

        if(System.IO.File.Exists(finalName)) return;

        using ( FileStream fs = File.Create(finalName)){
            byte[] info = new UTF8Encoding(true).GetBytes("def TickFunction(x, t):\n    pass\nx=TickFunction(x, t)");
            fs.Write(info,0, info.Length);
        }

        
        


    }

    public float TickFunction(float x, int t){
        pythonScope.SetVariable("t", t);
        pythonScope.SetVariable("x", x);
        var source = pythonEngine.CreateScriptSourceFromFile(finalName);
        source.Execute(pythonScope);
        x = pythonScope.GetVariable<float>("x");

        return x;
    }

    public override Tween Tick(float duration, int currentFrame)
    {
        if(currentFrame == 0) scaleX.value = scaleX.initialValue;
        var nextValue= scaleX.TFunc(scaleX.value, currentFrame);
        var tween = this.transform.DOScaleX(nextValue, duration).SetEase(scaleX.STransition).Pause();
        scaleX.value = nextValue;

        return tween;
    }
}
