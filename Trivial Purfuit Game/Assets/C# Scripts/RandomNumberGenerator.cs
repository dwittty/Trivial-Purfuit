using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNumberGenerator : MonoBehaviour
{
    private int _rand;
    private int _diceMin = 1;
    private int _diceMax = 6;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int rollDice()
    {
        System.Random random = new System.Random();
        _rand =random.Next(_diceMin,_diceMax);
        return _rand;
    }
}
