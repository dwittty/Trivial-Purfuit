using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AnswerSelection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnswerSelected()
    {
        //hide the question and answers so the user can't answer twice
        this.gameObject.transform.parent.gameObject.SetActive(false);
        //check answer (also displays correct/incorrect, awards cake etc
        RuleController.Instance.CheckAnswer(this.GetComponentInChildren<Text>().text);
    }
}
