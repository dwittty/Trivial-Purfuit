using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;

public class PlayerToken : MonoBehaviour
{
    // Start is called before the first frame update

    public bool hasRedCake;
    public bool hasBlueCake;
    public bool hasGreenCake;
    public bool hasWhiteCake;

    public Sprite[] playerTokenImages;

    public Tile PreviousTile;
    public Tile CurrentTile;   

    public float xOffset;
    public float yOffset;

    Vector3 targetPosition; //place where tile is moving to in move
    Vector3 velocity = Vector3.zero;
    float smoothTime = 0.25f; //take 0.25 seconds to complete move.
    float smoothDistance = 0.01f;
    Tile[] moveQueue;
    int moveQueueIndex;

    int spacesRemainingInMove;
    public GameObject directionChoiceButtonPrefab;

    public PlayerToken()
    {
        PreviousTile = null;
        CurrentTile = null;
        spacesRemainingInMove = 0;
    }


    void Start()
    {
        CurrentTile = FindObjectsOfType<Tile>().FirstOrDefault(x => x.name == "4_4_StartTile");
        targetPosition = this.transform.position;      
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, targetPosition) < smoothDistance)
        {
            if (moveQueue != null && moveQueueIndex < moveQueue.Length)
            {
                SetNewTargetPosition(moveQueue[moveQueueIndex].transform.position);
                moveQueueIndex++;
            }
        }
        this.transform.position = Vector3.SmoothDamp(this.transform.position, targetPosition, ref velocity, smoothTime);
    }


    //puts buttons in the available move directions for the player to choose from
    public void ChooseDirectionToMove()
    {       
        //take previous tile option away so player cannot go backwards
        var nameOfLastTile = PreviousTile?.name ?? "";

        //list of tiles that are not 'previous tile.' If this is the first move after rolling dice, this will be all adjacent tiles.
        var nextTiles = CurrentTile.NextTiles.Where(x => x.name != nameOfLastTile).ToList();

        var canvas = FindObjectOfType<Canvas>();
          
        foreach (Tile tile in nextTiles)
        {
            Vector3 tilePosition = tile.transform.position;
            tilePosition.z = -2; //make sure button appears above the board and any other player tokens

            //var screenPosition = Camera.main.WorldToScreenPoint(tile.transform.position);
            //screenPosition.z = -2;
            //GameObject newButton = Instantiate(directionChoiceButtonPrefab, screenPosition, Quaternion.identity);
            //newButton.transform.SetParent(canvas.transform, true);spacesRemainingInMove--;

            var screenPosition = Camera.main.WorldToScreenPoint(tile.transform.position);
            screenPosition.z = -2;
            GameObject newButton = Instantiate(directionChoiceButtonPrefab, screenPosition, Quaternion.identity);
            newButton.transform.SetParent(tile.transform, true); //needs a parent that is part of a canvas to show up on the screen. With tile as parent, can use parent info to determine which tile they selected
        }
    }

    public void MoveToken(Tile tileSelectedByUser)
    {
        UpdatePlayerTokenSprite();
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
            //var newPosition = nextTile.transform.position;
            
            ////this.transform.position = newPosition;
            //SetNewTargetPosition(newPosition);
        }
        //reset variables for next move
        moveQueueIndex = 0;
        PreviousTile = null;
    }

    void SetNewTargetPosition(Vector3 pos)
    {        
        pos.z = -1; //make z index -1 so the player token is always closer to the camera than the board object.
        pos.x += xOffset;  //adjust position by player offset
        pos.y += yOffset;  //adjust position by player offset

        targetPosition = pos;
        velocity = Vector3.zero;
    }

    internal static PlayerToken FindActivePlayerToken()
    {
        int currentTurn = RuleController.CurrentTurn();
        var playerTokens = FindObjectsOfType<PlayerToken>().ToList();
        var activePlayer = playerTokens.FirstOrDefault(x => x.name.Contains(currentTurn.ToString()));
        return activePlayer;
    }

    void UpdatePlayerTokenSprite()
    {
        var foo = this.transform.GetChild(0);
        var bar = foo.GetComponent<SpriteRenderer>();
        var foobar = bar.sprite = playerTokenImages[UnityEngine.Random.Range(0, 16)];
    }


    public void SetSpacesRemainingInMove(int spacesToMove)
    {
        spacesRemainingInMove = spacesToMove;
    }
   
}
