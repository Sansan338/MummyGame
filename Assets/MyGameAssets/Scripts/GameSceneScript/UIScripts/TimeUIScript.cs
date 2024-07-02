using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUIScript : MonoBehaviour
{
    public static float minutesCount;      //分数のカウント
    private float oldMinutesCount;         //比較用分数カウント
    public static int hoursCount;          //時間のカウント

    private float oneHours = 60.0f;        //1時間は60分
    private int oneDay = 24;               //1日は24時間

    public Text minutesText;
    public Text hoursText;

    // Start is called before the first frame update
    void Start()
    {
        //時間のカウントを初期化
        minutesCount = 0.0f;
        oldMinutesCount = 0.0f;
        hoursCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //カウントアップ
        minutesCount += Time.deltaTime;

        //60分(現実世界では60秒)経過すると
        if (minutesCount >= oneHours)
        {
            //1時間経過
            hoursCount++;

            //分数をリセット
            minutesCount -= oneHours;
        }

        //24時間(現実世界では24分)経過すると
        if (hoursCount >= oneDay)
        {
            //時間をリセット
            hoursCount -= oneDay;
        }
        if ((int)minutesCount != (int)oldMinutesCount)
        {
            //表示
            minutesText.text = minutesCount.ToString("F0").PadLeft(2,'0');
            hoursText.text = hoursCount.ToString();
        }
        //分数の余りの部分を残しておく
        oldMinutesCount = minutesCount;
        
    }
}
