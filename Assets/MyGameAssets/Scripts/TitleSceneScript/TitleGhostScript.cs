using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleGhostScript : MonoBehaviour
{
    [SerializeField]
    private Animator animator;     //�S�[�X�g�̃A�j���[�^�[��L����

    void Start()
    {
        //�ړ��A�j���[�V������L����
        animator.SetBool("isMoving", true);
    }
}
