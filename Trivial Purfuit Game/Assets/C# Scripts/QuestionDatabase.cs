using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionDatabase : MonoBehaviour
{
    // Start is called before the first frame update
    private string _question;
    private string[] _multipleChoice;
    private int _multipleChoiceSize = 4;
    private int _answer;
    
    private string _databaseFile = "QandA.csv";
    
    private int _category;

    public QuestionDatabase()       //constructor
    {
        loadFile();
        updateQuestionSet();
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
        return _question;
    }

    public string[] getMultipleChoice()
    {

        return _multipleChoice;
    }

    public int getAnswer()
    {

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
    }

    /// <summary>
    /// /////////////private function ////////////////////////////
    /// </summary>

    private void loadFile()
    {
        //this will load database file
        //to be implemented
    }
    

}
