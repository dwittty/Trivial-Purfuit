using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question : MonoBehaviour
{        
    public string QuestionString { get; set; }
    public string CorrectAnswer { get; set; }
    public List<string> AnswerChoices { get; set; }
    

    public Question(string question, string answer, List<string> choices)
    {
        QuestionString = question;
        CorrectAnswer = answer;
        AnswerChoices = choices;
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
