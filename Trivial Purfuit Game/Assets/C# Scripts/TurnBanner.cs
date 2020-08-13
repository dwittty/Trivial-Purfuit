using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class TurnBanner : MonoBehaviour
{
    public RuleController rc;
    // Start is called before the first frame update
    void Start()
    {
        var guiTextObject = this.gameObject.GetComponent<Text>();
        rc = FindObjectOfType<RuleController>() ?? new RuleController();
        var currentPlayer = rc.GetCurrentTurn();
        var currenPlayerName = GetCurrentPlayerName(rc, currentPlayer);
        var color = GetCurrentPlayerColor(currentPlayer);
        guiTextObject.text = $"<color={color}>{currenPlayerName}'s Turn</color>";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateTurnBanner(int player)
    {
        var currenPlayerName = GetCurrentPlayerName(rc, player);
        var guiTextObject = this.gameObject.GetComponent<Text>();
        var color = GetCurrentPlayerColor(player);
        guiTextObject.text = $"<color={color}>{currenPlayerName}'s Turn</color>";
    }
    
    //returns the hex for the current player's color
    public string GetCurrentPlayerColor(int player)
    {
        if(player == 1)
        {
            return "#880015";
        }
        if (player == 2)
        {
            return "#ffffff";
        }
        if (player == 3)
        {
            return "#2d36a8";
        }
        else
        {
            return "#177d36";
        }
    }
    public string GetCurrentPlayerName(RuleController rc, int currentPlayerTurn)
    {
        if (currentPlayerTurn == 1)
        {
            return string.IsNullOrWhiteSpace(rc.Player1Name) ? "Player1" : rc.Player1Name;
        }
        else if (currentPlayerTurn == 2)
        {
            return string.IsNullOrWhiteSpace(rc.Player2Name) ? "Player2" : rc.Player2Name;
        }
        else if (currentPlayerTurn == 3)
        {
            return string.IsNullOrWhiteSpace(rc.Player3Name) ? "Player3" : rc.Player3Name;
        }
        else if (currentPlayerTurn == 4)
        {
            return string.IsNullOrWhiteSpace(rc.Player4Name) ? "Player4" : rc.Player4Name;
        }
        else
        {
            return "Error Getting Player Name";
        }
        
    }

}
