using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public class QuestionDatabase
{
    
    private QuestionCategory[] Questions;
    private static readonly string ResourcePath = "TextFiles/questions";
    private TextAsset questionFile;

    public QuestionDatabase(string filepath)
    {
        questionFile = Resources.Load(ResourcePath, typeof(TextAsset)) as TextAsset;

        Questions = new QuestionCategory[4];
        Questions[0] = new QuestionCategory(Category.RED);
        Questions[1] = new QuestionCategory(Category.GREEN);
        Questions[2] = new QuestionCategory(Category.BLUE);
        Questions[3] = new QuestionCategory(Category.WHITE);

        //LoadQuestions(filepath);
        LoadQuestions();

        Questions[0].Shuffle();
        Questions[1].Shuffle();
        Questions[2].Shuffle();
        Questions[3].Shuffle();
    }

    public QuestionDatabase() : this(ResourcePath)
    { 
       
    }

    // Read/parse CSV text asset read from file in Resources and load the questions into their respective
    // categories.
    private void LoadQuestions()
    {
        // use CSV parser library to read file
        //fgCSVReader.LoadFromFile(filepath, new fgCSVReader.ReadLineDelegate(ReadLineIntoQuestion));
        fgCSVReader.LoadFromTextAsset(questionFile, new fgCSVReader.ReadLineDelegate(ReadLineIntoQuestion));
    }

    private void ReadLineIntoQuestion(int line_index, List<string> vals)
    {
        if (vals.Count < 6) // skip any line with less than 6 fields
        {
            return;
        }

        Category cat;
        switch (vals[0].ToUpper())
        {
            case "RED":
                cat = Category.RED;
                break;
            case "GREEN":
                cat = Category.GREEN;
                break;
            case "BLUE":
                cat = Category.BLUE;
                break;
            case "WHITE":
                cat = Category.WHITE;
                break;
            default:
                cat = Category.RED;
                break;
        }

        Question q = new Question(cat, vals[1], vals[2], vals[3], vals[4], vals[5]);
        Questions[(int)cat].AddQuestion(q);
    }
    
    //public functions - will be called by Rule Controller
 
    public Question GetQuestion(Category category)
    {
        return Questions[(int)category].Draw();
    }

    public void UpdateQuestionSet()
    {
        //this will update the Question, answer and multiple choices.
        //to be implemented
    }

}