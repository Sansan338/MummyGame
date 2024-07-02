using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultSurvivalTimeUIScript : MonoBehaviour
{
    [SerializeField]
    Text hoursText;
    [SerializeField]
    Text minutesText;

    public static int hours = 0;
    public static float minutes = 0;

    void Update()
    {
        hours = TimeUIScript.hoursCount;
        minutes = TimeUIScript.minutesCount;
        hoursText.text = hours.ToString();
        minutesText.text = Mathf.Floor(minutes).ToString();
    }
}
