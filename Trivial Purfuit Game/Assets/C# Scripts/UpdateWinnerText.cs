using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateWinnerText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var textObject = this.gameObject.GetComponent<Text>();
        var color = RuleController.Instance.GetPlayerColor(RuleController.Instance.GetCurrentTurn());
        var currentPlayerName = RuleController.Instance.GetPlayerName(RuleController.Instance.GetCurrentTurn());
        textObject.text = $"Congratulations <color={color}>{currentPlayerName}</color>!\nYou Win!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
