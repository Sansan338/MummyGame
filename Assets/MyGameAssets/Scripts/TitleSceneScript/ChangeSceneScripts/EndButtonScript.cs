using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndButtonScript : MonoBehaviour
{
    int waitTime = 1;

    public void WaitSound()
    {
        Invoke("EndButton", waitTime);
    }
    private void EndButton()
    {
        //ÉQÅ[ÉÄÇèIóπÇ∑ÇÈ
        Application.Quit();
    }
}
