using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrictedAreaScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        //プレイヤーがエリアに侵入したら
        if (collision.gameObject.tag == "Player")
        {
            //死んでいますか --> はい
            PlayerScript.isDie = true;

            //ゲームオーバーになる
            GameManager.gameStatus = GameManager.GameStatus.GameOver;
        }
    }
}
