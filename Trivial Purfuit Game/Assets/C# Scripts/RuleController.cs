using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class RuleController : MonoBehaviour
{
    //private variables
    
    private int _userNumber;
    private string _userName;
    private int _userLocationX;
    private int _userLocationY;
    private int _locationColor;
    private int _selectedAnswer;

    private List<Player> _turnOrder;

    private RandomNumberGeneratorDice _rng;
    private QuestionDatabase _qdb;
    private Question _currentQuestion;
    private CakeSquareStatus _css;
    private bool _winnerExist;
    
    private static int _currentPlayersTurn=1;
    private static int _numUsers = 2;

    public RuleController()
    {
        //generate instances
        _rng = new RandomNumberGeneratorDice();     
        _qdb = new QuestionDatabase();
        _css = new CakeSquareStatus(_numUsers);
        _winnerExist = false;               
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


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
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

    public void SetNumberOfPlayers(int playerNumber)
    {
        _userNumber = playerNumber;
    }

    public int GetNumberOfPlayers()
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

    //internal static void GetTriviaQuestion(Tile currentTile)
    //{
    //    var color = currentTile.color;
    //    Category category = (Category)Enum.Parse(typeof(Category), color, true);

    //    Question newQuestion = _qdb.GetQuestion(Category.RED).Prompt;
    //}

    public void GetAndDisplayNewTriviaQuestion(string color)
    {
        Debug.Log($"RuleController asks QuestionDB to display trivia question and answer choices.");
        
        if (color.ToUpper() == "RED")
        {
            _currentQuestion = _qdb.GetQuestion(Category.RED);
        }
        else if (color.ToUpper() == "BLUE")
        {
            _currentQuestion = _qdb.GetQuestion(Category.BLUE);
        }
        else if (color.ToUpper() == "GREEN")
        {
            _currentQuestion = _qdb.GetQuestion(Category.GREEN);
        }
        else 
        {
            _currentQuestion = _qdb.GetQuestion(Category.WHITE);
        }

        var questionText = FindObjectsOfType<Text>().FirstOrDefault(x => x.name == "Question");
        questionText.text = _currentQuestion.Prompt;
        var answerA = FindObjectsOfType<Button>().FirstOrDefault(x => x.name == "AnswerA");
        answerA.GetComponentInChildren<Text>().text = _currentQuestion.Correct;
        var answerB = FindObjectsOfType<Button>().FirstOrDefault(x => x.name == "AnswerB");
        answerB.GetComponentInChildren<Text>().text = _currentQuestion.Wrong1;
        var answerC = FindObjectsOfType<Button>().FirstOrDefault(x => x.name == "AnswerC");
        answerC.GetComponentInChildren<Text>().text = _currentQuestion.Wrong2;
        var answerD = FindObjectsOfType<Button>().FirstOrDefault(x => x.name == "AnswerD");
        answerD.GetComponentInChildren<Text>().text = _currentQuestion.Wrong3;

    }

    public string[] sendMultipleChoice()
    {
        return new string[0];//_qdb.getMultipleChoice();
    }

    public bool CheckAnswer(string selectedAnswer)
    {
        if(string.Equals(selectedAnswer, _currentQuestion.Correct))
        {
            Debug.Log($"Rule controller notifies Correct answer. Roll again.");
            return true;
            //TODO: call something to give cake (if on a cake square), and let user know they can continue their turn
        }
        //if (input == 0/*_qdb.getAnswer()*/)
        //{
        //    _css.setStatus(_userNumber, _locationColor, true);
        //    return true;
        //}
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

    public static int CurrentTurn()
    {
        return _currentPlayersTurn;
    }

    public static void SetCurrentTurn(int turn)
    {
        _currentPlayersTurn = turn;
    }

    public static void EndTurn()
    {
        _currentPlayersTurn = ((_currentPlayersTurn + 1) % _numUsers) + 1; //increment turn
    }


    public void winnerQuestion(int inputCategory)
    {
        //_qdb.updateCategory(inputCategory);
              
        _qdb.UpdateQuestionSet();
        //sendQuestion(); //this will be assigned to the object in front-end instance
        sendMultipleChoice(); //this will be assigned to the object in front-end instance

    }



    //function decide winner
    public bool winnerDecision(int input)
    {
        if (_css.isFull(_userNumber) && _userLocationX==0 && _userLocationY==0)
        {
            if (CheckAnswer(""))
            {
                _winnerExist = true;
            }
        }
        return _winnerExist;
    }
}
