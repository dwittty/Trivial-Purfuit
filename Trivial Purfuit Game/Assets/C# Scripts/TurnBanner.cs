using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class TurnBanner : MonoBehaviour
{    
    // Start is called before the first frame update
    void Start()
    {
        var guiTextObject = this.gameObject.GetComponent<Text>();        
        var currentPlayer = RuleController.Instance.GetCurrentTurn();
        var currenPlayerName = GetCurrentPlayerName(currentPlayer);
        var color = GetCurrentPlayerColor(currentPlayer);
        guiTextObject.text = $"<color={color}>{currenPlayerName}'s Turn</color>";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateTurnBanner(int player)
    {
        var currenPlayerName = GetCurrentPlayerName(player);
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
    public string GetCurrentPlayerName(int currentPlayerTurn)
    {
        if (currentPlayerTurn == 1)
        {
            return string.IsNullOrWhiteSpace(RuleController.Instance.Player1Name) ? "Player1" : RuleController.Instance.Player1Name;
        }
        else if (currentPlayerTurn == 2)
        {
            return string.IsNullOrWhiteSpace(RuleController.Instance.Player2Name) ? "Player2" : RuleController.Instance.Player2Name;
        }
        else if (currentPlayerTurn == 3)
        {
            return string.IsNullOrWhiteSpace(RuleController.Instance.Player3Name) ? "Player3" : RuleController.Instance.Player3Name;
        }
        else if (currentPlayerTurn == 4)
        {
            return string.IsNullOrWhiteSpace(RuleController.Instance.Player4Name) ? "Player4" : RuleController.Instance.Player4Name;
        }
        else
        {
            return "Error Getting Player Name";
        }
        
    }

}
