﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCakeSquare : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetUpdate()
    {

        RuleController rc = new RuleController();
        
        rc.sendUpdatedCakeSquare();
        


    }
}