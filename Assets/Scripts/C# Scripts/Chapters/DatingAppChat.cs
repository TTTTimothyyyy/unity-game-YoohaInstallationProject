using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DatingAppChat : MonoBehaviour
{
    [Header("References")]
    // Classes:
    public ChapterManager chapterManager;
    


    // UI Elements:

    [SerializeField] private Transform _chatTransform;
    // Prefabs:
    [SerializeField] private GameObject _playerMessagePrefab;
    private Text _playerMessageText;
    [SerializeField] private GameObject _yoohaMessagePrefab;
    private Text _yoohaMessageText;
    // Option Buttons:
    public Button optionOneButton;
    public Button optionTwoButton;
    // Options Text:
    [SerializeField] private Text _optionOneText;
    [SerializeField] private Text _optionTwoText;
    // Options Backgrounds:
    [SerializeField] private Image _optionOneBG;
    [SerializeField] private Image _optionTwoBG;
    // Timer:
    public Text timeLeftText;
    public Image timeLeftImageOne;
    public Image timeLeftImageTwo;
    // Fade:
    public Image fadeImage;

    [Header("Varaibles")]
    // Message Text:
    public string playerMessage = "";
    public string yoohaMessage = "";
    // Parts
    public int maxParts;
    public int currentPart;
    public int optionSelected;

    private int timeUntilYoohaResponds = 5; // seconds

    private bool _yResponded;
    private bool _playerResponded;

    // Countdown:
    public float maxReplyDuration = 16f;
    public float currentReplyDuration;
    public bool canCountDown;

    // Fade:
    private float _fadeTimer;
    private float _currentAlpha;
    private float _fadeDuration = 2f;
    [SerializeField] private bool _canFade;



    void Start()
    {
        // Classes:
        chapterManager = FindObjectOfType<ChapterManager>();

        // Values:

        maxParts = 0;
        currentPart = -1;
        optionSelected = 0;

        _yResponded = true;
        _playerResponded = true;

        maxReplyDuration = 16f;
        currentReplyDuration = maxReplyDuration;
        canCountDown = false;

        // UI Elements:
        //Countdown:
        timeLeftText.text = ((int)currentReplyDuration).ToString();

        optionOneButton.interactable = true;
        optionTwoButton.interactable = true;

        _optionOneText.gameObject.SetActive(true);
        _optionTwoText.gameObject.SetActive(true);

        _optionOneBG.gameObject.SetActive(true);
        _optionTwoBG.gameObject.SetActive(true);

        // Fade:
        fadeImage.gameObject.SetActive(true);
        _currentAlpha = fadeImage.color.a; // Get the initial alpha value of the image
        _fadeTimer = _fadeDuration; // Set the fade timer to the duration
        _canFade = true;
    }

    void Update()
    {

        //AutoChooser(); // Auto Chooser.

        // Count Down:
        if (optionSelected == 0 && _yResponded)
        {
            CountDownTimeUI();
        }
        else if (optionSelected > 0)
        {
            ResetCountDownTime();
        }

        // Y Responds:
        if (_yResponded)
        {
            // Enable the choice buttons
            optionOneButton.gameObject.SetActive(true);
            optionTwoButton.gameObject.SetActive(true);
            // Enable the button text:
            _optionOneText.gameObject.SetActive(true);
            _optionTwoText.gameObject.SetActive(true);
            // Enable the button Backgrounds:
            _optionOneBG.gameObject.SetActive(true);
            _optionTwoBG.gameObject.SetActive(true);
        }
        else if (!_yResponded)
        {
            // Disable the choice buttons
            optionOneButton.gameObject.SetActive(false);
            optionTwoButton.gameObject.SetActive(false);
            // Disable the button text:
            _optionOneText.gameObject.SetActive(false);
            _optionTwoText.gameObject.SetActive(false);
            // Disable the button Backgrounds:
            _optionOneBG.gameObject.SetActive(false);
            _optionTwoBG.gameObject.SetActive(false);
        }

        PartManager();

        print("Chapter: " + chapterManager.currentChapter.ToString() + " - Part: " + currentPart.ToString());
    }

    private void PartManager()
    {
        maxParts = 4;

        if (currentPart == -1)
        {
            fadeImage.gameObject.SetActive(true);
            _fadeTimer -= Time.deltaTime;
            float newAlpha = Mathf.Lerp(0f, _currentAlpha, _fadeTimer / _fadeDuration);
            // Set Values for logic:
            Color newColor = fadeImage.color;
            newColor.a = newAlpha;
            fadeImage.color = newColor;
            // disable once complete:
            if (_fadeTimer <= 0f)
            {
                fadeImage.gameObject.SetActive(false);
                fadeImage.color = Color.white;
                fadeImage.color.a.Equals(1);

                currentPart = 0;
            }
        }

        if (currentPart == 0) // Part Zero of Chapter One - Setting up Bumble Chat (App Chat):
        {
            // Set Dialogue to Y response text if Option 1:
            StartCoroutine(YResponseCoroutine(timeUntilYoohaResponds - timeUntilYoohaResponds, "Omgg, you're so pretty!!", "Pretty obsessed with you already ;)", "uhmmm, do I know you or?"));

            // Load Next Part:
            LoadNextPart();
        }

        if (currentPart == 1) // Part One of Chapter One - :
        {
            _playerResponded = false;

            if (optionSelected == 1)
            {
                _yResponded = false;

                if (!_playerResponded)
                {
                    PlayerSendMessageInstantiate("Pretty obsessed with you already ;)");
                    _playerResponded = true;
                }
                // Yooha Generate Yooha Response Based on Option Chosen:
                StartCoroutine(YResponseCoroutine(timeUntilYoohaResponds, "Hehe, pretty enough to take out on a date?", "I Can't Wait.", "Maybe I can get to know you..."));

                // Transition:
                StartCoroutine(Transition(1));

                // Load Next Part:
                LoadNextPart();
            }
            if (optionSelected == 2)
            {
                _yResponded = false;

                if (!_playerResponded)
                {
                    PlayerSendMessageInstantiate("Uhmmm, do I know you or?");
                    _playerResponded = true;
                }
                // Yooha Generate Yooha Response Based on Option Chosen:
                StartCoroutine(YResponseCoroutine(timeUntilYoohaResponds, "Not yet <3", "I Can't Wait.", "Maybe I can get to know you..."));

                // Transition:
                StartCoroutine(Transition(1));

                // Load Next Part:
                LoadNextPart();
            }
        }

        if (currentPart == 2) // Part Two of Chapter One - Setting up:
        {
            _playerResponded = false;

            if (optionSelected == 1)
            {
                _yResponded = false;

                if (!_playerResponded)
                {
                    PlayerSendMessageInstantiate("I Can't Wait, drinks tomorrow?");
                    _playerResponded = true;
                }
                // Yooha Generate Yooha Response Based on Option Chosen:
                StartCoroutine(YResponseCoroutine(timeUntilYoohaResponds, "Yes UwU", "", ""));

                // Load Next Part:
                LoadNextPart();
            }
            if (optionSelected == 2)
            {
                _yResponded = false;

                if (!_playerResponded)
                {
                    PlayerSendMessageInstantiate("Maybe I can get to know you... Over drinks tomorrow?");
                    _playerResponded = true;
                }
                // Yooha Generate Yooha Response Based on Option Chosen:
                StartCoroutine(YResponseCoroutine(timeUntilYoohaResponds, "Yes UwU", "ONTO", "THE NEXT"));

                // Load Next Part:
                LoadNextPart();
            }
        }

        if (currentPart == 3)
        {
            _yResponded = false;
            // Set Dialogue to Y response text if Option 1:
            StartCoroutine(EndChapterTransition());
        }
    }

    private void LoadNextChapter()
    {
        chapterManager.currentChapter++;
        currentPart = 0;
    }

    private void LoadNextPart()
    {
        ResetOptionVar(); // Reset Option Variable Value.
        currentPart++; // Incriment Part Value.
    }

    private void ResetOptionVar()
    {
        optionSelected = 0;
    }

    private IEnumerator YResponseCoroutine(int time,string yoohaResponse, string optionOneText, string optionTwoText)
    {
        yield return new WaitForSeconds(time);

        // Run Yooha Response:
        YoohaSendMessageInstantiate(yoohaResponse);
        _yResponded = true;

        // Set Option Buttons Text Based off response:
        _optionOneText.text = optionOneText;
        _optionTwoText.text = optionTwoText;

        ResetCountDownTime();
    }

    private void PlayerSendMessageInstantiate(string message)
    {
        //Instantiate Player Message:
        GameObject instantiatedPlayerMessage = Instantiate(_playerMessagePrefab, _chatTransform);
        // set instantiated text to message:
        Text messageText = instantiatedPlayerMessage.GetComponentInChildren<Text>();
        messageText.text = message;
    }

    private void YoohaSendMessageInstantiate(string message)
    {
        //Instantiate Yooha Message:
        GameObject instantiatedPlayerMessage = Instantiate(_yoohaMessagePrefab, _chatTransform);
        // set instantiated text to message:
        Text messageText = instantiatedPlayerMessage.GetComponentInChildren<Text>();
        messageText.text = message;
    }

    //
    private IEnumerator Transition(int time)
    {
        yield return new WaitForSeconds(time);
    }

    private IEnumerator EndChapterTransition()
    {
        yield return new WaitForSeconds(10f);
        LoadNextChapter();
    }

    public void OnOptionOneClicked() // Player clicks Option One:
    {
        optionSelected = 1;

        print("Option 1 Clicked");
    }

    public void OnOptionTwoClicked() // Player clicks Option Two:
    {
        optionSelected = 2;

        print("Option 2 Clicked");
    }

    public void OnOptionThreeClicked() // Player clicks Option Three:
    {
        optionSelected = 3;

        print("Option 3 Clicked");
    }

    private void CountDownTimeUI()
    {
        currentReplyDuration -= 1 * Time.deltaTime;

        timeLeftText.text = ((int)currentReplyDuration).ToString();

        float currentFillAmount = currentReplyDuration / maxReplyDuration;
        timeLeftImageOne.fillAmount = currentFillAmount;
        timeLeftImageTwo.fillAmount = currentFillAmount;

        if (currentReplyDuration <= 0)
        {
            AutoChooser();
            ResetCountDownTime();
        }
    }

    private void ResetCountDownTime()
    {
        currentReplyDuration = maxReplyDuration;
    }

    private void AutoChooser()
    {
        optionSelected = 1; // yooha automatically send a message:
        ResetCountDownTime();
    }
}
