using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class RollDice: MonoBehaviour
{
    public Text textField;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText()
    {
        //int randomInt = Random.Range(1, 7); //Return number 1 to 6 (the top of the range is exclusive, not inclusive)        
        RuleController rc = new RuleController();
        int randomInt = rc.rollDice();
        Debug.Log($"You rolled a {randomInt}.");
        textField.text = "The dice result is: " + randomInt.ToString();
        Debug.Log($"User moved by {randomInt}. Location updated.");
        
        var playerToken = PlayerToken.FindActivePlayerToken();
        playerToken.MoveToken(randomInt);                

    }

    private IEnumerator Waiter(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

}
