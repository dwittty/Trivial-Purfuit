using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;

public class RuleController : MonoBehaviour
{
    //private variables
    private int _numUsers=2;
    private int _userNumber;
    private string _userName;
    private int _userLocationX;
    private int _userLocationY;
    private int _locationColor;
    private int _selectedAnswer;

    private List<Player> _turnOrder;

    private RandomNumberGeneratorDice _rng;
    private QuestionDatabase _qdb;
    private CakeSquareStatus _css;
    private bool _winnerExist;
    
    private static int _currentPlayersTurn;

    public RuleController()
    {
        //generate instances
        _rng = new RandomNumberGeneratorDice();     
        _qdb = new QuestionDatabase();
        _css = new CakeSquareStatus(_numUsers);
        _winnerExist = false;
        _currentPlayersTurn = 1;
        
    }

    public enum PlayerList
    {
        Player1 = 1,
        Player2 = 2, 
        Player3 = 3,
        Player4 = 4
    }

    enum LocationColor
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
        Debug.Log($"RuleController asks RandomNumberGenerator to provide dice number.");
        return _rng.rollDice();
    }

    public string sendQuestion()
    {
        Debug.Log($"RuleController asks QuestionDB to update question and answer.");
        return _qdb.GetQuestion(Category.RED).Prompt;
    }

    public string[] sendMultipleChoice()
    {
        return new string[0];//_qdb.getMultipleChoice();
    }

    public bool checkAnswer(int input)
    {
        if (input == 0/*_qdb.getAnswer()*/)
        {
            _css.setStatus(_userNumber, _locationColor, true);
            return true;
        }
        else
        {
            Debug.Log($"Rule controller notifies Wrong answer. Turn over");
            return false;
        }

    }

    public void receiveAnswer(int answer)
    {
        _selectedAnswer = answer;
    }


    public int getAnswer()
    {
        Debug.Log($"Rule controller is notified that User selected {_selectedAnswer} as an answer.");
        return _selectedAnswer;
    }


    public bool sendUpdatedCakeSquare()
    {
        Debug.Log($"RuleController asks cake square to update status.");
        return _css.getStatus(_userNumber, _locationColor);
    }


    public List<Player> getTurnOrder()
    {
        return _turnOrder;
    }


    public void setTurnOrder(List<Player> turnOrder)
    {
        _turnOrder = turnOrder.ToList();
        
        _numUsers = _turnOrder.Count;
        for (int i = 0; i < _numUsers; i++)
        {
            Debug.Log($"{_turnOrder.ElementAt(i).getPlayerName()} got dice number {_turnOrder.ElementAt(i).getDiceNum()}");

        }
    }



    public void winnerQuestion(int inputCategory)
    {
        //_qdb.updateCategory(inputCategory);
        _qdb.UpdateQuestionSet();
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

    public static int CurrentTurn()
    {
        return _currentPlayersTurn;
    }

}
