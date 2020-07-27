using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerCorrectnessDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator ShowMessage(bool correct, float delay)
    {
        var guiTextObject = this.gameObject.GetComponent<Text>();
        if (correct)
        {
            guiTextObject.text = "CORRECT";                       
            var newColor = new Color32(11, 137, 11, 255);
            guiTextObject.color = newColor;
            guiTextObject.enabled = true;            
            yield return new WaitForSeconds(delay);
            guiTextObject.enabled = false;
        }
        else
        {
            guiTextObject.text = "INCORRECT";
            var newColor = new Color32(205, 42, 44, 255);
            guiTextObject.color = newColor;
            guiTextObject.enabled = true;
            yield return new WaitForSeconds(delay);
            guiTextObject.enabled = false;
        }
    }
}
