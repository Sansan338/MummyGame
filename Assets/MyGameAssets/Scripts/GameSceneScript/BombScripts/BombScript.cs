using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    [SerializeField]
    GameObject explosionEffect;       //爆発エフェクト
    [SerializeField]
    AudioClip explosionSound;         //爆発サウンド
    
    public bool isBreak = false;      //壊れた？

    public static int killMobCount = 0;           //モブを倒した数

    void Update()
    {
        //爆弾が壊れたなら
        if(isBreak == true )
        {
            //爆発サウンドを再生
            AudioSource.PlayClipAtPoint(explosionSound, this.transform.position);
            //爆発エフェクトを生成
            Instantiate(explosionEffect, this.transform.position, Quaternion.identity);
            //爆弾を破壊
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        //地面orプレイヤーに当たると
        if (collider.gameObject.tag == "Ground" || collider.gameObject.tag == "Player" )
        {
            //壊れた？ --> はい
           isBreak = true;
        }

        //赤以外のゴーストを倒した数をカウント
        if(collider.gameObject.tag == "Blue" || collider.gameObject.tag == "Yellow" 
            || collider.gameObject.tag == "White" || collider.gameObject.tag == "Black")
        {
            killMobCount++;
        }
    }
}
