using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using RollDice;

public class RandomNumberGeneratorDice
{
    private int _rand;
    private int _diceMin = 1;
    private int _diceMax = 7;

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
        System.Random random = new System.Random(Guid.NewGuid().GetHashCode());
        //_rand =random.Next(_diceMin,_diceMax);
        _rand = 4;
        return _rand;
    }
}
