using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class SetupGameBoard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RuleController rc = FindObjectOfType<RuleController>();
        int numPlayers = rc.GetNumberOfPlayers();
        var allPlayerTokens = FindObjectsOfType<PlayerToken>();
        if (numPlayers < 4)
        {
            PlayerToken player4 = allPlayerTokens.FirstOrDefault(x => x.name == "Player4");
            player4.transform.gameObject.SetActive(false);
        }
        if (numPlayers < 3)
        {
            PlayerToken player3 = allPlayerTokens.FirstOrDefault(x => x.name == "Player3");
            player3.transform.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
