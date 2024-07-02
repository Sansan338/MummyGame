using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultHitsCountUIScript : MonoBehaviour
{
    private int redCount = 0;
    private int blueCount = 0;
    private int yellowCount = 0;
    private int whiteCount = 0;
    private int blackCount = 0;

    [SerializeField]
    Text redText;
    [SerializeField]
    Text blueText;
    [SerializeField]
    Text yellowText;
    [SerializeField]
    Text whiteText;
    [SerializeField]
    Text blackText;

    // Update is called once per frame
    void Update()
    {
        redCount = PlayerScript.redCount;
        redText.text = redCount.ToString();

        blueCount = PlayerScript.blueCount;
        blueText.text = blueCount.ToString();

        yellowCount = PlayerScript.yellowCount;
        yellowText.text = yellowCount.ToString();

        whiteCount = PlayerScript.whiteCount;
        whiteText.text = whiteCount.ToString();

        blackCount = PlayerScript.blackCount;
        blackText.text = blackCount.ToString();
    }
}
