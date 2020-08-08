using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RuleController : MonoBehaviour
{
    //private variables
    
    private int _userNumber;
    private string _userName;
    private float _userLocationX;
    private float _userLocationY;
    private int _locationColor;
    private int _selectedAnswer;

    private List<Player> _turnOrder;

    private RandomNumberGeneratorDice _rng;
    private QuestionDatabase _qdb;
    private Question _currentQuestion;
    private CakeSquareStatus _css;
    private bool _winnerExist;
    private GameObject _timer;

    private int _currentPlayersTurn=1;
    private int _numUsers = 2;

    public RuleController()
    {
        //generate instances
        _rng = new RandomNumberGeneratorDice();     
        //_qdb = new QuestionDatabase(); //moved to Start because you can't call Load() in the constructor of a monobehavior
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

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;  
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "PlayGame")
        {
            _timer = GameObject.FindGameObjectWithTag("Timer");
            DisableAndResetTimer();            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _qdb = new QuestionDatabase();        
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


    public void receiveUserLocation(float locationX,float locationY)
    {
        _userLocationX = locationX;
        _userLocationY = locationY;
    }

    public float[] sendUserLocation()
    {
        float[] temp = {_userLocationX,_userLocationY };
        
        return temp;
    }

    public int rollDice()
    {
        Debug.Log($"RuleController asks RandomNumberGenerator to provide dice number.");
        return _rng.rollDice();
    }

    internal void PromptUserForColorSelection()
    {
        ActivateCategorySelectGroup(true);
    }

    internal void RollAgain()
    {        
        DisplayMessage(5, "ROLL AGAIN!", new Color32(11, 137, 11, 255));
        EnableDiceButton();        
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

        //activate the group first or the DisplayQuestion method wont be able to find the GameObjects
        SetAndActivateTimer();
        ActivateQuestionAnswerGroup(true);       

        questionDisplay.DisplayQuestion(_currentQuestion);

    }


    public string[] sendMultipleChoice()
    {
        return new string[0];//_qdb.getMultipleChoice();
    }

    public void CheckAnswer(string selectedAnswer)
    {
        DisableAndResetTimer(); //question was answered, right or wrong, end the timer countdown
        
        if (string.Equals(selectedAnswer, _currentQuestion.Correct))
        {
            float[] temp=sendUserLocation();
            //var playerTokens = FindObjectsOfType<PlayerToken>();
            //var activeToken = playerTokens.FirstOrDefault(x => x.name == "Player" + _currentPlayersTurn);
            //Debug.Log($"Current position is ({temp[0]},{temp[1]}). ");
            if (checkCenter() == true && checkFullCake() == true)
            {
                _winnerExist = true;
                Debug.Log($"winner is Player {_currentPlayersTurn}");
                SceneManager.LoadScene("Winner");
            }
            else
            {
                //Debug.Log($"Rule controller notifies Correct answer. Roll again.");
                DisplayMessage(5, "CORRECT", new Color32(11, 137, 11, 255));                
                DispenseCake();
            }
        }        
        else
        {
            Debug.Log($"Rule controller notifies Wrong answer. Turn over");
            DisplayMessage(5, "INCORRECT", new Color32(205, 42, 44, 255));            
            EndTurn();            
        }
        //Enable dice, this will either continue current turn if they were correct or allow the next player to roll if they were wrong
        EnableDiceButton();

    }

    //checks if the player who just answered correctly is on a cake square, if he is, gives that player cake of the appropriate color
    private void DispenseCake()
    {
        var playerTokens = FindObjectsOfType<PlayerToken>();
        var activeToken = playerTokens.FirstOrDefault(x => x.name == "Player" + _currentPlayersTurn);
        if (activeToken.CurrentTile.IsCake)
        {
            var cakeColor = activeToken.CurrentTile.color;
            var playerObject = activeToken.GetComponentInParent<Player>();
            playerObject.UpdateCakeState(cakeColor);
        }
    }


    private bool checkFullCake()
    {
        var playerTokens = FindObjectsOfType<PlayerToken>();
        var activeToken = playerTokens.FirstOrDefault(x => x.name == "Player" + _currentPlayersTurn);
        
        var playerObject = activeToken.GetComponentInParent<Player>();
        if (playerObject.hasRedCake == true && playerObject.hasBlueCake == true && playerObject.hasWhiteCake == true && playerObject.hasGreenCake == true)
            return true;
        else
            return false;

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
        ActivateQuestionAnswerGroup(false);
        EnableDiceButton();
        DisableAndResetTimer();
    }


    public void winnerQuestion(int inputCategory)
    {
        //_qdb.updateCategory(inputCategory);
              
        _qdb.UpdateQuestionSet();
        //sendQuestion(); //this will be assigned to the object in front-end instance
        sendMultipleChoice(); //this will be assigned to the object in front-end instance

    }



    //function decide winner
    public bool winnerDecision()
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


    public bool checkCenter()
    {        
        var currentPlayerToken = FindObjectsOfType<PlayerToken>().FirstOrDefault(x => x.name == "Player" + _currentPlayersTurn) ?? new PlayerToken();
        //if (_userLocationX>-1.14 && _userLocationX<1.37 && _userLocationY>-1.26 && _userLocationY<1.25)
        if (currentPlayerToken.CurrentTile.CheckIfTileIsStart()==true)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    //wrapper for displaying a message
    public void DisplayMessage(float delay, string message, Color32 color)
    {
        var rollAgainDisplay = FindObjectOfType<MessageDisplay>();
        StartCoroutine(rollAgainDisplay.ShowMessage(delay, message, color));
    }
    

    #region Helper Methods
    public static GameObject FindObject(GameObject parent, string name)
    {
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }
    
    public void ActivateQuestionAnswerGroup(bool activate)
    {
        //need to find inactive objects using the parent because the FindObjectOfType method won't find inactive objects
        var canvas = FindObjectsOfType<Canvas>().FirstOrDefault(x => x.name == "Canvas");
        var questionAnswerGroup = FindObject(canvas.gameObject, "QuestionAnswerGroup");
        questionAnswerGroup.SetActive(activate);
    }

    public void ActivateCategorySelectGroup(bool activate)
    {
        //need to find inactive objects using the parent because the FindObjectOfType method won't find inactive objects
        var canvas = FindObjectsOfType<Canvas>().FirstOrDefault(x => x.name == "Canvas");
        var questionAnswerGroup = FindObject(canvas.gameObject, "CategorySelectGroup");
        questionAnswerGroup.SetActive(activate);
    }


    private void SetAndActivateTimer()
    {
        _timer.SetActive(true);
        _timer.GetComponent<TurnTimer>().timeRemaining = 30;
        _timer.GetComponent<TurnTimer>().timerRunning = true;
    }

    public void DisableAndResetTimer()
    {               
        _timer.SetActive(false);
        _timer.GetComponent<TurnTimer>().timeRemaining = 30;
        _timer.GetComponent<TurnTimer>().timerRunning = true; //setting timerRunning to true so it is ready to start running on the next frame the next time setActive(true) happens.

    }

    public void EnableDiceButton()
    {
        var diceObject = FindObjectOfType<DiceRoller>();
        diceObject.EnableRollDiceButton();
    }

    #endregion

}
