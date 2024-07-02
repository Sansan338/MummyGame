using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RuleButtonScript : MonoBehaviour
{
    int waitTime = 1;

    public void WaitSound()
    {
        Invoke("RuleSceneButton", waitTime);
    }

    private void RuleSceneButton()
    {
        SceneManager.LoadScene("RuleScene");
    }
}
