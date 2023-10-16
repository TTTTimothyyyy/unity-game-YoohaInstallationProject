using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DatingAppChat2 : MonoBehaviour
{
    [Header("References")]
    // Classes:
    public ChapterManager chapterManager;
    public SoundManager soundManager;



    // UI Elements:

    public GameObject backgroundOptionImage;

    [SerializeField] private Transform _chatTransform;
    // Prefabs:
    [SerializeField] private GameObject _playerMessagePrefab;
    private Text _playerMessageText;
    [SerializeField] private GameObject _yoohaMessagePrefab;
    private Text _yoohaMessageText;
    [SerializeField] private GameObject _lastActivePrefab;
    private Text _lastActiveText;
    // Option Buttons:
    public Button optionOneButton;
    public Button optionTwoButton;
    public Button optionThreeButton;
    // Options Text:
    [SerializeField] private Text _optionOneText;
    [SerializeField] private Text _optionTwoText;
    [SerializeField] private Text _optionThreeText;
    // Options Backgrounds:
    [SerializeField] private Image _optionOneBG;
    [SerializeField] private Image _optionTwoBG;
    [SerializeField] private Image _optionThreeBG;
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
    private bool _ySpammed;

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
        soundManager = FindObjectOfType<SoundManager>();

        // Values:

        maxParts = 0;
        currentPart = -1;
        optionSelected = 0;

        _yResponded = true;
        _playerResponded = true;
        _ySpammed = false;

        maxReplyDuration = 16f;
        currentReplyDuration = maxReplyDuration;
        canCountDown = false;

        // UI Elements:
        //Countdown:
        timeLeftText.text = ((int)currentReplyDuration).ToString();

        optionOneButton.interactable = true;
        optionTwoButton.interactable = true;
        optionThreeButton.interactable = true;

        _optionOneText.gameObject.SetActive(true);
        _optionTwoText.gameObject.SetActive(true);
        _optionThreeText.gameObject.SetActive(true);

        _optionOneBG.gameObject.SetActive(true);
        _optionTwoBG.gameObject.SetActive(true);
        _optionThreeBG.gameObject.SetActive(true);

        // Fade:
        fadeImage.gameObject.SetActive(true);
        _currentAlpha = fadeImage.color.a; // Get the initial alpha value of the image
        _fadeTimer = _fadeDuration; // Set the fade timer to the duration
        _canFade = true;

        backgroundOptionImage.SetActive(true);
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
            optionThreeButton.gameObject.SetActive(true);
            // Enable the button text:
            _optionOneText.gameObject.SetActive(true);
            _optionTwoText.gameObject.SetActive(true);
            _optionThreeText.gameObject.SetActive(true);
            // Enable the button Backgrounds:
            _optionOneBG.gameObject.SetActive(true);
            _optionTwoBG.gameObject.SetActive(true);
            _optionThreeBG.gameObject.SetActive(true);
        }
        else if (!_yResponded)
        {
            // Disable the choice buttons
            optionOneButton.gameObject.SetActive(false);
            optionTwoButton.gameObject.SetActive(false);
            optionThreeButton.gameObject.SetActive(false);
            // Disable the button text:
            _optionOneText.gameObject.SetActive(false);
            _optionTwoText.gameObject.SetActive(false);
            _optionThreeText.gameObject.SetActive(false);
            // Disable the button Backgrounds:
            _optionOneBG.gameObject.SetActive(false);
            _optionTwoBG.gameObject.SetActive(false);
            _optionThreeBG.gameObject.SetActive(false);
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
            soundManager.PlayMessageToneClip();
            StartCoroutine(YResponseCoroutine(timeUntilYoohaResponds - timeUntilYoohaResponds, "I'm home, had a really good time tonight (^-^).", "Gn, hope you dream of me haha.", "Me too, ur rlly cute :0.", "*Likes message*."));

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
                    PlayerSendMessageInstantiate("Me too :). Gn, hope you dream of me haha.");
                    _playerResponded = true;
                }
                // Load Next Part:
                LoadNextPart();
            }
            if (optionSelected == 2)
            {
                _yResponded = false;

                if (!_playerResponded)
                {
                    PlayerSendMessageInstantiate("Me too, ur rlly cute :).");
                    _playerResponded = true;
                }
                // Load Next Part:
                LoadNextPart();
            }
            if (optionSelected == 3)
            {
                _yResponded = false;

                if (!_playerResponded)
                {
                    PlayerSendMessageInstantiate("*Likes message*");
                    _playerResponded = true;
                }
                // Load Next Part:
                LoadNextPart();
            }
        }

        if (currentPart == 2) // Part Two of Chapter One - Setting up:
        {
            _yResponded = false;
            // Start Spam:
            if (!_ySpammed)
            {
                backgroundOptionImage.SetActive(false);

                StartCoroutine(AutomaticSpam());
                _ySpammed = true;
            }
        }

        if (currentPart == 3) // Part Two of Chapter One - Setting up:
        {
            _yResponded = false;
            // Set Dialogue to Y response text if Option 1:
            StartCoroutine(EndChapterTransition());
        }
    }

    private void LoadNextChapter()
    {
        chapterManager.currentChapter++;
        //currentPart = 0;
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

    private IEnumerator YResponseCoroutine(int time, string yoohaResponse, string optionOneText, string optionTwoText, string optionThreeText)
    {
        yield return new WaitForSeconds(time);

        // Run Yooha Response:
        YoohaSendMessageInstantiate(yoohaResponse);
        _yResponded = true;

        // Set Option Buttons Text Based off response:
        _optionOneText.text = optionOneText;
        _optionTwoText.text = optionTwoText;
        _optionThreeText.text = optionThreeText;

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
        GameObject instantiatedYoohaMessage = Instantiate(_yoohaMessagePrefab, _chatTransform);
        // set instantiated text to message:
        Text messageText = instantiatedYoohaMessage.GetComponentInChildren<Text>();
        messageText.text = message;
    }

    private void LastActiveInstantiate(string message)
    {
        //Instantiate Yooha Message:
        GameObject instantiatedLastActiveMessage = Instantiate(_lastActivePrefab, _chatTransform);
        // set instantiated text to message:
        Text messageText = instantiatedLastActiveMessage.GetComponentInChildren<Text>();
        messageText.text = message;
    }

    private IEnumerator AutomaticSpam()
    {
        yield return new WaitForSeconds(5f);
        // Last Time Active Message - "The Day After":
        soundManager.PlayMessageToneClip();
        LastActiveInstantiate("One day later");

        // Auto Yooha Message - "Good Morning how are you doing today?":
        soundManager.PlayMessageToneClip();
        YoohaSendMessageInstantiate("Good Morning how are you doing today?");

        yield return new WaitForSeconds(5f);
        // Last Time Active Message - "2 Days Later":
        LastActiveInstantiate("2 days later");

        // Auto Yooha Message - "I'm hoping for a second date :).":
        soundManager.PlayMessageToneClip();
        YoohaSendMessageInstantiate("I'm hoping for a second date :).");

        yield return new WaitForSeconds(5f);
        // Last Time Active Message - "5 Days Later":
        LastActiveInstantiate("5 days later");

        // Auto Yooha Message - "Are you busy?":
        soundManager.PlayMessageToneClip();
        YoohaSendMessageInstantiate("Are you busy?");

        yield return new WaitForSeconds(5f);
        // Last Time Active Message - "6 Days Later":
        LastActiveInstantiate("6 days later");

        // Auto Yooha Message - "Hellow?":
        soundManager.PlayMessageToneClip();
        YoohaSendMessageInstantiate("Hello?");

        yield return new WaitForSeconds(5f);

        // Last Time Active Message - "7 Days Later":
        LastActiveInstantiate("7 days later");

        // Auto Yooha Message - "Hey":
        soundManager.PlayMessageToneClip();
        YoohaSendMessageInstantiate("Hey");

        yield return new WaitForSeconds(2f);

        // Last Time Active Message - "8 Days Later":
        LastActiveInstantiate("8 days later");

        // Auto Yooha Message - "Hey":
        soundManager.PlayMessageToneClip();
        YoohaSendMessageInstantiate("Hey");

        yield return new WaitForSeconds(2f);

        // Last Time Active Message - "9 Days Later":
        LastActiveInstantiate("9 days later");

        // Auto Yooha Message - "Hey":
        YoohaSendMessageInstantiate("Hey");

        yield return new WaitForSeconds(2f);

        // Last Time Active Message - "10 Days Later":
        LastActiveInstantiate("10 days later");

        // Auto Yooha Message - "Hey":
        soundManager.PlayMessageToneClip();
        YoohaSendMessageInstantiate("Hey");

        yield return new WaitForSeconds(2f);

        // Last Time Active Message - "11 Days Later":
        LastActiveInstantiate("11 days later");

        // Auto Yooha Message - "Hey":
        soundManager.PlayMessageToneClip();
        YoohaSendMessageInstantiate("Hey");

        yield return new WaitForSeconds(2f);

        // Last Time Active Message - "12 Days Later":
        LastActiveInstantiate("12 days later");

        // Auto Yooha Message - "Hey":
        soundManager.PlayMessageToneClip();
        YoohaSendMessageInstantiate("Hey");

        yield return new WaitForSeconds(2f);

        // Last Time Active Message - "13 Days Later":
        LastActiveInstantiate("13 days later");

        // Auto Yooha Message - "Hey":
        soundManager.PlayMessageToneClip();
        YoohaSendMessageInstantiate("Hey");

        yield return new WaitForSeconds(2f);

        // Last Time Active Message - "14 Days Later":
        LastActiveInstantiate("14 days later");

        // Auto Yooha Message - "Hey":
        soundManager.PlayMessageToneClip();
        YoohaSendMessageInstantiate("Hey");
        currentPart = 3;
    }

    //
    private IEnumerator Transition(int time)
    {
        yield return new WaitForSeconds(time);
    }

    private IEnumerator EndChapterTransition()
    {
        yield return new WaitForSeconds(2f);
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
