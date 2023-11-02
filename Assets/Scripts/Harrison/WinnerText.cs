using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerText : MonoBehaviour
{
    [SerializeField] private string[] text;
    [SerializeField] private Color player1Colour;
    [SerializeField] private Color player2Colour;
    public void Start()
    {
        TextMesh textComponent = GetComponent<TextMesh>();
        string displayText = "";
        bool addedNumber = false;
        for (int t = 0; t < text.Length; t++)
        {
            if (t > 0)
            {
                displayText += WinnerTracking.instance.winner.ToString();
                addedNumber = true;
            }
            displayText += text[t];
        }
        if (!addedNumber)
        {
            displayText += WinnerTracking.instance.winner.ToString();
        }
        Debug.Log(displayText);
        textComponent.text = displayText;
        if (WinnerTracking.instance.winner == 1)
        {
            textComponent.color = player1Colour;
        }
        else if (WinnerTracking.instance.winner == 2)
        {
            textComponent.color = player2Colour;
        }
    }
}
