using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class PlayerSelectionBehavior : MonoBehaviour
{
    private List<Toggle> toggles;
    private Toggle toggle2;
    private Toggle toggle3;
    private Toggle toggle4;
    private int numPlayer;    

    private List<Player> turnOrder;

    // Start is called before the first frame update
    void Start()
    {        
        toggles = FindObjectsOfType<Toggle>().ToList();
        toggle2 = toggles.FirstOrDefault(x => x.name == "Toggle2Players");
        toggle3 = toggles.FirstOrDefault(x => x.name == "Toggle3Players");
        toggle4 = toggles.FirstOrDefault(x => x.name == "Toggle4Players");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PrintNewToggleValue(Toggle toggle)
    {
        bool status = toggle.isOn;
        print(toggle.name + "status: " + status);        
    }

    //Returns the current number of players for game
    public int GetNumberOfPlayersForGame()
    {
        if (toggle2.isOn)
        {
            return 2;
        }
        else if (toggle3.isOn)
        {
            return 3;
        }
        else
        {
            return 4;
        }
    }

   


    //Allows only one button in the player selection group to be on at a time
    public void ToggleRadio(Toggle clickedToggle)
    {
        var clickedToggleName = clickedToggle.name;
        var activeToggles = toggles.Where(x => x.isOn).ToList();
        if(activeToggles.Count == 0)
        {
            clickedToggle.isOn = true; //clicked toggle was already the active toggle, leave it on
        }
        else  //turn off the previously active toggle and leave the clicked toggle on
        {
            var togglesToDeactivate = activeToggles.Where(x => x.name != clickedToggleName).ToList();
            foreach(Toggle toggle in togglesToDeactivate)
            {
                toggle.isOn = false;
            }

        }
        UpdatePlayerNameInputVisibility();

        DecideTurnOrder();      
    }

    private void UpdatePlayerNameInputVisibility()
    {
        var numPlayers = GetNumberOfPlayersForGame();
        var inputParentObject = GameObject.FindGameObjectWithTag("PlayerNameInput");

        if (numPlayers == 2)
        {
           inputParentObject.transform.Find("InputField_P3").gameObject.SetActive(false);
           inputParentObject.transform.Find("InputField_P4").gameObject.SetActive(false);
        }
        else if( numPlayers == 3)
        {
            inputParentObject.transform.Find("InputField_P3").gameObject.SetActive(true);
            inputParentObject.transform.Find("InputField_P4").gameObject.SetActive(false);
        }
        else if(numPlayers == 4)
        {
            inputParentObject.transform.Find("InputField_P3").gameObject.SetActive(true);
            inputParentObject.transform.Find("InputField_P4").gameObject.SetActive(true);
        }
    }

    private void DecideTurnOrder()  //logic to decide turn order by rolling a dice. Currently, automatically run the dice and set the turn order.
    {
        turnOrder = new List<Player>();

        RuleController rc = FindObjectOfType<RuleController>() ?? new RuleController();
        //RuleController rc = new RuleController();
        int temp;
        if (toggle2.isOn == true)
        {
            numPlayer = 2;
            turnOrder.Clear();
            Player player1 = new Player("player1", 1, rc.rollDice());

            Player player2 = new Player("player2", 2, rc.rollDice());

            turnOrder.Add(player1);
            turnOrder.Add(player2);
        }
        else if (toggle3.isOn == true)
        {
            numPlayer = 3;
            turnOrder.Clear();

            Player player1 = new Player("player1", 1, rc.rollDice());

            Player player2 = new Player("player2", 2, rc.rollDice());

            Player player3 = new Player("player3", 3, rc.rollDice());

            turnOrder.Add(player1);
            turnOrder.Add(player2);
            turnOrder.Add(player3);
        }
        else
        {
            numPlayer = 4;
            turnOrder.Clear();
            Player player1 = new Player("player1", 1, rc.rollDice());

            Player player2 = new Player("player2", 2, rc.rollDice());

            Player player3 = new Player("player3", 3, rc.rollDice());

            Player player4 = new Player("player4", 4, rc.rollDice());

            turnOrder.Add(player1);
            turnOrder.Add(player2);
            turnOrder.Add(player3);
            turnOrder.Add(player4);
        }

        turnOrder.Sort(delegate (Player x, Player y)        //sorting rule. if y has bigger dice number, y should go first. if x and y has same dice number, player which has smaller ID goes first. (if Player 1 and player 2 has same dice number, player 1 go first).
        {
            if (x.getDiceNum() < y.getDiceNum())
            {
                return 1;
            }
            else if (x.getDiceNum()==y.getDiceNum())
            {
                if (x.getPlayerID()>y.getPlayerID())
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            else
            {                
                return -1;
            }
        });

        rc.setTurnOrder(turnOrder);
    }

}
