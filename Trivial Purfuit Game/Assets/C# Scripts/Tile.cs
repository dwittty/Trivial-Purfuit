using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{

    public Tile[] NextTiles;

    public string color;
    public bool IsCake;
    public bool IsStart;
    public bool IsRollAgain;

    // Start is called before the first frame update
    void Start()
    {
        color = this.GetColor();
        IsCake = this.CheckIfTileIsCake();
        IsStart = this.CheckIfTileIsStart();
        IsRollAgain = this.CheckIfTileIsRollAgain();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetColor()
    {
        var name = gameObject.name.ToUpper();
        if (name.Contains("RED"))
        {
            return "RED";
        }
        else if (name.Contains("BLUE"))
        {
            return "BLUE";
        }
        else if (name.Contains("GREEN"))
        {
            return "GREEN";
        }
        else if (name.Contains("WHITE"))
        {
            return "WHITE";
        }
        else
        {
            return "NONE";
        }
    }


    private bool CheckIfTileIsCake()
    {
        var name = gameObject.name.ToUpper();
        return name.Contains("CAKE");
    }

    public bool CheckIfTileIsStart()
    {
        var name = gameObject.name.ToUpper();
        return name.Contains("START");
    }

    private bool CheckIfTileIsRollAgain()
    {
        var name = gameObject.name.ToUpper();
        return name.Contains("ROLLAGAIN");
    }
}
