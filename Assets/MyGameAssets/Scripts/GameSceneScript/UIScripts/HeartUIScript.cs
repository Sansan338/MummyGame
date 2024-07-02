using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUIScript : MonoBehaviour
{
    [SerializeField]
    Animator heartAnimator;                     //心臓のアニメーター

    private float maxplayerHP = 100.0f;         //プレイヤーの最HP
    private float deffaultHeartSpeed = 1.0f;    //デフォルトの心拍速度

    //普段の心拍に設定
    public void NormalHeartBeat()
    {
        heartAnimator.SetBool("isNormal", true);
        heartAnimator.SetBool("isSlow", false);
        heartAnimator.SetBool("isHigh", false);
        heartAnimator.SetBool("isStop", false);
    }

    //青ゴーストに当たった時の心拍に設定
    public void SlowHeartBeat()
    {
        heartAnimator.SetBool("isSlow", true);
        heartAnimator.SetBool("isNormal", false);
        heartAnimator.SetBool("isHigh", false);
        heartAnimator.SetBool("isStop", false);
    }

    //黄ゴーストに当たった時の心拍に設定
    public void StopHeartBeat()
    {
        heartAnimator.SetBool("isStop", true);
        heartAnimator.SetBool("isNormal", false);
        heartAnimator.SetBool("isSlow", false);
        heartAnimator.SetBool("isHigh", false);
    }

    //白ゴーストに当たった時の心拍に設定
    public void HighHeartBeat()
    {
        heartAnimator.SetBool("isHigh", true);
        heartAnimator.SetBool("isNormal", false);
        heartAnimator.SetBool("isSlow", false);
        heartAnimator.SetBool("isStop", false);
    }

    public void HeartSpeed()
    {
        //プレイヤーのHPが減少すると心拍が速くなる
        heartAnimator.SetFloat("Speed",deffaultHeartSpeed + (maxplayerHP - PlayerScript.playerHP) / 100);
    }
}
