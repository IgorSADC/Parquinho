using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate float TFunction(float x, int t);
public class BindManager : MonoBehaviour
{
    private static BindManager _instance;
    public static BindManager GetInstance() {
        if (_instance == null) {
            _instance = new BindManager();
        }
        return _instance;
    }
    private BindManager()
    {
    }

    



}
