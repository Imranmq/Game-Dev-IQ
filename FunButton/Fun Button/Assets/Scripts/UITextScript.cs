using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UITextScript : MonoBehaviour
{
    Text uiText;
    private void Awake()
    {
        uiText = gameObject.GetComponent<Text>();
    }
    public void setTextValue(string txt)
    {
        uiText.text = txt;
    }
    public string getTextValue()
    {
        return uiText.text;
    }
}
