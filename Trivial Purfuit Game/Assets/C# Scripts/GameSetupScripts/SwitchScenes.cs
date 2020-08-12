using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchScenes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        PlayerSelectionBehavior playerSelection = FindObjectOfType<PlayerSelectionBehavior>();
        RuleController rc = FindObjectOfType<RuleController>() ?? new RuleController();
        var numPlayers = playerSelection.GetNumberOfPlayersForGame();
        rc.SetNumberOfPlayers(numPlayers);
        SetPlayerNames(rc, numPlayers);        

        SceneManager.LoadScene("PlayGame");              
    }

    private void SetPlayerNames(RuleController rc, int numPlayers)
    {
        var inputParentObject = GameObject.FindGameObjectWithTag("PlayerNameInput");

        rc.Player1Name = inputParentObject.transform.Find("InputField_P1").gameObject.GetComponentsInChildren<Text>().FirstOrDefault(x => x.name == "Text").text;
        rc.Player2Name = inputParentObject.transform.Find("InputField_P2").gameObject.GetComponentsInChildren<Text>().FirstOrDefault(x => x.name == "Text").text;

        if (numPlayers > 2)
        {
            rc.Player3Name = inputParentObject.transform.Find("InputField_P3").gameObject.GetComponentsInChildren<Text>().FirstOrDefault(x => x.name == "Text").text;
        }
        if (numPlayers > 3)
        {
            rc.Player4Name = inputParentObject.transform.Find("InputField_P4").gameObject.GetComponentsInChildren<Text>().FirstOrDefault(x => x.name == "Text").text;
        }        
    }
}
