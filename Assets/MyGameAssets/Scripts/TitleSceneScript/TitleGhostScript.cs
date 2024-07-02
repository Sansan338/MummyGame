using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleGhostScript : MonoBehaviour
{
    [SerializeField]
    private Animator animator;     //ゴーストのアニメーターを有効化

    void Start()
    {
        //移動アニメーションを有効化
        animator.SetBool("isMoving", true);
    }
}
