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
    
    private int _currentPlayersTurn=1;
    private int _numUsers = 2;

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
        var questionDisplay = new QuestionDisplay();
        _currentQuestion = questionDisplay.GetNewQuestion(color, _qdb);
        questionDisplay.DisplayQuestion(_currentQuestion);
    }

    public string[] sendMultipleChoice()
    {
        return new string[0];//_qdb.getMultipleChoice();
    }

    public void CheckAnswer(string selectedAnswer)
    {
        if(string.Equals(selectedAnswer, _currentQuestion.Correct))
        {
            Debug.Log($"Rule controller notifies Correct answer. Roll again.");
            var correctnessDisplay = FindObjectOfType<AnswerCorrectnessDisplay>();            
            StartCoroutine(correctnessDisplay.ShowMessage(true, 5));
            DispenseCake();                       
        }        
        else
        {
            Debug.Log($"Rule controller notifies Wrong answer. Turn over");
            var correctnessDisplay = FindObjectOfType<AnswerCorrectnessDisplay>();
            StartCoroutine(correctnessDisplay.ShowMessage(false, 5));
            EndTurn();            
        }
        //Enable dice, this will either continue current turn if they were correct or allow the next player to roll if they were wrong
        var diceObject = FindObjectOfType<RollDice>();
        diceObject.EnableRollDiceButton();

    }

    //checks if the player who just answered correctly is on a cake square, if he is, gives that player cake of the appropriate color
    private void DispenseCake()
    {
        var playerTokens = FindObjectsOfType<PlayerToken>();
        var activeToken = playerTokens.FirstOrDefault(x => x.name == "Player" + _currentPlayersTurn);
        if (activeToken.CurrentTile.isCake)
        {
            var cakeColor = activeToken.CurrentTile.color;
            var playerObject = activeToken.GetComponentInParent<Player>();
            playerObject.UpdateCakeState(cakeColor);
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

    public int GetCurrentTurn()
    {
        return _currentPlayersTurn;
    }

    public void SetCurrentTurn(int turn)
    {
        _currentPlayersTurn = turn;
    }

    public void EndTurn()
    {
        _currentPlayersTurn = ((_currentPlayersTurn) % _numUsers) + 1; //increment turn
        var turnBanner = FindObjectOfType<TurnBanner>();
        turnBanner.UpdateTurnBanner(_currentPlayersTurn.ToString());
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
            //if (CheckAnswer(""))
            {
                _winnerExist = true;
            }
        }
        return _winnerExist;
    }
}
