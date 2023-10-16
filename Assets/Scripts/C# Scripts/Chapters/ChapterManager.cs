using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class ChapterManager : MonoBehaviour
{
    [Header("References")]
    // Classes:
    //public Date date;

    // GameObjects:
    public GameObject introGameObject;
    public GameObject bumbleChatGameObject;
    public GameObject dateGameObject;
    public GameObject bumbleChatGhostGameObject;
    public GameObject outroGameObject;

    public GameObject timeRemaining;
    public GameObject YoohaImages;

    [Header("Variables")]
    public int currentChapter = 0;

    private int _chapterZero_intro = 0;
    private int _chapterOne_bumbleChat = 1;
    private int _chapterTwo_date = 2;
    private int _chapterThree_bumbleChatGhost = 3;
    private int _chapterFour_outro = 4;

    public string playerChosenName;

    private int fadedCount;
    void Start()
    {
        //date = FindObjectOfType<Date>();

        currentChapter = 0; // change to 0 for actual build otherwise anything for testing.

        introGameObject.SetActive(false);
        bumbleChatGameObject.SetActive(false);
        dateGameObject.SetActive(false);
        bumbleChatGhostGameObject.SetActive(false);
        outroGameObject.SetActive(false);

        timeRemaining.SetActive(false);

        YoohaImages.SetActive(false);
    }

    void Update()
    {
        if (currentChapter == _chapterZero_intro) // Chapter Zero - INTRO:
        {
            // Run Chapter Zero Logic - Set GO and Class Active:
            introGameObject.SetActive(true);

            // Set All Other Chapters to InActive:
            bumbleChatGameObject.SetActive(false);
            dateGameObject.SetActive(false);
            bumbleChatGhostGameObject.SetActive(false);
            outroGameObject.SetActive(false);

            timeRemaining.SetActive(false);
        }
        else if (currentChapter == _chapterOne_bumbleChat) // Chapter One - MAIN - BUMBLE CHAT:
        {
            // Run Chapter One Logic - Set GO and Class Active:
            bumbleChatGameObject.SetActive(true);

            // Set All Other Chapters to InActive:
            introGameObject.SetActive(false);
            dateGameObject.SetActive(false);
            bumbleChatGhostGameObject.SetActive(false);
            outroGameObject.SetActive(false);

            timeRemaining.SetActive(true);
        }
        else if (currentChapter == _chapterTwo_date) // Chapter Two - MAIN - DATE:
        {
            // Run Chapter Two Logic - Set GO and Class Active:
            dateGameObject.SetActive(true);

            // Set All Other Chapters to InActive:
            introGameObject.SetActive(false);
            bumbleChatGameObject.SetActive(false);
            bumbleChatGhostGameObject.SetActive(false);
            outroGameObject.SetActive(false);

            timeRemaining.SetActive(true);

            YoohaImages.SetActive(true);
        }
        else if (currentChapter == _chapterThree_bumbleChatGhost) // Chapter Three - RAGE - BUMBER CHAT:
        {
            // Run Chapter Three Logic - Set GO and Class Active:
            bumbleChatGhostGameObject.SetActive(true);

            // Set All Other Chapters to InActive:
            introGameObject.SetActive(false);
            bumbleChatGameObject.SetActive(false);
            dateGameObject.SetActive(false);
            outroGameObject.SetActive(false);

            timeRemaining.SetActive(true);

            YoohaImages.SetActive(false);
        }
        else if (currentChapter == _chapterFour_outro) // Chapter Four - OUTRO - ENDING & LOAD MAIN MENU:
        {
            // Run Chapter Four Logic - Set GO and Class Active:
            outroGameObject.SetActive(true);

            // Set All Other Chapters to InActive:
            introGameObject.SetActive(false);
            bumbleChatGameObject.SetActive(false);
            dateGameObject.SetActive(false);
            bumbleChatGhostGameObject.SetActive(false);

            timeRemaining.SetActive(false);
        }
    }
}
