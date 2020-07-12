using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeSquareStatus : MonoBehaviour
{
    private bool[,] _isFilled;
    private int _numColor = 4;
    private int _numUsers;
    
    public CakeSquareStatus(int numUsers)
    {
        _numUsers = numUsers;
        _isFilled = new bool[numUsers ,_numColor];
        for (int i=0;i<numUsers;i++)
        {
            for (int j=0;j<_numColor;j++)
            {
                _isFilled[i][j] = false;
            }
        }


    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void setStatus(int player,int color,bool update)        //setter
    {
        _isFilled[player][color] = update;
    }

    private int getStatus(int player, int color)                    //getter
    {
        return _isFilled[player][color];
    }

    public int provideStatus(int player,int color)
    {
        return getStatus(player,color);
    }

    public bool isFull(int player)
    {
        bool temp = true;
       for (int i=0;i<_numColor;i++)
        {

            if (_isFilled[player][i]==false)
            {
                temp = false;
            }

        }
        return temp;
    }
}
