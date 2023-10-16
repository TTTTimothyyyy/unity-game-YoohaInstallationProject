using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Date : MonoBehaviour
{
    [Header("References")]
    // Classes:
    public ChapterManager chapterManager;

    // UI Elements:
    [SerializeField] private Text _speakerNameText;

    [SerializeField] private Text _dialogueText;

    [SerializeField] private Text _optionOneText;
    [SerializeField] private Text _optionTwoText;
    [SerializeField] private Text _optionThreeText;
    // Option Buttons:
    public Button optionOneButton;
    public Button optionTwoButton;
    public Button optionThreeButton;
    // Timer:
    public Text timeLeftText;
    public Image timeLeftImageOne;
    public Image timeLeftImageTwo;
    // Fade:
    public Image fadeImage;
    public Image fadeImage2;

    [Header("Variables")]
    public int maxParts;
    public int currentPart;
    public int optionSelected;

    public string yoohaName = "Yooha";
    public string playerName;

    private int timeUntilYoohaResponds = 10; // seconds

    private bool _yResponded;

    // Countdown:
    public float maxReplyDuration;
    public float currentReplyDuration;
    public bool canCountDown;

    // Fade:
    private float _fadeTimer;
    private float _currentAlpha;
    private float _fadeDuration = 2f;
    private bool _canFade;

    private bool _can2ndFade;

    // Images of Yooha:
    public GameObject yoohaImage_1;
    public GameObject yoohaImage_2;
    public GameObject yoohaImage_3;
    public GameObject yoohaImage_4;
    public GameObject yoohaImage_5;
    public GameObject yoohaImage_6;
    public GameObject yoohaImage_7;
    public GameObject yoohaImage_8;
    public GameObject yoohaImage_9;



    void Start()
    {
        // Classes:
        chapterManager = FindObjectOfType<ChapterManager>();

        // Values:
        playerName = "You";

        maxParts = 0;
        currentPart = -1;
        optionSelected = 0;

        _yResponded = true;

        maxReplyDuration = 16f;
        currentReplyDuration = maxReplyDuration;
        canCountDown = false;

        // UI Elements:
        // Images of Yooha:
        yoohaImage_1.SetActive(false);
        yoohaImage_2.SetActive(false);
        yoohaImage_3.SetActive(false);
        yoohaImage_4.SetActive(false);
        yoohaImage_5.SetActive(false);
        yoohaImage_6.SetActive(false);
        yoohaImage_7.SetActive(false);
        yoohaImage_8.SetActive(false);
        yoohaImage_9.SetActive(false);

        //Countdown:
        timeLeftText.text = ((int)currentReplyDuration).ToString();

        optionOneButton.interactable = true;
        optionTwoButton.interactable = true;
        optionThreeButton.interactable = true;

        _optionOneText.gameObject.SetActive(true);
        _optionTwoText.gameObject.SetActive(true);
        _optionThreeText.gameObject.SetActive(true);

        // Fade:
        fadeImage.gameObject.SetActive(true);
        _currentAlpha = fadeImage.color.a; // Get the initial alpha value of the image
        _fadeTimer = _fadeDuration; // Set the fade timer to the duration
        _canFade = true;

        _can2ndFade = false;
    }

    void Update()
    {
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
        }

        SecondFade();

        PartManager();

        print("Chapter: " + chapterManager.currentChapter.ToString() + " - Part: " + currentPart.ToString());
    }


    private void PartManager()
    {
        maxParts = 7; // Set this to a value.

        if (currentPart == -1)
        {
            // Yooha Image One - Y: Hey, I'm here!:
            yoohaImage_1.SetActive(true);

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

                _currentAlpha = fadeImage2.color.a; // Get the initial alpha value of the image
                _fadeTimer = _fadeDuration; // Set the fade timer to the duration

                currentPart = 0;
            }
        }

        if (currentPart == 0) // Part Zero of Chapter Two - Setting up:
        {
            // Set Dialogue to Y response text if Option 1:
            StartCoroutine(TimeBetweenSpeakers(timeUntilYoohaResponds - timeUntilYoohaResponds, "Hey, I'm here!", yoohaName, "You look even more beautiful in real life.", "Nice to meet you.", "Damn I wouldn't have recognised you.", 1));

            // Load Next Part:
            LoadNextPart();
        }

        //
        // 0 >----- Screen Transition to Next Part -----> 1
        //

        // Option Button 1 Current Text = "You look even more beautiful in real life.".
        // Option Button 2 Current Text = "Nice to meet you.".
        // Option Button 3 Current Text = "Damn I wouldn't have recognised you.".

        else if (currentPart == 1) // Part One of Chapter Two:
        {
            // Run Logic Depending on Option:
            if (optionSelected == 1)
            {
                _yResponded = false; // Yooha has not responded yet.

                _speakerNameText.text = playerName; // Set Speaker's Name/

                _dialogueText.text = "Heyy, you look even more beautiful in real life."; // Set Dialogue to A option 1 text.

                // Set Dialogue to Y response text if Option 1:
                StartCoroutine(TimeBetweenSpeakers(timeUntilYoohaResponds, "Haha, did I surprise you? I like to do that sometimes.", yoohaName, "You definitely caught me off guard", "Wanna grab a seat over there?", "Yeah", 2));

                // Transition:
                StartCoroutine(Transition(1));

                // Load Next Part:
                LoadNextPart();
            }
            if (optionSelected == 2)
            {
                _yResponded = false; // Yooha has not responded yet.

                _speakerNameText.text = playerName; // Set Speaker's Name.

                _dialogueText.text = "Hey, nice to meet you."; // Set Dialogue to A option 2 text.

                // Set Dialogue to Y response text if Option 2:
                StartCoroutine(TimeBetweenSpeakers(timeUntilYoohaResponds, "Nice to meet you too!", yoohaName, "You definitely caught me off guard", "Wanna grab a seat over there?", "Yeah", 3));

                // Transition:
                StartCoroutine(Transition(1));

                // Load Next Part:
                LoadNextPart();
            }
            if (optionSelected == 3)
            {
                _yResponded = false; // Yooha has not responded yet.

                _speakerNameText.text = playerName; // Set Speaker's Name.

                _dialogueText.text = "Hey... damn I wouldn't have recognised you."; // Set Result If Option 2.

                // Set Dialogue to Y response text if Option 3. [Same as 1]:
                StartCoroutine(TimeBetweenSpeakers(timeUntilYoohaResponds, "Haha, did I surprise you? I like to do that sometimes.", yoohaName, "You definitely caught me off guard", "Wanna grab a seat over there?", "Yeah", 2));

                // Transition:
                StartCoroutine(Transition(1));

                // Load Next Part:
                LoadNextPart();
            }
        }

        //
        // 1 >----- Screen Transition to Next Part -----> 2
        //

        // Option Button 1 Current Text = "You definitely caught me off guard".
        // Option Button 2 Current Text = "Wanna grab a seat over there?".
        // Option Button 3 Current Text = "...".

        else if (currentPart == 2) // Part Two of Chapter Two:
        {
            // Run Logic Depending on Option:
            if (optionSelected == 1 || optionSelected == 3)
            {
                _yResponded = false; // Yooha has not responded yet.

                _speakerNameText.text = playerName; // Set Speaker's Name/

                _dialogueText.text = "Well, you definitely caught me off guard… *quivers* Wanna grab a seat over there?"; // Set Dialogue to A option 1 text.

                // Set Dialogue to Y response text if Option 1:
                StartCoroutine(TimeBetweenSpeakers(timeUntilYoohaResponds, "Sure. Do you come here a lot?", yoohaName, "Yeah", "No, not really.", "yes I do.", 5));

                // Transition:
                StartCoroutine(Transition(1));

                // Load Next Part:
                LoadNextPart();
            }
            if (optionSelected == 2)
            {
                _yResponded = false; // Yooha has not responded yet.

                _speakerNameText.text = playerName; // Set Speaker's Name/

                _dialogueText.text = "You wana grab a seat over there?"; // Set Dialogue to A option 1 text.

                // Set Dialogue to Y response text if Option 1:
                StartCoroutine(TimeBetweenSpeakers(timeUntilYoohaResponds, "Sure. Do you come here a lot?", yoohaName, "Yeah", "No, not really.", "yes I do.", 5));

                // Transition:
                StartCoroutine(Transition(1));

                // Load Next Part:
                LoadNextPart();
            }

        }

        //
        // 2 >----- Screen Transition to Next Part -----> 3
        //

        // Option Button 1 Current Text = "Yeah".
        // Option Button 2 Current Text = "No, not really."
        // Option Button 3 Current Text = "...".

        else if (currentPart == 3) // Part Three of Chapter Two:
        {
            // Run Logic Depending on Option:
            if (optionSelected == 1 || optionSelected == 3)
            {
                _yResponded = false; // Yooha has not responded yet.

                _speakerNameText.text = playerName; // Set Speaker's Name/

                _dialogueText.text = "Yeah, I thought I'd bring you to one of my favourite spots."; // Set Dialogue to A option 1 text.

                // Set Dialogue to Y response text if Option 1:
                StartCoroutine(TimeBetweenSpeakers(timeUntilYoohaResponds, "Oh, that's really nice. I can't believe that I have never heard about this place before", yoohaName, "Where do you usually go?", "Tell me about yourself.", "Let me guess your favourite drink.", 6));

                // Transition:
                StartCoroutine(Transition(1));

                // Load Next Part:
                LoadNextPart();
            }
            if (optionSelected == 2)
            {
                _yResponded = false; // Yooha has not responded yet.

                _speakerNameText.text = playerName; // Set Speaker's Name/

                _dialogueText.text = "No, not really. I like to try out new things"; // Set Dialogue to A option 1 text.

                // Set Dialogue to Y response text if Option 1:
                StartCoroutine(TimeBetweenSpeakers(timeUntilYoohaResponds, "Oh, that's really nice. I can't believe that I have never heard about this place before", yoohaName, "Where do you usually go?", "Tell me about yourself.", "Let me guess your favourite drink.", 6));

                // Transition:
                StartCoroutine(Transition(1));

                // Load Next Part:
                LoadNextPart();
            }
        }

        //
        // 3 >----- Screen Transition to Next Part -----> 4
        //

        // Option Button 1 Current Text = "Where do you usually go?".
        // Option Button 2 Current Text = "Tell me about yourself."
        // Option Button 3 Current Text = "Let me guess your favourite drink.".

        else if (currentPart == 4) // Part Four of Chapter Two:
        {
            StartCoroutine(TimePassingQuickCoroutine());
            // Run Logic Depending on Option:
            if (optionSelected == 1)
            {
                _yResponded = false; // Yooha has not responded yet.

                _speakerNameText.text = playerName; // Set Speaker's Name/

                _dialogueText.text = "So, where do you usually go?"; // Set Dialogue to A option 1 text.

                // Special Transition - Images fading in/out to show the time pass:
                StartCoroutine(SpecialTransition(3));

                // Set Dialogue to Y response text if Option 1:
                StartCoroutine(TimeBetweenSpeakers(timeUntilYoohaResponds, "Wow it's 1AM already? I really didn't see the time pass!", yoohaName, "Time flies when you're having fun.", "You've had quite a few drinks", "Yeah well you know.", 7));

                // Transition:
                StartCoroutine(Transition(1));

                // Load Next Part:
                LoadNextPart();
            }
            if (optionSelected == 2)
            {
                _yResponded = false; // Yooha has not responded yet.

                _speakerNameText.text = playerName; // Set Speaker's Name/

                _dialogueText.text = "So, tell me about yourself."; // Set Dialogue to A option 1 text.

                // Special Transition - Images fading in/out to show the time pass:
                StartCoroutine(SpecialTransition(3));

                // Set Dialogue to Y response text if Option 1:
                StartCoroutine(TimeBetweenSpeakers(timeUntilYoohaResponds, "Wow it's 1AM already? I really didn't see the time pass!", yoohaName, "Time flies when you're having fun.", "You've had quite a few drinks", "Yeah well you know.", 7));

                // Transition:
                StartCoroutine(Transition(1));

                // Load Next Part:
                LoadNextPart();
            }
            if (optionSelected == 3)
            {
                _yResponded = false; // Yooha has not responded yet.

                _speakerNameText.text = playerName; // Set Speaker's Name/

                _dialogueText.text = "Let me guess your favourite drink."; // Set Dialogue to A option 1 text.

                // Special Transition - Images fading in/out to show the time pass:
                StartCoroutine(SpecialTransition(3));

                // Set Dialogue to Y response text if Option 1:
                StartCoroutine(TimeBetweenSpeakers(timeUntilYoohaResponds, "Wow it's 1AM already? I really didn't see the time pass!", yoohaName, "Time flies when you're having fun.", "You've had quite a few drinks", "Yeah well you know.", 7));

                // Transition:
                StartCoroutine(Transition(1));

                // Load Next Part:
                LoadNextPart();
            }
        }

        //
        // 4 >----- Screen Transition to Next Part -----> 5
        //

        // Option Button 1 Current Text = "Time flies when you're having fun.".
        // Option Button 2 Current Text = "You've had quite a few drinks"
        // Option Button 3 Current Text = "...".

        else if (currentPart == 5) // Part Five of Chapter Two:
        {
            if (optionSelected == 1 || optionSelected == 3)
            {
                _yResponded = false; // Yooha has not responded yet.

                _speakerNameText.text = playerName; // Set Speaker's Name/

                _dialogueText.text = "Well, time flies when you're having fun, I didn't notice either."; // Set Dialogue to A option 1 text.

                // Set Dialogue to Y response text if Option 1:
                StartCoroutine(TimeBetweenSpeakers(timeUntilYoohaResponds, "I guess it's time for me to go, then.", yoohaName, "I wish it didn't have to be.", "Until next time maybe?", "Yeah, get home safely.", 8));

                // Transition:
                StartCoroutine(Transition(1));

                // Load Next Part:
                LoadNextPart();
            }
            if (optionSelected == 2)
            {
                _yResponded = false; // Yooha has not responded yet.

                _speakerNameText.text = playerName; // Set Speaker's Name/

                _dialogueText.text = "Well, you've had quite a few drinks"; // Set Dialogue to A option 1 text.

                // Set Dialogue to Y response text if Option 1:
                StartCoroutine(TimeBetweenSpeakers(timeUntilYoohaResponds, "I guess it's time for me to go, then.", yoohaName, "I wish it didn't have to be.", "Until next time maybe?", "Yeah, get home safely.", 8));

                // Transition:
                StartCoroutine(Transition(1));

                // Load Next Part:
                LoadNextPart();
            }
        }

        //
        // 5 >----- Screen Transition to Next Part -----> 6
        //

        // Option Button 1 Current Text = "I wish it didn't have to be.".
        // Option Button 2 Current Text = "Until next time maybe?"
        // Option Button 3 Current Text = "Yeah, get home safely.".

        else if (currentPart == 6) // Part Five of Chapter Two:
        {
            if (optionSelected == 1)
            {
                _yResponded = false; // Yooha has not responded yet.

                _speakerNameText.text = playerName; // Set Speaker's Name/

                _dialogueText.text = "I really wish it didn't have to be."; // Set Dialogue to A option 1 text.

                // Set Dialogue to Y response text if Option 1:
                StartCoroutine(TimeBetweenSpeakers(timeUntilYoohaResponds, "Hope I see you again!", yoohaName, "", "", "", 9));

                // Transition:
                StartCoroutine(Transition(1));

                // Load Next Part:
                LoadNextPart();
            }
            if (optionSelected == 2)
            {
                _yResponded = false; // Yooha has not responded yet.

                _speakerNameText.text = playerName; // Set Speaker's Name/

                _dialogueText.text = "Yeah, until next time maybe?"; // Set Dialogue to A option 1 text.

                // Set Dialogue to Y response text if Option 1:
                StartCoroutine(TimeBetweenSpeakers(timeUntilYoohaResponds, "Hope I see you again!", yoohaName, "", "", "", 9));

                // Transition:
                StartCoroutine(Transition(1));

                // Load Next Part:
                LoadNextPart();
            }
            if (optionSelected == 3)
            {
                _yResponded = false; // Yooha has not responded yet.

                _speakerNameText.text = playerName; // Set Speaker's Name/

                _dialogueText.text = "Yeah, get home safely."; // Set Dialogue to A option 1 text.

                // Set Dialogue to Y response text if Option 1:
                StartCoroutine(TimeBetweenSpeakers(timeUntilYoohaResponds, "Hope I see you again!", yoohaName, "", "", "", 9));

                // Transition - Images fading out to show the Chapter end:
                //
                //
                StartCoroutine(Transition(1));

                // Load Next Part:
                LoadNextPart();
            }
        }

        else if (currentPart == maxParts)
        {
            StartCoroutine(LoadNextChapter());
        }
    }

    private IEnumerator LoadNextChapter()
    {
        yield return new WaitForSeconds(10f);
        //_canFade = true;
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

    private IEnumerator TimeBetweenSpeakers(int time, string followUpText, string name, string optionOneText, string optionTwoText, string optionThreeText, int imageNumber)
    {
        yield return new WaitForSeconds(time);

        _dialogueText.text = followUpText;
        _speakerNameText.text = name;

        _optionOneText.text = optionOneText;
        _optionTwoText.text = optionTwoText;
        _optionThreeText.text = optionThreeText;

        if (imageNumber == 1)
        {
            // Yooha Image One - Y: Hey, I'm here!.
            yoohaImage_1.SetActive(true);
        }
        else if (imageNumber == 2)
        {
            // Yooha Image Two - Y-1,3: haha, did I surprise you? I like to do that sometimes.
            yoohaImage_2.SetActive(true);
        }
        else if (imageNumber == 3)
        {
            // Yooha Image Three - Y-2: nice to meet you too!
            yoohaImage_3.SetActive(true);
        }
        else if (imageNumber == 4)
        {
            // Yooha Image Four - Transition.
            yoohaImage_4.SetActive(true);
        }
        else if (imageNumber == 5)
        {
            // Yooha Image Five - Y: Do you come here a lot?
            yoohaImage_5.SetActive(true);
        }
        else if (imageNumber == 6)
        {
            // Yooha Image Six - Y: Oh, that’s really nice. I can’t believe that I never heard of it before!
            yoohaImage_6.SetActive(true);
        }
        else if (imageNumber == 7)
        {
            // Yooha Image Seven - Y: Wow it's 1(am) already? I really didn’t see the time pass.
            _can2ndFade = true;
            yield return new WaitForSeconds(3);
        }
        else if (imageNumber == 8)
        {
            // Yooha Image Eight - Y: I guess it’s time for me to go, then.
            yoohaImage_8.SetActive(true);
        }
        else if (imageNumber == 9)
        {
            // Yooha Image Nine - Y: Hope I see you again!
            yoohaImage_9.SetActive(true);
        }
        _yResponded = true;

        ResetCountDownTime();
    }

    private IEnumerator TimePassingQuickCoroutine()
    {
        optionOneButton.interactable = false;
        optionTwoButton.interactable = false;
        optionThreeButton.interactable = false;

        _optionOneText.gameObject.SetActive(false);
        _optionTwoText.gameObject.SetActive(false);
        _optionThreeText.gameObject.SetActive(false);

        // Yooha Image Four - Transition.
        yoohaImage_4.SetActive(true);
        yield return new WaitForSeconds(5f);

        optionOneButton.interactable = true;
        optionTwoButton.interactable = true;
        optionThreeButton.interactable = true;

        _optionOneText.gameObject.SetActive(true);
        _optionTwoText.gameObject.SetActive(true);
        _optionThreeText.gameObject.SetActive(true);
    }

    private IEnumerator Transition(int time)
    {
        yield return new WaitForSeconds(time);
    }

    private IEnumerator SpecialTransition(int time)
    {
        yield return new WaitForSeconds(time);
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

    private void SecondFade()
    {
        if (_can2ndFade)
        {
            yoohaImage_7.SetActive(true);
            // Fade:
            fadeImage2.gameObject.SetActive(true);

            _fadeTimer -= Time.deltaTime;
            float newAlpha = Mathf.Lerp(0f, _currentAlpha, _fadeTimer / _fadeDuration);
            // Set Values for logic:
            Color newColor = fadeImage2.color;
            newColor.a = newAlpha;
            fadeImage2.color = newColor;
            // disable once complete:
            if (_fadeTimer <= 0f)
            {
                fadeImage2.gameObject.SetActive(false);

                _can2ndFade = false;
            }
        }
    }
}
