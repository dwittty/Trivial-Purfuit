using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PlayerToken : MonoBehaviour
{
    // Start is called before the first frame update

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

    public PlayerToken()
    {
        PreviousTile = null;
        CurrentTile = null;
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

    public void MoveToken(int spacesToMove)
    {
        moveQueue = new Tile[spacesToMove];

        for (int i = 0; i < spacesToMove; i++)
        {
            //take previous tile option away so player cannot go backwards
            var nameOfLastTile = PreviousTile?.name ?? "";

            //just chooses any... in the future may need to pass one in if the player selects a choice (i.e. at a branch)
            var nextTile = CurrentTile.NextTiles.FirstOrDefault(x => x.name != nameOfLastTile);
            PreviousTile = CurrentTile;
            CurrentTile = nextTile;

            //var newPosition = nextTile.transform.position;
            
            ////this.transform.position = newPosition;
            //SetNewTargetPosition(newPosition);

            moveQueue[i] = nextTile;
        }

        moveQueueIndex = 0;
        
    }

    void SetNewTargetPosition(Vector3 pos)
    {        
        pos.z = 1; //make z index -1 so the player token is always closer to the camera than the board object.
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

   
}
