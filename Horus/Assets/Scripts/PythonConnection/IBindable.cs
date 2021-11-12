using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IBindable<T>
{
    public Dictionary<string, Tuple<Func<T>,Action<T>>> BindData {get;}
    public void InitData();
    
}
