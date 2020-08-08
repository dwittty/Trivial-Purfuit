using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DiceRoller: MonoBehaviour
{
    public Text textField;
    public bool isDisabled;
    public Sprite[] diceImages;
   
    // Start is called before the first frame update
    void Start()
    {
        isDisabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Roll()
    {
        if (!isDisabled)
        {
            DisableRollDiceButton();

            //int randomInt = Random.Range(1, 7); //Return number 1 to 6 (the top of the range is exclusive, not inclusive)        
            RuleController rc = FindObjectOfType<RuleController>() ?? new RuleController();
            int diceResult = rc.rollDice();
            Debug.Log($"You rolled a {diceResult}.");
            textField.text = "The dice result is: " + diceResult.ToString();
            Debug.Log($"User moved by {diceResult}. Location updated.");

            this.transform.GetChild(0).GetComponent<Image>().sprite = diceImages[diceResult - 1];
                        
            var playerToken = PlayerToken.FindActivePlayerToken();
            playerToken.SetSpacesRemainingInMove(diceResult);
            playerToken.ChooseDirectionToMove();
        }
        else
        {
            Debug.Log($"The roll dice button is currently disabled.");
        }
    }

    public void DisableRollDiceButton()
    {
        var buttonImage = this.gameObject.GetComponent<Image>();
        buttonImage.color = Color.grey;
        isDisabled = true;        
    }

    public void EnableRollDiceButton()
    {
        var buttonImage = this.gameObject.GetComponent<Image>();
        buttonImage.color = Color.white;
        isDisabled = false;
    }


    private IEnumerator Waiter(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

}
