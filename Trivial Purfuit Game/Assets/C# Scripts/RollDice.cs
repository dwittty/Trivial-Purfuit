using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDice : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnButtonPress()
    {
        int randomInt = Random.Range(1, 7); //Return number 1 to 6 (the top of the range is exclusive, not inclusive)        
        Debug.Log($"You rolled a {randomInt}.");        
    }
}
