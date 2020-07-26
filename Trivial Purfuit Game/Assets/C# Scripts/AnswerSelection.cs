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
        RuleController rc = FindObjectOfType<RuleController>() ?? new RuleController();
        rc.CheckAnswer(this.GetComponentInChildren<Text>().text);
    }
}
