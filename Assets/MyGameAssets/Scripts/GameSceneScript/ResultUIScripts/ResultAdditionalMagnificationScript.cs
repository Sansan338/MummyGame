using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultAdditionalMagnificationScript : MonoBehaviour
{
    [SerializeField]
    Text magnificationText;

    private int deffaultMagnification = 1;       //デフォルトスコア倍率
    private int divideNumber = 10;               //スコア倍率を計算するために割る数
    public static float magnification;           //スコア加算倍率(1時間生きるごと)

    void Update()
    {
        //トータルスコア倍率 = 1 + (生存した時間 / 10)
        magnification = deffaultMagnification + (float)ResultSurvivalTimeUIScript.hours / divideNumber;
        magnificationText.text = magnification.ToString("n1");
    }
}
