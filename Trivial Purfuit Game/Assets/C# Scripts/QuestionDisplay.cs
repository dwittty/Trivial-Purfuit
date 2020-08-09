using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuestionDisplay : MonoBehaviour
{
    private static System.Random random = new System.Random();
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

        var answers = new Button[4];
        var shuffled = new string[4];
        shuffled[0] = question.Correct;
        shuffled[1] = question.Wrong1;
        shuffled[2] = question.Wrong2;
        shuffled[3] = question.Wrong3;

        int size = 4;
        while (size > 1)
        {
            size--;
            int k = random.Next(size + 1);
            string value = shuffled[k];
            shuffled[k] = shuffled[size];
            shuffled[size] = value;
        }





        answers[0]= FindObjectsOfType<Button>().FirstOrDefault(x => x.name == "AnswerA");
        answers[0].GetComponentInChildren<Text>().text = shuffled[0];

        answers[1] = FindObjectsOfType<Button>().FirstOrDefault(x => x.name == "AnswerB");
        answers[1].GetComponentInChildren<Text>().text = shuffled[1];

        answers[2] = FindObjectsOfType<Button>().FirstOrDefault(x => x.name == "AnswerC");
        answers[2].GetComponentInChildren<Text>().text = shuffled[2];

        answers[3] = FindObjectsOfType<Button>().FirstOrDefault(x => x.name == "AnswerD");
        answers[3].GetComponentInChildren<Text>().text = shuffled[3];

        /*
        var answerA = FindObjectsOfType<Button>().FirstOrDefault(x => x.name == "AnswerA");
        answerA.GetComponentInChildren<Text>().text = question.Correct;
        
        var answerB = FindObjectsOfType<Button>().FirstOrDefault(x => x.name == "AnswerB");
        answerB.GetComponentInChildren<Text>().text = question.Wrong1;
        
        var answerC = FindObjectsOfType<Button>().FirstOrDefault(x => x.name == "AnswerC");
        answerC.GetComponentInChildren<Text>().text = question.Wrong2;
        
        var answerD = FindObjectsOfType<Button>().FirstOrDefault(x => x.name == "AnswerD");
        answerD.GetComponentInChildren<Text>().text = question.Wrong3;
        */
    }
}
