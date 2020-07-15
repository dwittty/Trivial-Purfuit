using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public class QuestionDatabase
{
    // Start is called before the first frame update
    public List<Question> BlueQuestions { get; set; }
    public List<Question> RedQuestions { get; set; }
    public List<Question> GreenQuestions { get; set; }
    public List<Question> WhiteQuestions { get; set; }

    private List<Question> DiscardPile_BlueQuestions { get; set; }
    private List<Question> DiscardPile_RedQuestions { get; set; }
    private List<Question> DiscardPile_GreenQuestions { get; set; }
    private List<Question> DiscardPile_WhiteQuestions { get; set; }

    private string _question;
    private string[] _multipleChoice;
    private int _multipleChoiceSize = 4;
    private int _answer;

    string databasePath = "Assets/TextFiles/test.csv";
    private static System.Random random = new System.Random();

    private string _databaseFile = "QandA.csv";
    
    private int _category;

    public QuestionDatabase()       //constructor
    {
        //loadFile();
        //updateQuestionSet();
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //public functions - will be called by Rule Controller
 
    public string getQuestion()
    {
        Debug.Log($"QuestionDB updated the question and multiple choice.");
        _question = "Question";
        return _question;
    }

    public string[] getMultipleChoice()
    {

        return _multipleChoice;
    }

    public int getAnswer()
    {

        _answer = 0;
        return _answer;
    }


    public void updateQuestionSet()
    {
        //this will update the Question, answer and multiple choices.
        //to be implemented
    }


    public int updateCategory(int category)
    {
        _category = category;
        return _category;
    }

    /// <summary>
    /// /////////////private function ////////////////////////////
    /// </summary>
    private void loadFile()
    {

        using(StreamReader streamReader = new StreamReader(databasePath))
        {
            var questionInfo = new List<string>();
            string currentLine;
            while((currentLine = streamReader.ReadLine()) != null)
            {
                questionInfo = currentLine.Split(',')
                                   .ToList();
                ParseQuestionFromCSVFile(questionInfo);
            }
        }
        //this will load database file
        //to be implemented
    }

    //Breaks out the list from the CSV into its constituent parts, then makes call to add to QuestionDatabase
    private void ParseQuestionFromCSVFile(List<string> questionInfo)
    {
        string color = questionInfo[0];
        string question = questionInfo[1];
        string correctAnswer = questionInfo[2];
        List<string> answerChoices = new List<string> { questionInfo[2], questionInfo[3], questionInfo[4], questionInfo[5] };
        var newQuestion = new Question(question, correctAnswer, answerChoices);
        AddQuestionToQuestionDatabase(color, newQuestion);
    }

    //Determines which color database the question belongs to
    private void AddQuestionToQuestionDatabase(string color, Question newQuestion)
    {
        color = color.ToUpper();
        switch (color)
        {
            case "RED":
                RedQuestions.Add(newQuestion); break;
            case "BLUE":
                BlueQuestions.Add(newQuestion); break;
            case "GREEN":
                GreenQuestions.Add(newQuestion); break;
            case "WHITE":
                WhiteQuestions.Add(newQuestion); break;
            default:
                Debug.Log("This color is unknown."); break;

        }
    }

    //takes in a list of questions (redlist, bluelist etc) and shuffles the order of those questions 
    //so that the questions are delivered in a different order every game
    private void ShuffleQuestions(List<string> questions)
    {       
        int size = questions.Count;
        while(size > 1)
        {
            size--;
            int k = random.Next(size + 1);
            string value = questions[k];
            questions[k] = questions[size];
            questions[size] = value;
        }
    }
    

}
