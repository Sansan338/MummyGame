using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePlayerScript : MonoBehaviour
{
    [SerializeField]
    private Animator animator;     //プレイヤーのアニメーター
    
    void Start()
    {
        //歩きアニメーションを有効化
        animator.SetBool("isMoving", true);
    }
}
