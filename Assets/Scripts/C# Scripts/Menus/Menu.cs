using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Menu : MonoBehaviour
{
    [Header("References")]
    // Classes:

    // UI Elements - Main Menu:


    // UI Elements - Pause Menu:
    public TMP_Text btn_1_txt;

    public GameObject howToPlay_go;

    [Header("Variables")]
    private int _activeSceneIndex;
    private bool _gameScenesActive;

    private bool _howToPlayerMenuActive;

    // Start is called before the first frame update
    void Start()
    {
        // Set References in Scene:
        btn_1_txt = GetComponent<TextMeshPro>();

        //btn_1_txt = GameObject.Find("YourGameObjectName").GetComponent<TextMeshProUGUI>();


        // Set script var to current active scene:
        _activeSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Run Check Active Scene script based on script var value:
        CheckActiveScene(_activeSceneIndex);
        print("Active Scene: " + _activeSceneIndex);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckActiveScene(int sceneIndex)
    {
        if (sceneIndex == 0)
        {
            _gameScenesActive = false;
        }
        else
        {
            _gameScenesActive = true;
        }
    }

    public void OnButtonZeroClicked()
    {
        if (!_gameScenesActive) // Main Menu Logic:
        {
            // Button 1 = Play:
            SceneManager.LoadScene(_activeSceneIndex + 1);
        }

        else // Pause Menu Logic:
        {
            // Button 1 = Restart:
            // set pause to false..
        }
    }

    public void OnButtonOneCLicked()
    {
        if (!_gameScenesActive) // Main Menu Logic:
        {
            // Button 1 = How to Play Menu:
            if (!_howToPlayerMenuActive)
            {
                howToPlay_go.SetActive(true);

                btn_1_txt.text = "BACK";
                //btn_1_txt.text = "BACK";
            }
            else
            {
                howToPlay_go.SetActive(false);
                btn_1_txt.text = "HOW TO PLAY";
            }
        }

        else // Pause Menu Logic:
        {
            // Button 1 = Restart:
            SceneManager.LoadScene(_activeSceneIndex);
        }
    }

    public void OnButtonTwoCLicked()
    {
        if (!_gameScenesActive) // Main Menu Logic:
        {
            // Button 2 = Quit:
            Application.Quit();
        }

        else // Pause Menu Logic:
        {
            // Button 2 = Load Main Menu:
            SceneManager.LoadScene(0);
        }
    }
}
