﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnTimer : MonoBehaviour
{

    public float timeRemaining = 30;
    public bool timerRunning = true;

    public Text timeText;

    // Start is called before the first frame update
    void Start()
    {
        
    }
        
    // Update is called once per frame
    void Update()
    {
        if (timerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                if(timeRemaining < 10)
                {
                    timeText.color = Color.red;
                }
                else
                {
                    timeText.color = Color.white ;
                }
                DisplayTime(timeRemaining);
            }
            else
            {
                timeText.text = string.Format("{0:00}:{1:00}", 0, 0);
                Debug.Log("Out of time!!");
                timeRemaining = 0;
                timerRunning = false;
            }
        }
        else
        {
            //timer no longer running, they ran out of time, end the turn
            RuleController.Instance.DisplayMessage(5, "OUT OF TIME!", new Color32(205, 42, 44, 255));
            RuleController.Instance.EndTurn();
        }
        


    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1; //for truncating purposing since we are counting down

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


}

