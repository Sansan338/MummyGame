using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{
    //プレイヤーのステータス
    public static int playerHP;　//プレイヤーの体力
    public float walkSpeed = 8.0f;      //歩く速度
    public float defaultSpeed = 8.0f;   //歩く速度を元に戻すための
    public float buffSpeed = 11.0f;     //バフ状態速度
    public float slowSpeed = 5.0f;    　//スロウ状態速度
    public float rotateSpeed = 10.0f;   //回転速度
    public static bool isDie;    //死んでいるか
    Vector3 moveDirection;       //座標


    public int hitDamage = 20;          //ゴーストに当たった時のダメージ

    //バフデバフ効果時間
    private float blueTimeCount = 0.0f;     //青ゴーストに当たった時のタイムカウント
    private float yellowTimeCount = 0.0f;   //黄ゴーストに当たった時のタイムカウント
    private float whiteTimeCount = 0.0f;    //白ゴーストに当たった時のタイムカウント
    private float blackTimeCount = 0.0f;    //黒ゴーストに当たった時のタイムカウント
    
    public float slowTime = 5.0f;                  //青ゴーストに当たった時のスロウ効果時間
    public float electricshockTime = 1.5f;         //黄ゴーストに当たった時の停止時間
    public float buffTime = 5.0f;                  //白ゴーストに当たった時の速度アップ時間
    public float blindTime = 2.5f;                 //黒ゴーストに当たった時のブラインド時間

    //衝突判定
    private bool isBlue = false;    //青
    private bool isYellow = false;   //黄色
    private bool isBlack = false;   //黒
    private bool isWhite = false;   //白

    //衝突カウント
    public static int redCount;      //赤に当たったか
    public static int blueCount;     //青に何回当たったか
    public static int yellowCount;   //黄に何回当たったか
    public static int whiteCount;    //白に何回当たったか
    public static int blackCount;    //黒に何回当たったか

    //プレイヤー
    [SerializeField]
    Animator playerAnimator;     //プレイヤーのアニメーター
    [SerializeField]
    Rigidbody playerRididbody;   //プレイヤーのリジッドボディ

    //パーティクル
    [SerializeField]
    GameObject deathEffect;                  //死亡時のパーティクル
    [SerializeField]
    ParticleSystem slowParticle;             //スロウ効果のパーティクル
    [SerializeField]
    ParticleSystem electricshockParticle;    //感電効果のパーティクル
    [SerializeField]
    ParticleSystem buffParticle;             //移動速度アップのパーティクル

    //心臓UI
    [SerializeField]
    GameObject heart;

    //ライト
    [SerializeField]
    GameObject spotLight;
    [SerializeField]
    GameObject directionalLight;

    //BGM
    [SerializeField]
    AudioSource InGameAudio;     //プレイ中のBGM

    void Start()
    {
        //プレイヤーは生きていて、体力が100
        isDie = false;
        playerHP = 100;

        //ゴーストに当たった数の初期化
        redCount = 0;
        blueCount = 0;
        yellowCount = 0;
        whiteCount = 0;
        blackCount = 0;
    }

    void Update()
    {
        //WASDもしくは矢印キーの入力情報を取得
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        moveDirection = new Vector3(moveX,0,moveZ);
        //正規化
        moveDirection.Normalize();
        //黄の効果時間以外は常に移動可能
        if (isYellow == false)
        {
            transform.position += moveDirection * walkSpeed * Time.deltaTime;
        }
        //プレイヤーを移動
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
        //プレイヤーが移動しているなら
        if (moveZ != 0 || moveX != 0)
        {
            //歩きアニメーションを有効化
            playerAnimator.SetBool("isMoving", true);
        }
        //プレイヤーが移動していないなら
        else
        {
            //歩きアニメーションを無効化
            playerAnimator.SetBool("isMoving", false);
        }

        //HPが0になると
        if (playerHP <= 0)
        {
            //死んでいますか --> はい
            isDie = true;
        }
        //プレイヤーが死ぬと
        if (isDie == true)
        {
            //ゲームオーバーになる
            GameManager.gameStatus = GameManager.GameStatus.GameOver;
            //死亡エフェクトを生成
            Instantiate(deathEffect,this.transform.position,Quaternion.Euler(-90,0,0));
            //プレイヤーのゲームオブジェクトを破壊
            Destroy(this.gameObject);
            //ゲームオーバーになるとBGMの停止
            InGameAudio.volume = 0;
        }

        //バフデバフ効果
        //青
        if (isBlue == true)
        {
            HitBlueGhost();
        }
        //黄
        if (isYellow == true)
        {
            HitYellowGhost();
        }
        //白
        if (isWhite == true)
        {
            HitWhiteGhost();
        }
        //黒
        if (isBlack == true)
        {
            HitBlackGhost();
        }
    }

    //プレイヤーが何かにぶつかった
    private void OnTriggerEnter(Collider collider)
    {
        //プレイヤーが爆弾に当たると
        if(collider.gameObject.tag == "Bomb")
        {
            //死んでいますか --> はい
            isDie = true;
        }
        //プレイヤーが赤ゴーストに当たると
        if (collider.gameObject.tag == "Enemy")
        {
            //死んでいますか --> はい
            isDie = true;
            //赤に当たった回数　+1
            redCount++;
        }
        //青モブに当たったら
        if (collider.gameObject.tag == "Blue")
        {
            //プレイヤーのステータスをリセット
            ResetPlayerState();
            //青に当たりましたよ
            isBlue = true;

            //体力マイナス
            HitMobDamage();

            //パーティクルを生成and再生and破壊
            BuffDebuffParticle(slowParticle, slowTime);

            //青ゴーストに当たった回数カウント
            blueCount++;
        }
        //黄モブに当たったら
        if (collider.gameObject.tag == "Yelow")
        {
            //プレイヤーのステータスをリセット
            ResetPlayerState();
            //黄に当たりましたよ
            isYellow = true;

            //体力マイナス
            HitMobDamage();

            //パーティクルを生成and再生and破壊
            BuffDebuffParticle(electricshockParticle, electricshockTime);

            //黄ゴーストに当たった回数カウント
            yellowCount++;
        }
        //白モブに当たったら
        if (collider.gameObject.tag == "White")
        {
            //プレイヤーのステータスをリセット
            ResetPlayerState();
            //白に当たりましたよ
            isWhite = true;

            //パーティクルを生成and再生and破壊
            BuffDebuffParticle(buffParticle,buffTime);

            //白ゴーストに当たった回数カウント
            whiteCount++;
        }
        //黒モブに当たったら
        if(collider.gameObject.tag == "Black")
        {
            //黒に当たりましたよ
            isBlack = true;

            //黒に当たった回数カウント
            blackCount++;
        }
    }

    //プレイヤーのステータスをリセット
    private void ResetPlayerState()
    {
        //速度を元の速度に戻す
        walkSpeed = defaultSpeed;
        //衝突判定をリセット
        isBlue = false;
        isYellow = false;
        isWhite = false;
    }

    //モブに衝突した時にダメージを受ける
    private void HitMobDamage()
    {
        //体力-ダメージ
        playerHP -= hitDamage;
    }

    //バフデバフ効果のパーティクル
    private void BuffDebuffParticle(ParticleSystem particle,float effectTime)
    {
        //パーティクルを生成
        ParticleSystem newParticle = Instantiate(particle);
        newParticle.transform.position = this.gameObject.transform.position;
        newParticle.transform.parent = this.gameObject.transform;
        //パーティクルを再生
        newParticle.Play();
        Destroy(newParticle.gameObject, effectTime);
    }

    //青ゴーストに当たった時のバフデバフ効果
    private void HitBlueGhost()
    {
        //タイムカウント
        blueTimeCount += Time.deltaTime;

        //スロウ効果時間内は
        if (blueTimeCount <= slowTime)
        {
            //スロウ効果により減速
            walkSpeed = slowSpeed;

            //心臓の鼓動が遅くなるアニメーション
            heart.GetComponent<HeartUIScript>().SlowHeartBeat();
        }
        //スロウ効果時間が終了すると
        else if (blueTimeCount > slowTime)
        {
            //速度を元の速度に戻す
            walkSpeed = defaultSpeed;

            //衝突判定リセット
            isBlue = false;

            //タイムカウントをリセット
            blueTimeCount = 0;

            //通常の鼓動に戻る
            heart.GetComponent<HeartUIScript>().NormalHeartBeat();
        }
    }

    //黄ゴーストに当たった時のバフデバフ効果

    private void HitYellowGhost()
    {
        //タイムカウント
        yellowTimeCount += Time.deltaTime;

        //停止効果時間内は
        if (yellowTimeCount <= electricshockTime)
        {
            //黄色にぶつかっている
            isYellow = true;

            //心臓の鼓動が止まるアニメーション
            heart.GetComponent<HeartUIScript>().StopHeartBeat();
        }
        //停止効果時間が終了すると
        else if (yellowTimeCount > electricshockTime)
        {
            //速度を元の速度に戻す
            walkSpeed = defaultSpeed;

            //衝突判定リセット
            isYellow = false;

            //タイムカウントをリセット
            yellowTimeCount = 0;

            //通常の鼓動に戻る
            heart.GetComponent<HeartUIScript>().NormalHeartBeat();
        }
    }

    //白ゴーストに当たった時のバフデバフ効果
    private void HitWhiteGhost()
    {
        //タイムカウント
        whiteTimeCount += Time.deltaTime;

        //スピードアップ効果時間内は
        if (whiteTimeCount <= buffTime)
        {
            //スピードアップ効果により加速
            walkSpeed = buffSpeed;

            //心臓の鼓動が早くなるアニメーション
            heart.GetComponent<HeartUIScript>().HighHeartBeat();
        }
        //スピードアップ効果が終了すると
        else if (whiteTimeCount > buffTime)
        {
            //速度を元の速度に戻す
            walkSpeed = defaultSpeed;

            //衝突判定リセット
            isWhite = false;

            //タイムカウントをリセット
            whiteTimeCount = 0;

            //通常の鼓動に戻る
            heart.GetComponent<HeartUIScript>().NormalHeartBeat();
        }
    }

    //黒ゴーストに当たった時のバフデバフ効果
    private void HitBlackGhost()
    {
        //タイムカウント
        blackTimeCount += Time.deltaTime;

        //ブラインド効果時間内なら
        if (blackTimeCount <= blindTime)
        {
            //ライトが消える
            spotLight.SetActive(false);
            directionalLight.SetActive(false);
        }
        //ブラインド効果時間が終了すると
        else if (blackTimeCount > blindTime)
        {
            //ライトが点く
            spotLight.SetActive(true);
            directionalLight.SetActive(true);

            //衝突判定リセット
            isBlack = false;

            //タイムカウントをリセット
            blackTimeCount = 0;
        }
    }
}
