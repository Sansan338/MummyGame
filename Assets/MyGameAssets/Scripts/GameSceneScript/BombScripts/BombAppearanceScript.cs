using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAppearanceScript : MonoBehaviour
{
    [SerializeField]
    int AppearanceWidth = 12;        //爆弾生成エリアの幅
    [SerializeField]
    int appearanceHeight = 20;       //爆弾生成位置の高さ
    [SerializeField]
    GameObject bomb;                 //爆弾のゲームオブジェクト

    public static int bombCount = 0;            //生成された爆弾の個数
    public static bool isSpeedUp = false;       //落下スピードが上昇したか
    private float appearanceTime = 0.0f;        //爆弾生成時間
    private float time = 0.0f;                  //経過時間
    public float appearanceSpan = 3f;           //爆弾生成のスパン
    public float minAppearanceSpan = 0.4f;      //最低爆弾生成スパン
    public float minusSpanSpeed = 0.2f;         //爆弾生成スパンのスピードアップ量
    public float spanSpeedUpTime = 30.0f;       //爆弾生成スパンのスピードアップするまでの時間

    void Update()
    {
        //ゲームがプレイ中なら
        if (GameManager.gameStatus == GameManager.GameStatus.Play)
        {
            //爆弾を生成する座標をランダムに
            int bombAppearanceX = Random.Range(-AppearanceWidth, AppearanceWidth + 1);
            int bombAppearanceY = appearanceHeight;
            int bombAppearanceZ = Random.Range(-AppearanceWidth, AppearanceWidth + 1);

            //時間をカウント
            time += Time.deltaTime;
            appearanceTime += Time.deltaTime;

            if(appearanceSpan >= minAppearanceSpan && time >= spanSpeedUpTime)
            {
                //爆弾生成スパンを短くする
                appearanceSpan -= minusSpanSpeed;
                isSpeedUp = true;

                //時間カウントをリセット
                time = 0;
            }

            if (appearanceTime >= appearanceSpan)
            {
                //爆弾を生成
                Instantiate(bomb, new Vector3(bombAppearanceX, bombAppearanceY, bombAppearanceZ), Quaternion.identity);
                bombCount++;

                //時間カウントをリセット
                appearanceTime = 0;
            }
        }
    }
}
