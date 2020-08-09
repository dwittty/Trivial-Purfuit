using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;
using System.Runtime.Versioning;
//using System.Diagnostics;

public class PlayerToken : MonoBehaviour
{      
    public Tile PreviousTile;
    public Tile CurrentTile;   

    public float xOffset;
    public float yOffset;

    //tile position is giving a world position that is off by a fixed amount each time... this fixes it though it might be good to investigate root cause at some point
    public float xPositionOffset = 14.8895904f;     
    public float yPositionOffset = 8.500021216f;

    Vector3 targetPosition; //place where tile is moving to in move
    Vector3 velocity = Vector3.zero;
    float smoothTime = 0.25f; //take 0.25 seconds to complete move.
    float smoothDistance = 0.01f;
    bool newQuestionNeeded;

    Tile[] moveQueue;
    int moveQueueIndex;

    int spacesRemainingInMove;
    public GameObject directionChoiceButtonPrefab;

    public PlayerToken()
    {
        PreviousTile = null;        
        spacesRemainingInMove = 0;
        newQuestionNeeded = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        PreviousTile = null;
        CurrentTile = FindObjectsOfType<Tile>().FirstOrDefault(x => x.name == "4_4_StartTile");
        targetPosition = this.transform.position;
        xPositionOffset = 15f;
        yPositionOffset = 8.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, targetPosition) < smoothDistance)
        {
            if (moveQueue != null && moveQueueIndex < moveQueue.Length)
            {
                if (moveQueue[moveQueueIndex] != null) //null happens when waiting for user input at an intersection, there's more spaces to move but we don't know what they are yet.
                {
                    SetNewTargetPosition(moveQueue[moveQueueIndex].transform.position);
                    moveQueueIndex++;
                }
            }
            else
            {
                //no spaces remaining in the move, token has moved to the designated location, and a new question has not yet been delivered to the user
                if (spacesRemainingInMove == 0 && newQuestionNeeded)
                {
                    newQuestionNeeded = false;
                    //move is complete, ask ruleController for a question:
                    RuleController rc = FindObjectOfType<RuleController>() ?? new RuleController(); // will be null when debugging if you dont start from Scene 1.         
                    if (CurrentTile.IsStart)
                    {
                        rc.PromptUserForColorSelection();
                    }
                    else if (CurrentTile.IsRollAgain)
                    {
                        rc.RollAgain();
                    }
                    else
                    {
                        rc.GetAndDisplayNewTriviaQuestion(CurrentTile.color);
                    }
                }
            }
        }        
        this.transform.position = Vector3.SmoothDamp(this.transform.position, targetPosition, ref velocity, smoothTime);
    }
    
    //////////////////////////////////////////////////////
    //This is where I was working to make it so the new and improved player tokens in color (R,W,B,G) would display.  They are located in Sprites under P1-P4 and following the naming convention set out before
    //<Red><Blue><Green><White>CakeS.png.  Each of the folders coincides with one of the players (P1-4 respectively) with the files in each having the same name (E.g. Each of the folders has a RedBlueCakeS.png).
    //What I was attempting to do was get the _playerID from Player and then when SetSpriteBasedOnCakeStatus checked to see which of the flags are true it would then check the player number (via the convoluted
    //mess below above the first if statement) and update the sprite from P1-4 (depending upon the player) with the correct color and cake squares.
    //I attempted to pass the file location via a variable though that didn't work.  And now it has the correct NoCakeS.png for each of the player tokens, but once you get your first cake square the token disappears
    //from the board.
    //
    //I will be out 8/9 and unavailable all day for a work commitment, but will check in sometime in the evening.  Thank you for taking a look and seeing what I'm missing.  And there's got to be a better way to do
    //lines 103-108!  
    
    public void SetSpriteBasedOnCakeStatus(bool hasRedCake, bool hasBlueCake, bool hasGreenCake, bool hasWhiteCake, PlayerToken playerNumPlayerToken) // square tokens
    {
        string playerNumString = playerNumPlayerToken.ToString(); // takes PlayerToken from Player and converts to a string. playerNumString will equal Player<#1-4>.
        string[] playerNumArray = playerNumString.Split('r'); // splits playerNumString array around the 'r'.
        string[] test = playerNumArray[1].Split(' '); // splits playerNumArray array around the ' '. test[0] equals string <#1-4>.
        int playerNumber = Int32.Parse(test[0]); // converts string of player number into an integer
        //int playerNumber = 1; // for debugging purposes, delete after done
        Debug.Log($"Player Number is {playerNumber}"); // for debugging purposes, delete after done
        if (!hasRedCake && !hasBlueCake && !hasGreenCake && !hasWhiteCake) // NoCakeS
        {
            if (playerNumber == 1)
            {
                Sprite sprite = Resources.Load("Sprites/P1/NoCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 2)
            {
                Sprite sprite = Resources.Load("Sprites/P2/NoCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 3)
            {
                Sprite sprite = Resources.Load("Sprites/P3/NoCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 4)
            {
                Sprite sprite = Resources.Load("Sprites/P4/NoCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            /*else
            {
                Sprite sprite = Resources.Load("Sprites/Uhoh", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }*/
        }
        else if (hasRedCake && !hasBlueCake && !hasGreenCake && !hasWhiteCake) // RedCakeS
        {
            if (playerNumber == 1)
            {
                Sprite sprite = Resources.Load("Sprites/P1/RedCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 2)
            {
                Sprite sprite = Resources.Load("Sprites/P2/RedCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 3)
            {
                Sprite sprite = Resources.Load("Sprites/P3/RedCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 4)
            {
                Sprite sprite = Resources.Load("Sprites/P4/RedCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            /*else
            {
                Sprite sprite = Resources.Load("Sprites/Uhoh", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }*/
        }
        else if (!hasRedCake && hasBlueCake && !hasGreenCake && !hasWhiteCake) // BlueCakeS
        {
            if (playerNumber == 1)
            {
                Sprite sprite = Resources.Load("Sprites/P1/BlueCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 2)
            {
                Sprite sprite = Resources.Load("Sprites/P2/BlueCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 3)
            {
                Sprite sprite = Resources.Load("Sprites/P3/BlueCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 4)
            {
                Sprite sprite = Resources.Load("Sprites/P4/BlueCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            /*else
            {
                Sprite sprite = Resources.Load("Sprites/Uhoh", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }*/
        }
        else if (hasRedCake && hasBlueCake && !hasGreenCake && !hasWhiteCake) // RedBlueCakeS
        {
            if (playerNumber == 1)
            {
                Sprite sprite = Resources.Load("Sprites/P1/RedBlueCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 2)
            {
                Sprite sprite = Resources.Load("Sprites/P2/RedBlueCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 3)
            {
                Sprite sprite = Resources.Load("Sprites/P3/RedBlueCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 4)
            {
                Sprite sprite = Resources.Load("Sprites/P4/RedBlueCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            /*else
            {
                Sprite sprite = Resources.Load("Sprites/Uhoh", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }*/
        }
        else if (!hasRedCake && !hasBlueCake && hasGreenCake && !hasWhiteCake) // GreenCakeS
        {
            if (playerNumber == 1)
            {
                Sprite sprite = Resources.Load("Sprites/P1/GreenCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 2)
            {
                Sprite sprite = Resources.Load("Sprites/P2/GreenCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 3)
            {
                Sprite sprite = Resources.Load("Sprites/P3/GreenCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 4)
            {
                Sprite sprite = Resources.Load("Sprites/P4/GreenCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            /*else
            {
                Sprite sprite = Resources.Load("Sprites/Uhoh", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }*/
        }
        else if (hasRedCake && !hasBlueCake && hasGreenCake && !hasWhiteCake) // RedGreenCakeS
        {
            if (playerNumber == 1)
            {
                Sprite sprite = Resources.Load("Sprites/P1/RedGreenCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 2)
            {
                Sprite sprite = Resources.Load("Sprites/P2/RedGreenCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 3)
            {
                Sprite sprite = Resources.Load("Sprites/P3/RedGreenCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 4)
            {
                Sprite sprite = Resources.Load("Sprites/P4/RedGreenCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            /*else
            {
                Sprite sprite = Resources.Load("Sprites/Uhoh", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }*/
        }
        else if (!hasRedCake && hasBlueCake && hasGreenCake && !hasWhiteCake) // BlueGreenCakeS
        {
            if (playerNumber == 1)
            {
                Sprite sprite = Resources.Load("Sprites/P1/BlueGreenCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 2)
            {
                Sprite sprite = Resources.Load("Sprites/P2/BlueGreenCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 3)
            {
                Sprite sprite = Resources.Load("Sprites/P3/BlueGreenCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 4)
            {
                Sprite sprite = Resources.Load("Sprites/P4/BlueGreenCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            /*else
            {
                Sprite sprite = Resources.Load("Sprites/Uhoh", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }*/
        }
        else if (hasRedCake && hasBlueCake && hasGreenCake && !hasWhiteCake) // RedBlueGreenCakeS
        {
            if (playerNumber == 1)
            {
                Sprite sprite = Resources.Load("Sprites/P1/RedBlueGreenCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 2)
            {
                Sprite sprite = Resources.Load("Sprites/P2/RedBlueGreenCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 3)
            {
                Sprite sprite = Resources.Load("Sprites/P3/RedBlueGreenCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 4)
            {
                Sprite sprite = Resources.Load("Sprites/P4/RedBlueGreenCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            /*else
            {
                Sprite sprite = Resources.Load("Sprites/Uhoh", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }*/
        }
        else if (!hasRedCake && !hasBlueCake && !hasGreenCake && hasWhiteCake) // WhiteCakeS
        {
            if (playerNumber == 1)
            {
                Sprite sprite = Resources.Load("Sprites/P1/WhiteCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 2)
            {
                Sprite sprite = Resources.Load("Sprites/P2/WhiteCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 3)
            {
                Sprite sprite = Resources.Load("Sprites/P3/WhiteCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 4)
            {
                Sprite sprite = Resources.Load("Sprites/P4/WhiteCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            /*else
            {
                Sprite sprite = Resources.Load("Sprites/Uhoh", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }*/
        }
        else if (hasRedCake && !hasBlueCake && !hasGreenCake && hasWhiteCake) // RedWhiteCakeS
        {
            if (playerNumber == 1)
            {
                Sprite sprite = Resources.Load("Sprites/P1/RedWhiteCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 2)
            {
                Sprite sprite = Resources.Load("Sprites/P2/RedWhiteCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 3)
            {
                Sprite sprite = Resources.Load("Sprites/P3/RedWhiteCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 4)
            {
                Sprite sprite = Resources.Load("Sprites/P4/RedWhiteCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            /*else
            {
                Sprite sprite = Resources.Load("Sprites/Uhoh", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }*/
        }
        else if (!hasRedCake && hasBlueCake && !hasGreenCake && hasWhiteCake) // BlueWhiteCakeS
        {
            if (playerNumber == 1)
            {
                Sprite sprite = Resources.Load("Sprites/P1/BlueWhiteCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 2)
            {
                Sprite sprite = Resources.Load("Sprites/P2/BlueWhiteCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 3)
            {
                Sprite sprite = Resources.Load("Sprites/P3/BlueWhiteCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 4)
            {
                Sprite sprite = Resources.Load("Sprites/P4/BlueWhiteCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            /*else
            {
                Sprite sprite = Resources.Load("Sprites/Uhoh", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }*/
        }
        else if (hasRedCake && hasBlueCake && !hasGreenCake && hasWhiteCake) // RedBlueWhiteCakeS
        {
            if (playerNumber == 1)
            {
                Sprite sprite = Resources.Load("Sprites/P1/RedBlueWhiteCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 2)
            {
                Sprite sprite = Resources.Load("Sprites/P2/RedBlueWhiteCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 3)
            {
                Sprite sprite = Resources.Load("Sprites/P3/RedBlueWhiteCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 4)
            {
                Sprite sprite = Resources.Load("Sprites/P4/RedBlueWhiteCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            /*else
            {
                Sprite sprite = Resources.Load("Sprites/Uhoh", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }*/
        }
        else if (!hasRedCake && !hasBlueCake && hasGreenCake && hasWhiteCake) // GreenWhiteCakeS
        {
            if (playerNumber == 1)
            {
                Sprite sprite = Resources.Load("Sprites/P1/GreenWhiteCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 2)
            {
                Sprite sprite = Resources.Load("Sprites/P2/GreenWhiteCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 3)
            {
                Sprite sprite = Resources.Load("Sprites/P3/GreenWhiteCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 4)
            {
                Sprite sprite = Resources.Load("Sprites/P4/GreenWhiteCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            /*else
            {
                Sprite sprite = Resources.Load("Sprites/Uhoh", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }*/
        }
        else if (hasRedCake && !hasBlueCake && hasGreenCake && hasWhiteCake) // RedGreenWhiteCakeS
        {
            if (playerNumber == 1)
            {
                Sprite sprite = Resources.Load("Sprites/P1/RedGreenWhiteCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 2)
            {
                Sprite sprite = Resources.Load("Sprites/P2/RedGreenWhiteCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 3)
            {
                Sprite sprite = Resources.Load("Sprites/P3/RedGreenWhiteCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 4)
            {
                Sprite sprite = Resources.Load("Sprites/P4/RedGreenWhiteCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            /*else
            {
                Sprite sprite = Resources.Load("Sprites/Uhoh", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }*/
        }
        else if (!hasRedCake && hasBlueCake && hasGreenCake && hasWhiteCake) // BlueGreenWhiteCakeS
        {
            if (playerNumber == 1)
            {
                Sprite sprite = Resources.Load("Sprites/P1/BlueGreenWhiteCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 2)
            {
                Sprite sprite = Resources.Load("Sprites/P2/BlueGreenWhiteCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 3)
            {
                Sprite sprite = Resources.Load("Sprites/P3/BlueGreenWhiteCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 4)
            {
                Sprite sprite = Resources.Load("Sprites/P4/BlueGreenWhiteCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            /*else
            {
                Sprite sprite = Resources.Load("Sprites/Uhoh", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }*/
        }
        else if (hasRedCake && hasBlueCake && !hasGreenCake && hasWhiteCake) // RedBlueWhiteCakeS
        {
            if (playerNumber == 1)
            {
                Sprite sprite = Resources.Load("Sprites/P1/RedBlueWhiteCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 2)
            {
                Sprite sprite = Resources.Load("Sprites/P2/RedBlueWhiteCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 3)
            {
                Sprite sprite = Resources.Load("Sprites/P3/RedBlueWhiteCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 4)
            {
                Sprite sprite = Resources.Load("Sprites/P4/RedBlueWhiteCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            /*else
            {
                Sprite sprite = Resources.Load("Sprites/Uhoh", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }*/
        }
        else if (hasRedCake && hasBlueCake && hasGreenCake && hasWhiteCake) // RedBlueGreenWhiteCakeS
        {
            if (playerNumber == 1)
            {
                Sprite sprite = Resources.Load("Sprites/P1/RedBlueGreenWhiteCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 2)
            {
                Sprite sprite = Resources.Load("Sprites/P2/RedBlueGreenWhiteCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 3)
            {
                Sprite sprite = Resources.Load("Sprites/P3/RedBlueGreenWhiteCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            else if (playerNumber == 4)
            {
                Sprite sprite = Resources.Load("Sprites/P4/RedBlueGreenWhiteCakeS", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }
            /*else
            {
                Sprite sprite = Resources.Load("Sprites/Uhoh", typeof(Sprite)) as Sprite;
                UpdatePlayerTokenSprite(sprite);
            }*/
        }
        else
        {
            Sprite sprite = Resources.Load("Sprites/Uhoh", typeof(Sprite)) as Sprite;
            UpdatePlayerTokenSprite(sprite);
        }
    }

    private void UpdatePlayerTokenSprite(Sprite newSprite)
    {        
        var spriteRenderer = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = newSprite;
    }
    //puts buttons in the available move directions for the player to choose from
    public void ChooseDirectionToMove()
    {     
        //take previous tile option away so player cannot go backwards
        var nameOfLastTile = PreviousTile?.name ?? "";

        //list of tiles that are not 'previous tile.' If this is the first move after rolling dice, this will be all adjacent tiles.
        var nextTiles = CurrentTile.NextTiles.Where(x => x.name != nameOfLastTile).ToList();

        var canvas = FindObjectsOfType<Canvas>().FirstOrDefault(x => x.name == "Canvas");

        foreach (Tile tile in nextTiles)
        {
            Vector3 tilePosition = tile.transform.position;
            tilePosition.x = tilePosition.x - xPositionOffset;
            tilePosition.y = tilePosition.y - yPositionOffset;
            tilePosition.z = -2; //make sure button appears above the board and any other player tokens                                                        
            Debug.Log($"Tileposition: {tilePosition.x},{tilePosition.y}");
            var screenPosition = Camera.main.WorldToScreenPoint(tilePosition);
            Debug.Log($"Screenposition: {screenPosition.x}, {screenPosition.y}");
            screenPosition.z = -2;
            GameObject newButton = Instantiate(directionChoiceButtonPrefab, screenPosition, Quaternion.identity);
            newButton.transform.SetParent(canvas.transform, true);
            
            //associate  tile to button so it can notify the move method of which tile to go to if this button is clicked.
            newButton.GetComponent<DirectionChoice>().SelectedTile = tile;

        }
    }


    //moves the player token until the dice value is used up or another branch is encountered, at which point the user is prompted for a choice again via ChooseDirectionToMove()
    public void MoveToken(Tile tileSelectedByUser)
    {
        newQuestionNeeded = true;
        moveQueue = new Tile[spacesRemainingInMove];
        int spacesToMoveLocal = spacesRemainingInMove;
        //while (spacesRemainingInMove >= 0)
        //{
        for (int i = 0; i < spacesToMoveLocal; i++)
        {
            //first pass through loop always choose tileSelectedByUser as the next tile
            if (i == 0)
            {
                PreviousTile = CurrentTile;
                CurrentTile = tileSelectedByUser;
                moveQueue[i] = tileSelectedByUser;
                spacesRemainingInMove--; //decrement spaces left to move
            }
            else
            {
                //take previous tile option away so player cannot go backwards
                var nameOfLastTile = PreviousTile?.name ?? "";                
                var nextTiles = CurrentTile.NextTiles.Where(x => x.name != nameOfLastTile).ToList();

                //no choice to be made, just move the token to the next space
                if (nextTiles.Count == 1) {
                    PreviousTile = CurrentTile;
                    CurrentTile = nextTiles[0];
                    moveQueue[i] = nextTiles[0];
                    spacesRemainingInMove--;
                }
                else
                {
                    ChooseDirectionToMove();
                    break;
                }                                              
            }  
        }
        //reset variables for next move
        moveQueueIndex = 0;
        PreviousTile = null;
    }

    void SetNewTargetPosition(Vector3 pos)
    {        
        pos.z = -1; //make z index -1 so the player token is always closer to the camera than the board object.
        pos.x += xOffset - xPositionOffset;  //adjust position by player offset 
        pos.y += yOffset - yPositionOffset;  //adjust position by player offset

        RuleController rc = FindObjectOfType<RuleController>();
        rc.receiveUserLocation(pos.x, pos.y);

        targetPosition = pos;
        velocity = Vector3.zero;
    }

    internal static PlayerToken FindActivePlayerToken()
    {
        var ruleController = FindObjectOfType<RuleController>() ?? new RuleController();
        var currentPlayer = ruleController.GetCurrentTurn();
        var playerTokens = FindObjectsOfType<PlayerToken>().ToList();
        var activePlayer = playerTokens.FirstOrDefault(x => x.name.Contains(currentPlayer.ToString()));
        return activePlayer;
    }

    public void SetSpacesRemainingInMove(int spacesToMove)
    {
        spacesRemainingInMove = spacesToMove;
    }
   
}
