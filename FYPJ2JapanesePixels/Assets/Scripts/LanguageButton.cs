﻿using UnityEngine;
using UnityEngine.UI;

public class LanguageButton : MonoBehaviour 
{
    public int buttonIndex { get; set; }
    public float ShowAnswerHowLong = 5;
    private TimerRoutine resetColourTime;
    private Color originalColour;

    void Start()
    {
        originalColour = GetComponent<Image>().color;
        resetColourTime = gameObject.AddComponent<TimerRoutine>();
        resetColourTime.initTimer(ShowAnswerHowLong);
        resetColourTime.executedFunction = resetColour;
    }

    void Update()
    {
        //if (transform.localPosition.y > 39f)
        //{
        //    transform.localPosition = new Vector2(transform.localPosition.x, 39f);
        //    Debug.Log(gameObject.name + " OUT, " + transform.localPosition.y);
        //}
    }

    void resetColour()
    {
        GetComponent<Image>().color = originalColour;
    }

    public void highlightCorrect()
    {
        GetComponent<Image>().color = Color.green;
        resetColourTime.executeFunction();
    }
}
