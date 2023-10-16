using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMessage : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Text _playerMessageText;
    public DatingAppChat datingAppChat;

    void Start()
    {
        datingAppChat = FindObjectOfType<DatingAppChat>();

        _playerMessageText.text = datingAppChat.playerMessage;
    }
}
