using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomTMPEditor : TMP_InputField
{
    protected EditState KeyPressed(Event evt){
        if(evt.keyCode == KeyCode.KeypadEnter && m_Text[-1] == ':'){
            text += "\n    ";
            return EditState.Finish;
        }

        else if (evt.keyCode == KeyCode.Tab){
            text += "    ";
            return EditState.Finish;

        }

        return base.KeyPressed(evt);

    }
    
}
