using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DiceRoller: MonoBehaviour
{
    public Text textField;
    public bool isDisabled;
    public bool isRolling; 

    public Sprite[] diceImages;
   
    // Start is called before the first frame update
    void Start()
    {
        isDisabled = false;
        isRolling = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRolling)
        {
            textField.text = "Moves Left: ?";
        }        
    }

    public void Roll()
    {
        if (!isDisabled)
        {
            DisableRollDiceButton();

            //int randomInt = Random.Range(1, 7); //Return number 1 to 6 (the top of the range is exclusive, not inclusive)                    
            int diceResult = RuleController.Instance.rollDice();

            //animate random images for length of time specified in seconds by the float value
            StartCoroutine(RollDiceAnimator(1f, diceResult));                                             
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

    public IEnumerator RollDiceAnimator(float animationTime, int finalResult) {
        int iterationsToAnimate = (int)(20 * animationTime);
        for (int i = 0; i <= iterationsToAnimate; i++)
        {            
            var randomDiceNumber = Random.Range(0, 5);            
            this.transform.GetChild(0).GetComponent<Image>().sprite = diceImages[randomDiceNumber];            
            yield return new WaitForSeconds(0.05f);
        }
        //after animation of random images, update to final result from rule controller
        Debug.Log($"You rolled a {finalResult}.");
        textField.text = "Moves Left: " + finalResult.ToString();
        Debug.Log($"User moved by {finalResult}. Location updated.");

        this.transform.GetChild(0).GetComponent<Image>().sprite = diceImages[finalResult - 1];

        var playerToken = PlayerToken.FindActivePlayerToken();
        playerToken.SetSpacesRemainingInMove(finalResult);
        playerToken.ChooseDirectionToMove();
    }

}
