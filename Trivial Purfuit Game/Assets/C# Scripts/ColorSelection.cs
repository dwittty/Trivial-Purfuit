﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSelection : MonoBehaviour
{
    public string color;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ColorSelected()
    {
        var rc = FindObjectOfType<RuleController>() ?? new RuleController();
        //color selected, hide this interface
        this.gameObject.transform.parent.gameObject.SetActive(false);
        //serve up a question with the selected color
        rc.GetAndDisplayNewTriviaQuestion(color);
    }
}
