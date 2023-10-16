using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YoohaMessage : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Text _yoohaMessageText;
    public DatingAppChat datingAppChat;

    void Start()
    {
        datingAppChat = FindObjectOfType<DatingAppChat>();

        _yoohaMessageText.text = datingAppChat.yoohaMessage;
    }
}
