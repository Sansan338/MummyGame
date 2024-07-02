using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuffDebuffMobScript : MonoBehaviour
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

    void Start()
    {
        //プレイヤーの座標を取得
        playerTransform = GameObject.Find("Player").transform;
    }

    void Update()
    {
        //ゴーストが存在しているかつゲームがプレイ中なら
        if (isDie == false && GameManager.gameStatus == GameManager.GameStatus.Play)
        {
            //アニメーションを有効化
            ghostAnimator.SetBool("isMoving", true);
            //ゴーストの目的地をプレイヤーの現在座標に
            agent.destination = playerTransform.position;
        }
        //ゲームオーバーになったら
        if(GameManager.gameStatus == GameManager.GameStatus.GameOver)
        {
            //ゴーストが逃げるエフェクトを生成
            Instantiate(escapeEffect, this.transform.position, Quaternion.Euler(-90,0,0));
            //ゴーストオブジェクトを破壊
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        //ゴーストが爆弾かプレイヤーに当たると
        if (collider.gameObject.tag == "Bomb" || collider.gameObject.tag == "Player")
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