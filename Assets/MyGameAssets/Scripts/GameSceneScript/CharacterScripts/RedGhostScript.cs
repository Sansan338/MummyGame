using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;

public class RedGhostScript : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;     //プレイヤーの位置情報
    [SerializeField]
    private GameObject deathEffect;        //死亡時のエフェクト
    [SerializeField]
    private GameObject escapeEffect;       //ゲームオーバー時のゴーストが逃げたエフェクト
    [SerializeField]
    private Animator ghostAnimator;        //ゴーストのアニメーター
    [SerializeField]
    private NavMeshAgent agent;　　　　　　//NavMeshAgent

    private bool isDie = false;            //ゴーストは死んでいますか

    private float timeCount;               //タイムカウント
    public int lifeSpan = 90;              //赤ゴーストの生存時間
    public static int escapeRedCount;             //赤ゴーストの脅威から逃れた回数
    void Start()
    {
        //カウントリセット
        timeCount = 0;
        escapeRedCount = 0;

        //プレイヤーの座標を取得
        playerTransform = GameObject.Find("Player").transform;
    }

    void Update()
    {
        //時間をカウント
        timeCount += Time.deltaTime;
        //ゴーストが存在しているかつゲームがプレイ中なら
        if (isDie == false && GameManager.gameStatus == GameManager.GameStatus.Play)
        {
            //アニメーションを有効化
            ghostAnimator.SetBool("isMoving", true);
            //ゴーストの目的地をプレイヤーの現在座標に
            agent.destination = playerTransform.position;
        }
        //ゲームオーバーになったら
        if (GameManager.gameStatus == GameManager.GameStatus.GameOver)
        {
            //ゴーストが逃げるエフェクトを生成
            Instantiate(escapeEffect, this.transform.position, Quaternion.Euler(-90, 0, 0));
            //ゴーストオブジェクトを破壊
            Destroy(this.gameObject);
        }

        //赤ゴーストが沸いてから寿命を迎えると
        if(timeCount >= lifeSpan)
        {
            //ゴーストの死亡エフェクトを生成
            Instantiate(escapeEffect, this.transform.position, Quaternion.Euler(-90,0,0));
            //ゴーストのオブジェクトを破壊
            Destroy(this.gameObject);
            //ゴーストは死んでいますか　-->はい
            isDie = true;
            escapeRedCount++;
            timeCount = 0;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        //プレイヤーに当たると
        if (collider.gameObject.tag == "Player")
        {
            //ゴーストの死亡エフェクトを生成
            Instantiate(deathEffect, this.transform.position, Quaternion.identity);
            //ゴーストのオブジェクトを破壊
            Destroy(this.gameObject);
            //ゴーストは死んでいますか　-->はい
            isDie = true;
        }
    }
}
