using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject discalimerMenu;
    public GameObject welcomeBackMenu;

    [SerializeField] private bool _disclaimmerShowed;

    [SerializeField] private int _currentPart;
    public void Start()
    {
        _currentPart = 0;

        _disclaimmerShowed = false;

        welcomeBackMenu.SetActive(true);
    }
    public void Update()
    {
        if (_currentPart == 0)
        {
            StartCoroutine(WelcomeBackCoroutine());
        }
        if (_currentPart == 1)
        {
            if (!_disclaimmerShowed)
            {
                StartCoroutine(DisclaimerCoroutine());
            }
            if (Input.anyKeyDown)
            {
                StopCoroutine(DisclaimerCoroutine());
                discalimerMenu.SetActive(false);
                _disclaimmerShowed = true;
            }
        }
    }

    public void PlayGameClicked()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGameClicked()
    {
        Application.Quit();
    }

    private IEnumerator WelcomeBackCoroutine()
    {
        yield return new WaitForSeconds(5f);
        welcomeBackMenu.SetActive(false);
        _currentPart++;
    }

    private IEnumerator DisclaimerCoroutine()
    {
        yield return new WaitForSeconds(10f);

        discalimerMenu.SetActive(false);
        _disclaimmerShowed = true;
    }
}
