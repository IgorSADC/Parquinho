using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(IBindable<float>))]
public class DisplayPyVariable : MonoBehaviour
{
    private IBindable<float> BindableData;

    public Vector2 Offset;

    public TextMesh text;
    private GameObject display;

    public Font font;
    public int fontsize;
    public Color color;
    public float lineSpacing = 0.7f;
    public float characterSize = .28f;
    // Start is called before the first frame update
    void Start()
    {
        BindableData = this.GetComponent<IBindable<float>>();

        display = new GameObject("PyVariableDisplay");
        display.transform.parent = this.transform;


        text = display.AddComponent<TextMesh>();
//        text.font = font;
        UpdateText();

        display.transform.localPosition = Offset;
        display.SetActive(false);


    }

    private void UpdateText()
    {
        display.transform.localPosition = Offset;
        text.fontSize = fontsize;
        text.color = color;
        text.lineSpacing = lineSpacing;
        text.characterSize = characterSize;
        
    }

    void OnMouseOver(){
        display.SetActive(true);
        UpdateText();
        string textToDisplay = "";

        foreach (var item in BindableData.BindData)
        {
            textToDisplay += item.Key;
            textToDisplay += " = ";
            textToDisplay += item.Value.Item1();
            textToDisplay += "\n";
        }
        text.text = textToDisplay;

    }

    void OnMouseExit() {
        display.SetActive(false);
    }
}
