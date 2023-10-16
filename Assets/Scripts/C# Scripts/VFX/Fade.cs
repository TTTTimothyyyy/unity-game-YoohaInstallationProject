using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{

    [Header("References")]
    // Classes:

    // UI:
    public Image fadeImage;

    [Header("Variables")]
    // Fade:
    private float _fadeTimer;
    private float _currentAlpha;
    private float _fadeDuration = 2f;
    // Start is called before the first frame update
    void Start()
    {
        fadeImage.enabled = true;
        //fadeImage = FindObjectOfType<Image>(); // Get the Image component attached to the same GameObject
        _currentAlpha = fadeImage.color.a; // Get the initial alpha value of the image
        _fadeTimer = _fadeDuration; // Set the fade timer to the duration
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeInFromLastChapter()
    {
        fadeImage.enabled = true;
        // Decrease the alpha value gradually over time
        _fadeTimer -= Time.deltaTime;
        float newAlpha = Mathf.Lerp(0f, _currentAlpha, _fadeTimer / _fadeDuration);

        // Update the image's color with the new alpha value
        Color newColor = fadeImage.color;
        newColor.a = newAlpha;
        fadeImage.color = newColor;

        // Disable the image component when the fade is complete
        if (_fadeTimer <= 0f)
        {
            _fadeTimer = 2f;

            fadeImage.enabled = false;
            fadeImage.color = Color.white;
            fadeImage.color.a.Equals(1);
        }
    }

    public void FadeOutFromLastChapter()
    {
        fadeImage.enabled = true;
        // Decrease the alpha value gradually over time
        _fadeTimer += Time.deltaTime;
        float newAlpha = Mathf.Lerp(0f, _currentAlpha, _fadeTimer / _fadeDuration);

        // Update the image's color with the new alpha value
        Color newColor = fadeImage.color;
        newColor.a = newAlpha;
        fadeImage.color = newColor;

        // Disable the image component when the fade is complete
        if (_fadeTimer <= 0f)
        {
            _fadeTimer = 2f;

            fadeImage.enabled = true;
            fadeImage.color = Color.white;
            fadeImage.color.a.Equals(1);
        }
    }
}
