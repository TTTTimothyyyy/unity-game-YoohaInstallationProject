using System.Collections;
using System.Collections.Generic;
//using System.Net.NetworkInformation;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Swipe : MonoBehaviour
{
    [Header("References")]
    // Classes:
    public ChapterManager chapterManager;
    public SoundManager soundManager;

    // Scene GameObjects:
    public GameObject MoveToPointGameObject;
    public GameObject SwpipeGameObject;

    // Parts:
    public GameObject Part1;
    public GameObject Part2;
    public GameObject Part3;
    // Fade:
    public Image fadeImage;
    // Instruction Images:
    public GameObject instructionOne;
    public GameObject instructionTwo;

    [Header("Varaibles")]
    // Animation:
    [SerializeField] private float _rotationSpeed = -45f;
    [SerializeField] private float _moveSpeed = 500f;
    [SerializeField] private bool _isMoving;
    [SerializeField] private bool _canWait;
    [SerializeField] private float _waitTime = 2f;
    // Current Part:
    [SerializeField] private int _currentPart = 0;
    // Fade:
    private float _fadeTimer;
    private float _currentAlpha;
    private float _fadeDuration = 3f;
    //
    private int maxParts;
    // Images:
    private int _currentImage;
    //private float timeLeft;
    //private bool _canWaitForImage;

    //public Button chatNowButton;

    void Start()
    {
        chapterManager = FindObjectOfType<ChapterManager>();
        soundManager = FindObjectOfType<SoundManager>();

        // Part 0:
        _currentPart = 0;
        fadeImage.gameObject.SetActive(true);
        _currentAlpha = fadeImage.color.a; // Get the initial alpha value of the image
        _fadeTimer = _fadeDuration; // Set the fade timer to the duration
        // Part 1:
        Part1.SetActive(true);
        SwpipeGameObject.SetActive(true);
        _canWait = false;
        _isMoving = false;
        // Part 2:
        Part2.SetActive(false);
        // Part 3:
        Part3.SetActive(false);
        // Instruction Images:
        instructionOne.SetActive(true);
        instructionTwo.SetActive(false);
        //_canWaitForImage = false;
        //
        maxParts = 4;

        _currentImage = 1;
    }


    void Update()
    {
        PartManager();
    }

    private void PartManager()
    {
        // Part 0:
        if (_currentPart == 0)
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

                _isMoving = true;
                _currentPart = 1;
            }
        }
        // Part 1:
        else if (_currentPart == 1)
        {
            if (_isMoving)
            {
                StartCoroutine(Part1Coroutine());
            }
        }
        // Part 2:
        else if (_currentPart == 2)
        {
            StartCoroutine(Part2Coroutine());
        }
        // Part 3:
        else if (_currentPart == 3)
        {
            StartCoroutine(Part3Coroutine());
        }
        // Part 4:
        else if (_currentPart == maxParts)
        {
            StartCoroutine(WaitTime(2));

            chapterManager.currentChapter++; // Load Next Chapter.
        }
    }

    public void PlayerInput()
    {
        if (_currentPart == 2)
        {
            print("Clicked: Chat Now. Showing Instructions");

            _currentPart = 3;
        }
    }

    /*private IEnumerator InstructionOneWaitTime(int time)
    {
        yield return new WaitForSeconds(time); // Wait for 4 seconds.
    }
    private IEnumerator InstructionTwoWaitTime(int time)
    {
        yield return new WaitForSeconds(time); // Wait for 2 seconds.
    }*/

    private IEnumerator Part1Coroutine()
    {
        //Play Sound:
        soundManager.PlaySwipeClip();
        yield return new WaitForSeconds(5f);

        // Calculate the translation distance based on time:
        float translationDistance = _moveSpeed * Time.deltaTime;

        // Move the game object towards MoveToPoint:
        SwpipeGameObject.transform.position = Vector3.MoveTowards(SwpipeGameObject.transform.position, MoveToPointGameObject.transform.position, translationDistance);

        // Calculate the rotation angle based on time:
        float rotationAngle = _rotationSpeed * Time.deltaTime;

        // Rotate the game object around the z-axis:
        SwpipeGameObject.transform.Rotate(0f, 0f, rotationAngle);

        // Check if the game object has reached point B:
        if (SwpipeGameObject.transform.position == MoveToPointGameObject.transform.position || SwpipeGameObject.transform.rotation.z == rotationAngle)
        {
            _isMoving = false;
            print("Reached point B");
            _canWait = true;

            SwpipeGameObject.SetActive(false);

            _currentPart = 2;
        }
    }

    private IEnumerator Part2Coroutine()
    {
        yield return new WaitForSeconds(5f);
        Part2.SetActive(true);
        Part1.SetActive(false);
    }

    private IEnumerator Part3Coroutine()
    {
        yield return new WaitForSeconds(1f);
        Part1.SetActive(false);
        Part2.SetActive(false);

        Part3.SetActive(true);

        if (_currentImage == 1)
        {
            StartCoroutine(Part4Coroutine_01());
        }
        if (_currentImage == 2)
        {
            StartCoroutine(Part4Coroutine_02());
        }
    }

    private IEnumerator Part4Coroutine_01()
    {
        yield return new WaitForSeconds(8f);
        // Set the first game object inactive
        instructionOne.SetActive(false);
        _currentImage = 2;
    }

    private IEnumerator Part4Coroutine_02()
    {
        // Set the second game object active
        instructionTwo.SetActive(true);
        yield return new WaitForSeconds(8f);
        // Load Next Part:
        _currentPart = 4;
    }

    private IEnumerator WaitTime(int time)
    {
        yield return new WaitForSeconds(time); // Wait for any amount of seconds.
    }
}
