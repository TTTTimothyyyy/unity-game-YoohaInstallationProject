using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollView : MonoBehaviour
{
    void Update()
    {
        // Scroll to the bottom to show the latest item
        ScrollRect scrollRect = GetComponent<ScrollRect>();
        scrollRect.verticalNormalizedPosition = 0f;
    }
}
