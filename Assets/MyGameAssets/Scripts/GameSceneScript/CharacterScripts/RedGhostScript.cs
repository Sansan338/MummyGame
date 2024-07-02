using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;

public class RedGhostScript : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;     //�v���C���[�̈ʒu���
    [SerializeField]
    private GameObject deathEffect;        //���S���̃G�t�F�N�g
    [SerializeField]
    private GameObject escapeEffect;       //�Q�[���I�[�o�[���̃S�[�X�g���������G�t�F�N�g
    [SerializeField]
    private Animator ghostAnimator;        //�S�[�X�g�̃A�j���[�^�[
    [SerializeField]
    private NavMeshAgent agent;�@�@�@�@�@�@//NavMeshAgent

    private bool isDie = false;            //�S�[�X�g�͎���ł��܂���

    private float timeCount;               //�^�C���J�E���g
    public int lifeSpan = 90;              //�ԃS�[�X�g�̐�������
    public static int escapeRedCount;             //�ԃS�[�X�g�̋��Ђ��瓦�ꂽ��
    void Start()
    {
        //�J�E���g���Z�b�g
        timeCount = 0;
        escapeRedCount = 0;

        //�v���C���[�̍��W���擾
        playerTransform = GameObject.Find("Player").transform;
    }

    void Update()
    {
        //���Ԃ��J�E���g
        timeCount += Time.deltaTime;
        //�S�[�X�g�����݂��Ă��邩�Q�[�����v���C���Ȃ�
        if (isDie == false && GameManager.gameStatus == GameManager.GameStatus.Play)
        {
            //�A�j���[�V������L����
            ghostAnimator.SetBool("isMoving", true);
            //�S�[�X�g�̖ړI�n���v���C���[�̌��ݍ��W��
            agent.destination = playerTransform.position;
        }
        //�Q�[���I�[�o�[�ɂȂ�����
        if (GameManager.gameStatus == GameManager.GameStatus.GameOver)
        {
            //�S�[�X�g��������G�t�F�N�g�𐶐�
            Instantiate(escapeEffect, this.transform.position, Quaternion.Euler(-90, 0, 0));
            //�S�[�X�g�I�u�W�F�N�g��j��
            Destroy(this.gameObject);
        }

        //�ԃS�[�X�g�������Ă���������}�����
        if(timeCount >= lifeSpan)
        {
            //�S�[�X�g�̎��S�G�t�F�N�g�𐶐�
            Instantiate(escapeEffect, this.transform.position, Quaternion.Euler(-90,0,0));
            //�S�[�X�g�̃I�u�W�F�N�g��j��
            Destroy(this.gameObject);
            //�S�[�X�g�͎���ł��܂����@-->�͂�
            isDie = true;
            escapeRedCount++;
            timeCount = 0;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        //�v���C���[�ɓ������
        if (collider.gameObject.tag == "Player")
        {
            //�S�[�X�g�̎��S�G�t�F�N�g�𐶐�
            Instantiate(deathEffect, this.transform.position, Quaternion.identity);
            //�S�[�X�g�̃I�u�W�F�N�g��j��
            Destroy(this.gameObject);
            //�S�[�X�g�͎���ł��܂����@-->�͂�
            isDie = true;
        }
    }
}
