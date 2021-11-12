using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IronPython.Hosting;
using IronPython.Runtime;
using System.IO;

public class ScriptReader : MonoBehaviour
{

    [SerializeField]
    string fileName = "test.py";
    
    [SerializeField]
    Transform LinkedTransform;
    // Start is called before the first frame update
    void Start()
    {
        Callpython();

    }

    private void Callpython()
    {
        var engine = Python.CreateEngine();
        var scope = engine.CreateScope();

        var source = engine.CreateScriptSourceFromFile($"{Application.dataPath}\\PythonScripts\\{fileName}");
        source.Execute(scope);

        LinkedTransform.position = new Vector3(scope.GetVariable<int>("x"), scope.GetVariable<int>("y"), scope.GetVariable<int>("z"));


        Debug.Log(scope.GetVariable<int>("x"));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Backspace))
            Callpython();
    }
}
