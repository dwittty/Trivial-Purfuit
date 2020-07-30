using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnBanner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var guiTextObject = this.gameObject.GetComponent<Text>();
        var ruleController = FindObjectOfType<RuleController>();
        var currentPlayer = ruleController.CurrentTurn();
        guiTextObject.text = $"Player {currentPlayer}'s Turn";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateTurnBanner(string player)
    {
        var guiTextObject = this.gameObject.GetComponent<Text>();
        guiTextObject.text = $"Player {player}'s Turn";
    }
        

}
