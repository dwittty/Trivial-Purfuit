using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeSquareStatus : MonoBehaviour
{
    private bool[,] _isFilled;
    private int _numColor = 4;
    public CakeSquareStatus(int numUsers)
    {

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
}
