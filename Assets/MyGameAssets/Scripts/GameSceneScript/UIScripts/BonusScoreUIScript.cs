using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusScoreUIScript : MonoBehaviour
{
    [SerializeField]
    Animator addScoreAnimator;

    private int hours = 0;

    private int waittime = 3;

    void Start()
    {
        addScoreAnimator.SetBool("isBonus", false);
    }

    void Update()
    {
        if (TimeUIScript.hoursCount != hours)
        {
            addScoreAnimator.SetBool("isBonus",true);
            Invoke("WaitTime",waittime);
        }
        else if(TimeUIScript.hoursCount == hours)
        {
            addScoreAnimator.SetBool("isBonus", false);
        }
    }

    private void WaitTime()
    {
        hours = TimeUIScript.hoursCount;
    }
}
