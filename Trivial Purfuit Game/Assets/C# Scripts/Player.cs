using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public string PlayerName;     //default PlayerX (X is the player ID)
    private int _playerID;          //Player3 has playerID=3    
    private int _diceNum;           //this decides the turn order after rolling the initial dice

    public bool hasRedCake;
    public bool hasBlueCake;
    public bool hasGreenCake;
    public bool hasWhiteCake;
        

    public Player(string playerName,int playerID,int diceNum )
    {
        _playerID = playerID;
        PlayerName = playerName;
        _diceNum = diceNum;
    }

    // Start is called before the first frame update
    void Start()
    {
        RuleController rc = FindObjectOfType<RuleController>() ?? new RuleController();
        if(this.gameObject.name == "Player1")
        {
            PlayerName = rc.Player1Name ?? "Player1";
        }
        else if (this.gameObject.name == "Player2")
        {
            PlayerName = rc.Player2Name ?? "Player2";
        }
        else if (this.gameObject.name == "Player3")
        {
            PlayerName = rc.Player3Name ?? "Player3";
        }
        else if (this.gameObject.name == "Player4")
        {
            PlayerName = rc.Player4Name ?? "Player4";
        }
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
        return PlayerName;
    }


    //When the rule controller determines that cake should be awarded to a player,
    //This method can be called to update the state of the player's cake
    public void UpdateCakeState(string cakeColor)
    {
        if (cakeColor.ToUpper() == "RED")
        {
            hasRedCake = true;
        }
        if (cakeColor.ToUpper() == "BLUE")
        {
            hasBlueCake = true;
        }
        if (cakeColor.ToUpper() == "GREEN")
        {
            hasGreenCake = true;
        }
        if (cakeColor.ToUpper() == "WHITE")
        {
            hasWhiteCake = true;
        }

        
        var activePlayerToken = PlayerToken.FindActivePlayerToken();
        activePlayerToken.SetSpriteBasedOnCakeStatus(hasRedCake, hasBlueCake, hasGreenCake, hasWhiteCake);

    }
}
