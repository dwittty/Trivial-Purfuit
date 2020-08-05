using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{

    public Tile[] NextTiles;

    public string color;
    public bool isCake;
    public bool isStart;
   

    // Start is called before the first frame update
    void Start()
    {
        color = this.GetColor();
        isCake = this.IsCake();
        isStart = this.IsStart();
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


    private bool IsCake()
    {
        var name = gameObject.name.ToUpper();
        return name.Contains("CAKE");
    }

    public bool IsStart()
    {
        var name = gameObject.name.ToUpper();
        return name.Contains("START");
    }
}
