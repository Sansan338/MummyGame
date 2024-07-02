using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManegerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject DeathUI;        //死亡UI
    [SerializeField]
    private GameObject TimeUI;         //現在時刻UI
    [SerializeField]
    private GameObject ScoreUI;        //リザルトスコアUI
    [SerializeField]
    private GameObject HeartUI;        //心臓UI
    [SerializeField]
    private GameObject BonusScoreUI;   //ボーナススコアUI
    [SerializeField]
    private GameObject FallSpeedUpUI;  //爆弾落下間隔スピードアップUI
    [SerializeField]
    private GameObject SpawnSpeedUI;   //ゴーストスポーンスピードアップUI

    [SerializeField]
    Animator fallSpeedUpAnimator;
    [SerializeField]
    Animator spawnSpeedUpAnimator;

    private int deathUIwaitTime = 1;   //死亡後に死亡UIを表示するまでの待機時間
    private int speedUpUIwaitTime = 4; //スピードアップ非表示までの時間

    void Start()
    {
        //ゲーム開始時は死亡UIとリザルトスコアUIは非表示
        DeathUI.SetActive(false);
        ScoreUI.SetActive(false);
        //ゲーム開始時に現在時刻UIと心臓UI、ボーナススコアUI、落下スピード上昇UI、スポーンスピード上昇UIは有効化
        TimeUI.SetActive(true);
        HeartUI.SetActive(true);
        BonusScoreUI.SetActive(true);
        FallSpeedUpUI.SetActive(true);
        SpawnSpeedUI.SetActive(true);
        //スピードアップ上昇UIのアニメーターを無効状態にしておく
        fallSpeedUpAnimator.SetBool("isSpeedUp", false);
        spawnSpeedUpAnimator.SetBool("isSpeedUp", false);
    }

    void Update()
    {
        //ゲームステータスがゲームオーバー状態になると
        if(GameManager.gameStatus == GameManager.GameStatus.GameOver)
        {
            //現在時刻UIと心臓UI、ボーナススコアUIを無効化
            TimeUI.SetActive(false);
            HeartUI.SetActive(false);
            BonusScoreUI.SetActive(false );
            //deathUIwaitTime秒後にDeathUIを表示
            Invoke("ShowDeathUI", deathUIwaitTime);
            //スピードアップ上昇UIを無効化
            FallSpeedUpUI.SetActive(false);
            SpawnSpeedUI.SetActive(false);
        }
        //ゲームステータスがリザルト状態になると
        else if(GameManager.gameStatus == GameManager.GameStatus.Result)
        {
            //リザルトスコアUIを有効化
            ScoreUI.SetActive(true);
        }

        //爆弾落下間隔スピードアップUI表示
        if (BombAppearanceScript.isSpeedUp == true)
        {
            fallSpeedUpAnimator.SetBool("isSpeedUp", true);
            Invoke("HiddenFallSpeedUpUI", speedUpUIwaitTime);
        }
        //ゴーストスポーン間隔スピードアップUI表示
        if (MobSpawnScript.isSpeedUp == true)
        {
            spawnSpeedUpAnimator.SetBool("isSpeedUp", true);
            Invoke("HiddenSpawnSpeedUpUI", speedUpUIwaitTime);
        }
    }

    private void ShowDeathUI()
    {
        //死亡UIを有効化
        DeathUI.SetActive(true);
    }

    private void HiddenFallSpeedUpUI()
    {
        //スピードアップUIを非表示
        fallSpeedUpAnimator.SetBool("isSpeedUp", false);
        BombAppearanceScript.isSpeedUp = false;
    }
    private void HiddenSpawnSpeedUpUI()
    {
        //スピードアップUIを非表示
        spawnSpeedUpAnimator.SetBool("isSpeedUp", false);
        MobSpawnScript.isSpeedUp = false;
    }
}
