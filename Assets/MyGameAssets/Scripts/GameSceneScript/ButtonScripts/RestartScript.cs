using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScript : MonoBehaviour
{
    int waitTime = 1;

    public void WaitTime()
    {
        Invoke("RestartScene", waitTime);
    }

    //リスタート
    private void RestartScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
