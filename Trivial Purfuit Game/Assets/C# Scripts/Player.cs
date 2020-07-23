using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private int _playerID;          //Player3 has playerID=3
    private string _playerName;     //default PlayerX (X is the player ID)
    private int _diceNum;           //this decides the turn order after rolling the initial dice    

    public Player(string playerName,int playerID,int diceNum )
    {
        _playerID = playerID;
        _playerName = playerName;
        _diceNum = diceNum;


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getDiceNum()
    {
        return _diceNum;
    }

    public int getPlayerID()
    {
        return _playerID;
    }


    public string getPlayerName()
    {
        return _playerName;
    }

}
