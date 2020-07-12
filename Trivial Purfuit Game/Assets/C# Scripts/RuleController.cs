using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;

public class RuleController : MonoBehaviour
{
    //private variables
    private int _numUsers;
    private int _userNumber;
    private string _userName;
    private int _userLocationX;
    private int _userLocationY;
    private int _locationColor;
    private RandomNumberGeneratorDice _rng;
    private QuestionDatabase _qdb;
    private CakeSquareStatus _css;

    private bool _winnerExist;

    public RuleController()
    {
        //generate instances
        _rng = new RandomNumberGeneratorDice();     
        _qdb = new QuestionDatabase();
        _css = new CakeSquareStatus(_numUsers);
        _winnerExist = false;
    }


    enum locationColor
    {
        Red,
        Blue,
        Green,
        White

    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    // function communicate with front-end

    public void receiveUserNumber(int playerNumber)
    {
        _userNumber = playerNumber;
    }

    public int sendUserNumber()
    {
        return _userNumber;
    }

    public void receiveUserName(string userName)
    {
        _userName = userName;
    }

    public string sendUserName()
    {
        return _userName;
    }


    public void receiveUserLocation(int locationX,int locationY)
    {
        _userLocationX = locationX;
        _userLocationY = locationY;
    }

    public int[] sendUserLocation()
    {
        int[] temp = {_userLocationX,_userLocationY };
        
        return temp;
    }

    public int rollDice()
    {
        return _rng.rollDice();
    }

    public string sendQuestion()
    {
        return _qdb.getQuestion();
    }

    public string[] sendMultipleChoice()
    {
        return _qdb.getMultipleChoice();
    }

    public bool checkAnswer(int input)
    {
        if (input==_qdb.getAnswer())
        {
            _css.setStatus(_userNumber, _locationColor, true);
            return true;
        }
        else
        {
            return false;
        }

    }


    public bool sendUpdatedCakeSquare()
    {
        return _css.getStatus(_userNumber, _locationColor);
    }


    public void winnerQuestion(int inputCategory)
    {
        _qdb.updateCategory(inputCategory);
        _qdb.updateQuestionSet();
        sendQuestion(); //this will be assigned to the object in front-end instance
        sendMultipleChoice(); //this will be assigned to the object in front-end instance

    }



    //function decide winner
    public bool winnerDecision(int input)
    {
        if (_css.isFull(_userNumber) && _userLocationX==0 && _userLocationY==0)
        {
            if (checkAnswer(input))
            {
                _winnerExist = true;
            }
        }
        return _winnerExist;
    }
}
