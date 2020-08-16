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
        var currenPlayerName = RuleController.Instance.GetPlayerName(currentPlayer);
        var color = RuleController.Instance.GetPlayerColor(currentPlayer);
        guiTextObject.text = $"<color={color}>{currenPlayerName}'s Turn</color>";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateTurnBanner(int player)
    {
        var currenPlayerName = RuleController.Instance.GetPlayerName(player);
        var guiTextObject = this.gameObject.GetComponent<Text>();
        var color = RuleController.Instance.GetPlayerColor(player);
        guiTextObject.text = $"<color={color}>{currenPlayerName}'s Turn</color>";
    }
    
   
    

}
