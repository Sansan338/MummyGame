using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    [SerializeField]
    Animator playerAnimator;

    //プレイヤー関連スクリプト
    public PlayerMoveScript playerMoveScript;
    public PlayerScript playerScript;

    //プレイヤーのステータス
    public float WalkSpeed { get; private set; } = 8.0f;     //歩く速度
 
    public float DefaultSpeed { get; private set; } = 8.0f;     //歩く速度を元に戻すための
  
    public float BuffSpeed { get; private set; } = 11.0f;     //バフ状態速度
   　
    public float SlowSpeed { get; private set; } = 5.0f;     //スロウ状態速度
 
    public float RotateSpeed { get; private set; } = 10.0f;     //回転速度

    Vector3 moveDirection;       //座標
    void Update()
    {
        //プレイヤー情報の取得
        float walkSpeed = playerMoveScript.WalkSpeed;
        float rotateSpeed = playerMoveScript.RotateSpeed;
        bool isYellow = playerScript.IsYellow;

        //WASDもしくは矢印キーの入力情報を取得
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        moveDirection = new Vector3(moveX, 0, moveZ);
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
    }
}
