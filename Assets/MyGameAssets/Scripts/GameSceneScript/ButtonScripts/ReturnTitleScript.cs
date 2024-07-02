using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnTitleScript : MonoBehaviour
{
    int waitTime = 1;

    public void WaitTime()
    {
        Invoke("ReturnTitle", waitTime);
    }

    //�^�C�g���ɖ߂�
    private void ReturnTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
