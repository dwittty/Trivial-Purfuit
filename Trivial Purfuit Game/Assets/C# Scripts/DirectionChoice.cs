using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionChoice : MonoBehaviour
{
    public Tile SelectedTile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void selectButtonClick()
    {
        //Tile selectedTile = this.GetComponentInParent<Tile>();
        
        //find all game objects tagged as 'DirectionChoice' and destroy them
        var directionChoiceGameObjects = GameObject.FindGameObjectsWithTag("DirectionChoice");
        foreach (var item in directionChoiceGameObjects)
        {
            Destroy(item);
        }

        var playerToken = PlayerToken.FindActivePlayerToken();        
        playerToken.MoveToken(SelectedTile);
    }

}
