using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueMechanics : MonoBehaviour
{
    [Header("References")]
    // Classes:

    // UI Elements:
    [SerializeField] private Text _speakerNameText;

    [SerializeField] private Text _dialogueText;

    [SerializeField] private Text _optionOneText;
    [SerializeField] private Text _optionTwoText;
    [SerializeField] private Text _optionThreeText;

    [Header("Variables")]
    public int chapterMaxParts;
    public int currentChapter;
    public int currentPart;

    public int optionSelected;


    void Start()
    {
        chapterMaxParts = 0;

        currentChapter = 1;
        currentPart = 1;

        optionSelected = 0;
    }

    void Update()
    {
        ChapterUIManager();
    }

    private void LoadNextChapter()
    {
        currentChapter++;
        currentPart = 1;
    }

    private void LoadNextPart()
    {
        currentPart++;
    }

    private void ChapterUIManager()
    {
        if (currentChapter == 1) // Chapter One - "Info about this chapter":
        {
            // Set Chapter Max Pats:
            chapterMaxParts = 4;

            if (currentPart == 1) // Part One of Chapter One:
            {
                // Run Logic:


            }
            if (currentPart == 2) // Part Two of Chapter One:
            {
                // Run Logic:


            }
            if (currentPart == 3) // Part Three of Chapter One:
            {
                // Run Logic:


            }

            if (currentPart == chapterMaxParts)
            {
                LoadNextChapter();
            }
        }

        if (currentChapter == 2) // Chapter Two - "Info about this chapter":
        {
            // Set Chapter Max Pats:
            chapterMaxParts = 4;

            if (currentPart == 1) // Part One of Chapter Two:
            {
                // Run Logic:


            }
            if (currentPart == 2) // Part Two of Chapter Two:
            {
                // Run Logic:


            }
            if (currentPart == 3) // Part Three of Chapter Two:
            {
                // Run Logic:


            }

            if (currentPart == chapterMaxParts)
            {
                LoadNextChapter();
            }
        }

        if (currentChapter == 3) // Chapter Three - "Info about this chapter":
        {
            // Set Chapter Max Pats:
            chapterMaxParts = 4;

            if (currentPart == 1) // Part One of Chapter Three:
            {
                // Run Logic:


            }
            if (currentPart == 2) // Part Two of Chapter Three:
            {
                // Run Logic:


            }
            if (currentPart == 3) // Part Three of Chapter Three:
            {
                // Run Logic:


            }

            if (currentPart == chapterMaxParts)
            {
                LoadNextChapter();
            }
        }
    }

    public void OnOptionOneClicked() // Player clicks Option One:
    {
        optionSelected = 1;

        // Load Next Part:
        LoadNextPart();
    }

    public void OnOptionTwoClicked() // Player clicks Option Two:
    {
        optionSelected = 2;

        // Load Next Part:
        LoadNextPart();
    }

    public void OnOptionThreeClicked() // Player clicks Option Three:
    {
        optionSelected = 3;

        // Load Next Part:
        LoadNextPart();
    }
}
