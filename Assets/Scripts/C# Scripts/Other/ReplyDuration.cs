using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReplyDuration : MonoBehaviour
{
    [Header("References")]
    // Classes:
    //public Date date;

    // UI Elements:
    public Text timeLeftText;
    public Image timeLeftImageOne;
    public Image timeLeftImageTwo;

    [Header("Variables")]
    public float maxReplyDuration;
    public float _timeLeft;
    

    // Start is called before the first frame update
    void Start()
    {
        // Classes:
        //date = GetComponentInParent<Date>();

        //
        /*_timeLeft = maxReplyDuration;*/
    }

    // Update is called once per frame
    void Update()
    {
        /*if (date.canCountDown)
        {
            CountDownTimeUI();
        }
        else if (!date.canCountDown)
        {
            ResetReplyDurationTime();
        }*/
    }

    public void CountDownTimeUI()
    {
        _timeLeft -= 1 * Time.deltaTime;

        timeLeftText.text = ((int)_timeLeft).ToString();

        float currentFillAmount = _timeLeft / maxReplyDuration;
        timeLeftImageOne.fillAmount = currentFillAmount;
        timeLeftImageTwo.fillAmount = currentFillAmount;

        if (_timeLeft <= 0)
        {
            ResetReplyDurationTime();
        }
        
    }

    public void ResetReplyDurationTime() // can call this outside of class.
    {
        _timeLeft = maxReplyDuration;
    }
}
