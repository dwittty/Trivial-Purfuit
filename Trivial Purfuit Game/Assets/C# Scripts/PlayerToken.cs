using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerToken : MonoBehaviour
{
    // Start is called before the first frame update

    public Tile PreviousTile;
    public Tile CurrentTile;
    
    public PlayerToken()
    {
        PreviousTile = null;
        CurrentTile = null;
    }


    void Start()
    {
        CurrentTile = FindObjectsOfType<Tile>().FirstOrDefault(x => x.name == "4_4_StartTile"); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToken()
    {
        //take previous tile option away so player cannot go backwards
        var nameOfLastTile = PreviousTile?.name ?? "";
        var tileThatIsNotPrevious = CurrentTile.NextTiles.FirstOrDefault(x => x.name != nameOfLastTile);
        PreviousTile = CurrentTile;
        CurrentTile = tileThatIsNotPrevious;

        var newPosition = tileThatIsNotPrevious.transform.position;
        newPosition.z = -1; //make z index -1 so the player token is always closer to the camera than the board object.
        this.transform.position = newPosition;
        
    }

    public void MoveTokenWithChoice(Tile tileToMoveTo)
    {

    }
}
