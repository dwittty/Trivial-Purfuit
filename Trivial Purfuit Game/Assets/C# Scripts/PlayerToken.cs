﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;

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
                    UpdateMovesRemainingText();
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
                    if (CurrentTile.IsStart)
                    {
                        RuleController.Instance.PromptUserForColorSelection();
                    }
                    else if (CurrentTile.IsRollAgain)
                    {
                        RuleController.Instance.RollAgain();
                    }
                    else
                    {
                        RuleController.Instance.GetAndDisplayNewTriviaQuestion(CurrentTile.color);
                    }
                }
            }
        }        
        this.transform.position = Vector3.SmoothDamp(this.transform.position, targetPosition, ref velocity, smoothTime);
    }

    private void UpdateMovesRemainingText()
    {
        var movesRemainingText = FindObjectsOfType<Text>().FirstOrDefault(x => x.name == "MovesRemainingText");
        var lastCharacter = movesRemainingText.text.Substring(movesRemainingText.text.Length - 1);
        int previousSpacesLeft;
        int.TryParse(lastCharacter, out previousSpacesLeft);
        movesRemainingText.text = $"Moves Left: {previousSpacesLeft - 1}";
    }

    /*public void SetSpriteBasedOnCakeStatus(bool hasRedCake, bool hasBlueCake, bool hasGreenCake, bool hasWhiteCake) // round tokens, commented out for testing square tokens
    {
        if (!hasRedCake && !hasBlueCake && !hasGreenCake && !hasWhiteCake)
        {
            Sprite sprite = Resources.Load("Sprites/NoCake", typeof(Sprite)) as Sprite;
            UpdatePlayerTokenSprite(sprite);
        }
        else if (hasRedCake && !hasBlueCake && !hasGreenCake && !hasWhiteCake)
        {
            Sprite sprite = Resources.Load("Sprites/RedCake", typeof(Sprite)) as Sprite;
            UpdatePlayerTokenSprite(sprite);
        }
        else if (!hasRedCake && hasBlueCake && !hasGreenCake && !hasWhiteCake) 
        {
            Sprite sprite = Resources.Load("Sprites/BlueCake", typeof(Sprite)) as Sprite;
            UpdatePlayerTokenSprite(sprite);
        }
        else if (hasRedCake && hasBlueCake && !hasGreenCake && !hasWhiteCake)
        {
            Sprite sprite = Resources.Load("Sprites/RedBlueCake", typeof(Sprite)) as Sprite;
            UpdatePlayerTokenSprite(sprite);
        }
        else if (!hasRedCake && !hasBlueCake && hasGreenCake && !hasWhiteCake)
        {
            Sprite sprite = Resources.Load("Sprites/GreenCake", typeof(Sprite)) as Sprite;
            UpdatePlayerTokenSprite(sprite);
        }
        else if (hasRedCake && !hasBlueCake && hasGreenCake && !hasWhiteCake)
        {
            Sprite sprite = Resources.Load("Sprites/RedGreenCake", typeof(Sprite)) as Sprite;
            UpdatePlayerTokenSprite(sprite);
        }
        else if (!hasRedCake && hasBlueCake && hasGreenCake && !hasWhiteCake)
        {
            Sprite sprite = Resources.Load("Sprites/BlueGreenCake", typeof(Sprite)) as Sprite;
            UpdatePlayerTokenSprite(sprite);
        }
        else if (hasRedCake && hasBlueCake && hasGreenCake && !hasWhiteCake)
        {
            Sprite sprite = Resources.Load("Sprites/RedBlueGreenCake", typeof(Sprite)) as Sprite;
            UpdatePlayerTokenSprite(sprite);
        }
        else if (!hasRedCake && !hasBlueCake && !hasGreenCake && hasWhiteCake)
        {
            Sprite sprite = Resources.Load("Sprites/WhiteCake", typeof(Sprite)) as Sprite;
            UpdatePlayerTokenSprite(sprite);
        }
        else if (hasRedCake && !hasBlueCake && !hasGreenCake && hasWhiteCake)
        {
            Sprite sprite = Resources.Load("Sprites/RedWhiteCake", typeof(Sprite)) as Sprite;
            UpdatePlayerTokenSprite(sprite);
        }
        else if (!hasRedCake && hasBlueCake && !hasGreenCake && hasWhiteCake)
        {
            Sprite sprite = Resources.Load("Sprites/BlueWhiteCake", typeof(Sprite)) as Sprite;
            UpdatePlayerTokenSprite(sprite);
        }
        else if (hasRedCake && hasBlueCake && !hasGreenCake && hasWhiteCake)
        {
            Sprite sprite = Resources.Load("Sprites/RedBlueWhiteCake", typeof(Sprite)) as Sprite;
            UpdatePlayerTokenSprite(sprite);
        }
        else if (!hasRedCake && !hasBlueCake && hasGreenCake && hasWhiteCake)
        {
            Sprite sprite = Resources.Load("Sprites/GreenWhiteCake", typeof(Sprite)) as Sprite;
            UpdatePlayerTokenSprite(sprite);
        }
        else if (hasRedCake && !hasBlueCake && hasGreenCake && hasWhiteCake)
        {
            Sprite sprite = Resources.Load("Sprites/RedGreenWhiteCake", typeof(Sprite)) as Sprite;
            UpdatePlayerTokenSprite(sprite);
        }
        else if (!hasRedCake && hasBlueCake && hasGreenCake && hasWhiteCake)
        {
            Sprite sprite = Resources.Load("Sprites/BlueGreenWhiteCake", typeof(Sprite)) as Sprite;
            UpdatePlayerTokenSprite(sprite);
        }
        else if (hasRedCake && hasBlueCake && !hasGreenCake && hasWhiteCake)
        {
            Sprite sprite = Resources.Load("Sprites/RedBlueWhiteCake", typeof(Sprite)) as Sprite;
            UpdatePlayerTokenSprite(sprite);
        }
        else if (hasRedCake && hasBlueCake && hasGreenCake && hasWhiteCake)
        {
            Sprite sprite = Resources.Load("Sprites/RedBlueGreenWhiteCake", typeof(Sprite)) as Sprite;
            UpdatePlayerTokenSprite(sprite);
        }
        else
        {
            Sprite sprite = Resources.Load("Sprites/Uhoh", typeof(Sprite)) as Sprite;
            UpdatePlayerTokenSprite(sprite);
        }
    }*/

    public void SetSpriteBasedOnCakeStatus(bool hasRedCake, bool hasBlueCake, bool hasGreenCake, bool hasWhiteCake) // square tokens
    {
        string folderName = "";
        string fileNameDecorator = "";
        if(this.gameObject.name == "Player1")
        {
            folderName = "P1";
            fileNameDecorator = "1";
        }
        else if (this.gameObject.name == "Player2")
        {
            folderName = "P2";
            fileNameDecorator = "2";
        }
        else if (this.gameObject.name == "Player3")
        {
            folderName = "P3";
            fileNameDecorator = "3";
        }
        else
        {
            folderName = "P4";
            fileNameDecorator = "4";
        }

        if (!hasRedCake && !hasBlueCake && !hasGreenCake && !hasWhiteCake)
        {
            Sprite sprite = Resources.Load($"Sprites/{folderName}/NoCakeS{fileNameDecorator}", typeof(Sprite)) as Sprite;
            UpdatePlayerTokenSprite(sprite);
        }
        else if (hasRedCake && !hasBlueCake && !hasGreenCake && !hasWhiteCake)
        {
            Sprite sprite = Resources.Load($"Sprites/{folderName}/RedCakeS{fileNameDecorator}", typeof(Sprite)) as Sprite;
            UpdatePlayerTokenSprite(sprite);
        }
        else if (!hasRedCake && hasBlueCake && !hasGreenCake && !hasWhiteCake)
        {
            Sprite sprite = Resources.Load($"Sprites/{folderName}/BlueCakeS{fileNameDecorator}", typeof(Sprite)) as Sprite;
            UpdatePlayerTokenSprite(sprite);
        }
        else if (hasRedCake && hasBlueCake && !hasGreenCake && !hasWhiteCake)
        {
            Sprite sprite = Resources.Load($"Sprites/{folderName}/RedBlueCakeS{fileNameDecorator}", typeof(Sprite)) as Sprite;
            UpdatePlayerTokenSprite(sprite);
        }
        else if (!hasRedCake && !hasBlueCake && hasGreenCake && !hasWhiteCake)
        {
            Sprite sprite = Resources.Load($"Sprites/{folderName}/GreenCakeS{fileNameDecorator}", typeof(Sprite)) as Sprite;
            UpdatePlayerTokenSprite(sprite);
        }
        else if (hasRedCake && !hasBlueCake && hasGreenCake && !hasWhiteCake)
        {
            Sprite sprite = Resources.Load($"Sprites/{folderName}/RedGreenCakeS{fileNameDecorator}", typeof(Sprite)) as Sprite;
            UpdatePlayerTokenSprite(sprite);
        }
        else if (!hasRedCake && hasBlueCake && hasGreenCake && !hasWhiteCake)
        {
            Sprite sprite = Resources.Load($"Sprites/{folderName}/BlueGreenCakeS{fileNameDecorator}", typeof(Sprite)) as Sprite;
            UpdatePlayerTokenSprite(sprite);
        }
        else if (hasRedCake && hasBlueCake && hasGreenCake && !hasWhiteCake)
        {
            Sprite sprite = Resources.Load($"Sprites/{folderName}/RedBlueGreenCakeS{fileNameDecorator}", typeof(Sprite)) as Sprite;
            UpdatePlayerTokenSprite(sprite);
        }
        else if (!hasRedCake && !hasBlueCake && !hasGreenCake && hasWhiteCake)
        {
            Sprite sprite = Resources.Load($"Sprites/{folderName}/WhiteCakeS{fileNameDecorator}", typeof(Sprite)) as Sprite;
            UpdatePlayerTokenSprite(sprite);
        }
        else if (hasRedCake && !hasBlueCake && !hasGreenCake && hasWhiteCake)
        {
            Sprite sprite = Resources.Load($"Sprites/{folderName}/RedWhiteCakeS{fileNameDecorator}", typeof(Sprite)) as Sprite;
            UpdatePlayerTokenSprite(sprite);
        }
        else if (!hasRedCake && hasBlueCake && !hasGreenCake && hasWhiteCake)
        {
            Sprite sprite = Resources.Load($"Sprites/{folderName}/BlueWhiteCakeS{fileNameDecorator}", typeof(Sprite)) as Sprite;
            UpdatePlayerTokenSprite(sprite);
        }
        else if (hasRedCake && hasBlueCake && !hasGreenCake && hasWhiteCake)
        {
            Sprite sprite = Resources.Load($"Sprites/{folderName}/RedBlueWhiteCakeS{fileNameDecorator}", typeof(Sprite)) as Sprite;
            UpdatePlayerTokenSprite(sprite);
        }
        else if (!hasRedCake && !hasBlueCake && hasGreenCake && hasWhiteCake)
        {
            Sprite sprite = Resources.Load($"Sprites/{folderName}/GreenWhiteCakeS{fileNameDecorator}", typeof(Sprite)) as Sprite;
            UpdatePlayerTokenSprite(sprite);
        }
        else if (hasRedCake && !hasBlueCake && hasGreenCake && hasWhiteCake)
        {
            Sprite sprite = Resources.Load($"Sprites/{folderName}/RedGreenWhiteCakeS{fileNameDecorator}", typeof(Sprite)) as Sprite;
            UpdatePlayerTokenSprite(sprite);
        }
        else if (!hasRedCake && hasBlueCake && hasGreenCake && hasWhiteCake)
        {
            Sprite sprite = Resources.Load($"Sprites/{folderName}/BlueGreenWhiteCakeS{fileNameDecorator}", typeof(Sprite)) as Sprite;
            UpdatePlayerTokenSprite(sprite);
        }
        else if (hasRedCake && hasBlueCake && !hasGreenCake && hasWhiteCake)
        {
            Sprite sprite = Resources.Load($"Sprites/{folderName}/RedBlueWhiteCakeS{fileNameDecorator}", typeof(Sprite)) as Sprite;
            UpdatePlayerTokenSprite(sprite);
        }
        else if (hasRedCake && hasBlueCake && hasGreenCake && hasWhiteCake)
        {
            Sprite sprite = Resources.Load($"Sprites/{folderName}/RedBlueGreenWhiteCakeS{fileNameDecorator}", typeof(Sprite)) as Sprite;
            UpdatePlayerTokenSprite(sprite);
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
        //update here in case a new direction is chosen while the token is still in transit to the intersection
        var movesRemainingText = FindObjectsOfType<Text>().FirstOrDefault(x => x.name == "MovesRemainingText");                    
        movesRemainingText.text = $"Moves Left: {spacesRemainingInMove}";

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


        RuleController.Instance.ReceiveUserLocation(pos.x, pos.y);

        targetPosition = pos;
        velocity = Vector3.zero;
    }

    internal static PlayerToken FindActivePlayerToken()
    {        
        var currentPlayer = RuleController.Instance.GetCurrentTurn();
        var playerTokens = FindObjectsOfType<PlayerToken>().ToList();
        var activePlayer = playerTokens.FirstOrDefault(x => x.name.Contains(currentPlayer.ToString()));
        return activePlayer;
    }

    public void SetSpacesRemainingInMove(int spacesToMove)
    {
        spacesRemainingInMove = spacesToMove;
    }
   
}
