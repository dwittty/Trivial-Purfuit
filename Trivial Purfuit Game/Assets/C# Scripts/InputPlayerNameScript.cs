using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputPlayerNameScript : MonoBehaviour
{

    EventSystem system;

    // Start is called before the first frame update
    void Start()
    {
        system = EventSystem.current;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (!next.name.Contains("InputField"))
            {
                next = FindObjectsOfType<InputField>().FirstOrDefault(x => x.name == "InputField_P1");
            }
            if (next != null)
            {

                InputField inputfield = next.GetComponent<InputField>();
                if (inputfield != null)
                    inputfield.OnPointerClick(new PointerEventData(system));  //if it's an input field, also set the text caret

                system.SetSelectedGameObject(next.gameObject, new BaseEventData(system));
                }
            //else Debug.Log("next nagivation element not found");

        }
    }
}
