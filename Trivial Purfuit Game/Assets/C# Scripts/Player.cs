using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private int _playerID;          //Player3 has playerID=3
    private string _playerName;     //default PlayerX (X is the player ID)
    private int _diceNum;           //this decides the turn order after rolling the initial dice

    public bool hasRedCake;
    public bool hasBlueCake;
    public bool hasGreenCake;
    public bool hasWhiteCake;


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
        activePlayerToken.UpdateSprite(hasRedCake, hasBlueCake, hasGreenCake, hasWhiteCake);

    }

    internal static Player FindActivePlayer()
    {
        int currentTurn = RuleController.CurrentTurn();
        var players = FindObjectsOfType<Player>().ToList();
        var activePlayer = players.FirstOrDefault(x => x.name.Contains(currentTurn.ToString()));
        return activePlayer;
    }


}
