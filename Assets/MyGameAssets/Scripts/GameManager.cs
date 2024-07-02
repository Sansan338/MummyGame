using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameStatus { Play, GameOver, Result, Next}     //ゲームステータス(プレイ状態、ゲームオーバー状態、リザルト状態)
    public static GameStatus gameStatus;

    private int waitTime = 3;      //ゲームオーバー後にリザルト画面へ以降するまでの待機時間

    void Start()
    {
        //ゲーム開始時はゲームステータスをプレイ中に設定
        gameStatus = GameStatus.Play;
    }

    void Update()
    {
        //ゲームオーバー状態になると
        if(gameStatus == GameStatus.GameOver)
        {
            //waitTime秒待ってからリザルト画面に移行する
            Invoke("TransferResult", waitTime);
        }
    }

    private void TransferResult()
    {
        //ゲームステータスをリザルト状態にする
        gameStatus = GameStatus.Result;
    }
}
