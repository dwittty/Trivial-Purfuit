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
        var numPlayers = playerSelection.GetNumberOfPlayersForGame();
        RuleController.Instance.SetNumberOfPlayers(numPlayers);
        SetPlayerNames(numPlayers);        

        SceneManager.LoadScene("PlayGame");              
    }

    private void SetPlayerNames(int numPlayers)
    {
        var inputParentObject = GameObject.FindGameObjectWithTag("PlayerNameInput");

        RuleController.Instance.Player1Name = inputParentObject.transform.Find("InputField_P1").gameObject.GetComponentsInChildren<Text>().FirstOrDefault(x => x.name == "Text").text;
        RuleController.Instance.Player2Name = inputParentObject.transform.Find("InputField_P2").gameObject.GetComponentsInChildren<Text>().FirstOrDefault(x => x.name == "Text").text;

        if (numPlayers > 2)
        {
            RuleController.Instance.Player3Name = inputParentObject.transform.Find("InputField_P3").gameObject.GetComponentsInChildren<Text>().FirstOrDefault(x => x.name == "Text").text;
        }
        if (numPlayers > 3)
        {
            RuleController.Instance.Player4Name = inputParentObject.transform.Find("InputField_P4").gameObject.GetComponentsInChildren<Text>().FirstOrDefault(x => x.name == "Text").text;
        }        
    }
}
