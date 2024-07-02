using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultTotalScoreUIScript : MonoBehaviour
{
    [SerializeField]
    Text totalScoreText;

    private int hoursBonus = 1000;     //生存した時間に加算するスコア倍率
    private int minutesBonus = 10;     //生存した分数に加算するスコア倍率
    private int timeScore;             //生存時間のみのスコア
    private int oneHour = 60;          //1時間は60秒

    private float additionalMagnification;     //スコア加算倍率(1時間生きるごと)

    private int hitCount = 0;            //ゴーストに当たった総数
    private int minusScore = 0;          //ゴーストに触れたことによるマイナススコア
    private int minusMagnification = 50;

    private int killScore = 0;           //キルスコア
    private int escapeRedBonus = 200;
    private int killMobBonus = 50;

    private float totalScore;            //加点減点した結果のトータルスコア
    void Update()
    {
        //ゴーストに当たった総数
        hitCount = PlayerScript.redCount + PlayerScript.blueCount + PlayerScript.yellowCount +PlayerScript.whiteCount + PlayerScript.blackCount;

        //マイナススコア = ゴーストに当たった総数 * 50
        minusScore = hitCount * minusMagnification;

        //トータルスコア倍率 = 1 + (生存した時間 / 10)
        additionalMagnification = ResultAdditionalMagnificationScript.magnification;

        //生存スコア = (生存した時間 × 1000) + (60 × 生存した時間 + 生存した分数) × 10
        timeScore = ResultSurvivalTimeUIScript.hours * hoursBonus + (oneHour * ResultSurvivalTimeUIScript.hours+(int)TimeUIScript.minutesCount) * minutesBonus;

        //キルスコア = 赤を倒した数 × 200 + 赤以外を倒した数 × 50
        killScore = (RedGhostScript.escapeRedCount * escapeRedBonus) + (BombScript.killMobCount * killMobBonus);

        //トータルスコア = タイムスコア × トータルスコア倍率
        totalScore = (timeScore - minusScore + killScore) * additionalMagnification;

        //トータルスコアを表示
        totalScoreText.text = Mathf.Floor(totalScore).ToString();
    }
}
