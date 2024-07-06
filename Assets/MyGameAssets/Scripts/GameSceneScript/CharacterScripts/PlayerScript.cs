using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{
    public PlayerMoveScript playerMoveScript;

    public static int playerHP;　//プレイヤーの体力
    float p_walkSpeed;
    float p_defaultSpeed;
    float p_buffSpeed;
    float p_slowSpeed;
    float p_rotateSpeed;

    public static bool isDie;    //死んでいるか

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
    public bool IsBlue { get; set; } = false;    //青
    public bool IsYellow { get; set; } = false;   //黄色
    public bool IsBlack { get; set; } = false;   //黒
    public bool IsWhite { get; set; } = false;   //白

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
        //プレイヤーのスピードを参照
        p_walkSpeed = playerMoveScript.WalkSpeed;
        p_defaultSpeed = playerMoveScript.DefaultSpeed;

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
        if (IsBlue == true)
        {
            HitBlueGhost();
        }
        //黄
        if (IsYellow == true)
        {
            HitYellowGhost();
        }
        //白
        if (IsWhite == true)
        {
            HitWhiteGhost();
        }
        //黒
        if (IsBlack == true)
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
            IsBlue = true;

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
            IsYellow = true;

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
            IsWhite = true;

            //パーティクルを生成and再生and破壊
            BuffDebuffParticle(buffParticle,buffTime);

            //白ゴーストに当たった回数カウント
            whiteCount++;
        }
        //黒モブに当たったら
        if(collider.gameObject.tag == "Black")
        {
            //黒に当たりましたよ
            IsBlack = true;

            //黒に当たった回数カウント
            blackCount++;
        }
    }

    //プレイヤーのステータスをリセット
    private void ResetPlayerState()
    {
        //速度を元の速度に戻す
        p_walkSpeed = p_defaultSpeed;
        //衝突判定をリセット
        IsBlue = false;
        IsYellow = false;
        IsWhite = false;
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
            p_walkSpeed = p_slowSpeed;

            //心臓の鼓動が遅くなるアニメーション
            heart.GetComponent<HeartUIScript>().SlowHeartBeat();
        }
        //スロウ効果時間が終了すると
        else if (blueTimeCount > slowTime)
        {
            //速度を元の速度に戻す
            p_walkSpeed = p_defaultSpeed;

            //衝突判定リセット
            IsBlue = false;

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
            IsYellow = true;

            //心臓の鼓動が止まるアニメーション
            heart.GetComponent<HeartUIScript>().StopHeartBeat();
        }
        //停止効果時間が終了すると
        else if (yellowTimeCount > electricshockTime)
        {
            //速度を元の速度に戻す
            p_walkSpeed = p_defaultSpeed;

            //衝突判定リセット
            IsYellow = false;

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
            p_walkSpeed = p_buffSpeed;

            //心臓の鼓動が早くなるアニメーション
            heart.GetComponent<HeartUIScript>().HighHeartBeat();
        }
        //スピードアップ効果が終了すると
        else if (whiteTimeCount > buffTime)
        {
            //速度を元の速度に戻す
            p_walkSpeed = p_defaultSpeed;

            //衝突判定リセット
            IsWhite = false;

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
            IsBlack = false;

            //タイムカウントをリセット
            blackTimeCount = 0;
        }
    }
}
