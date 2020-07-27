using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuestionDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Question GetNewQuestion(string color, QuestionDatabase _qdb)
    {
        Question question;
        if (color.ToUpper() == "RED")
        {
            question = _qdb.GetQuestion(Category.RED);
        }
        else if (color.ToUpper() == "BLUE")
        {
            question = _qdb.GetQuestion(Category.BLUE);
        }
        else if (color.ToUpper() == "GREEN")
        {
            question = _qdb.GetQuestion(Category.GREEN);
        }
        else
        {
            question = _qdb.GetQuestion(Category.WHITE);
        }
        return question;        
    }

    public void DisplayQuestion(Question question)
    {
        var questionText = FindObjectsOfType<Text>().FirstOrDefault(x => x.name == "Question");
        questionText.text = question.Prompt;
        var answerA = FindObjectsOfType<Button>().FirstOrDefault(x => x.name == "AnswerA");
        answerA.GetComponentInChildren<Text>().text = question.Correct;
        var answerB = FindObjectsOfType<Button>().FirstOrDefault(x => x.name == "AnswerB");
        answerB.GetComponentInChildren<Text>().text = question.Wrong1;
        var answerC = FindObjectsOfType<Button>().FirstOrDefault(x => x.name == "AnswerC");
        answerC.GetComponentInChildren<Text>().text = question.Wrong2;
        var answerD = FindObjectsOfType<Button>().FirstOrDefault(x => x.name == "AnswerD");
        answerD.GetComponentInChildren<Text>().text = question.Wrong3;
    }
}
