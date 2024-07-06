using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    [SerializeField]
    Animator playerAnimator;

    //�v���C���[�֘A�X�N���v�g
    public PlayerMoveScript playerMoveScript;
    public PlayerScript playerScript;

    //�v���C���[�̃X�e�[�^�X
    public float WalkSpeed { get; private set; } = 8.0f;     //�������x
 
    public float DefaultSpeed { get; private set; } = 8.0f;     //�������x�����ɖ߂����߂�
  
    public float BuffSpeed { get; private set; } = 11.0f;     //�o�t��ԑ��x
   �@
    public float SlowSpeed { get; private set; } = 5.0f;     //�X���E��ԑ��x
 
    public float RotateSpeed { get; private set; } = 10.0f;     //��]���x

    Vector3 moveDirection;       //���W
    void Update()
    {
        //�v���C���[���̎擾
        float walkSpeed = playerMoveScript.WalkSpeed;
        float rotateSpeed = playerMoveScript.RotateSpeed;
        bool isYellow = playerScript.IsYellow;

        //WASD�������͖��L�[�̓��͏����擾
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        moveDirection = new Vector3(moveX, 0, moveZ);
        //���K��
        moveDirection.Normalize();
        //���̌��ʎ��ԈȊO�͏�Ɉړ��\
        if (isYellow == false)
        {
            transform.position += moveDirection * walkSpeed * Time.deltaTime;
        }
        //�v���C���[���ړ�
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
        //�v���C���[���ړ����Ă���Ȃ�
        if (moveZ != 0 || moveX != 0)
        {
            //�����A�j���[�V������L����
            playerAnimator.SetBool("isMoving", true);
        }
        //�v���C���[���ړ����Ă��Ȃ��Ȃ�
        else
        {
            //�����A�j���[�V�����𖳌���
            playerAnimator.SetBool("isMoving", false);
        }
    }
}
