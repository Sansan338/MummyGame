using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePlayerScript : MonoBehaviour
{
    [SerializeField]
    private Animator animator;     //�v���C���[�̃A�j���[�^�[
    
    void Start()
    {
        //�����A�j���[�V������L����
        animator.SetBool("isMoving", true);
    }
}
