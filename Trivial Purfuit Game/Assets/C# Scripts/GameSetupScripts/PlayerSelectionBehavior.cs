using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PlayerSelectionBehavior : MonoBehaviour
{
    private List<Toggle> toggles;
    private Toggle toggle2;
    private Toggle toggle3;
    private Toggle toggle4;

    // Start is called before the first frame update
    void Start()
    {        
        toggles = FindObjectsOfType<Toggle>().ToList();
        toggle2 = toggles.FirstOrDefault(x => x.name == "Toggle2Players");
        toggle3 = toggles.FirstOrDefault(x => x.name == "Toggle3Players");
        toggle4 = toggles.FirstOrDefault(x => x.name == "Toggle4Players");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PrintNewToggleValue(Toggle toggle)
    {
        bool status = toggle.isOn;
        print(toggle.name + "status: " + status);
    }
    
    //Allows only one button in the player selection group to be on at a time
    public void ToggleRadio(Toggle clickedToggle)
    {
        var clickedToggleName = clickedToggle.name;
        var activeToggles = toggles.Where(x => x.isOn).ToList();
        if(activeToggles.Count == 0)
        {
            clickedToggle.isOn = true; //clicked toggle was already the active toggle, leave it on
        }
        else  //turn off the previously active toggle and leave the clicked toggle on
        {
            var togglesToDeactivate = activeToggles.Where(x => x.name != clickedToggleName).ToList();
            foreach(Toggle toggle in togglesToDeactivate)
            {
                toggle.isOn = false;
            }

        }
    }

}
