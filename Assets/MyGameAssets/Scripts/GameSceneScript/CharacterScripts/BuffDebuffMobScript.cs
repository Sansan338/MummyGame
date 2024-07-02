using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuffDebuffMobScript : MonoBehaviour
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

    void Start()
    {
        //�v���C���[�̍��W���擾
        playerTransform = GameObject.Find("Player").transform;
    }

    void Update()
    {
        //�S�[�X�g�����݂��Ă��邩�Q�[�����v���C���Ȃ�
        if (isDie == false && GameManager.gameStatus == GameManager.GameStatus.Play)
        {
            //�A�j���[�V������L����
            ghostAnimator.SetBool("isMoving", true);
            //�S�[�X�g�̖ړI�n���v���C���[�̌��ݍ��W��
            agent.destination = playerTransform.position;
        }
        //�Q�[���I�[�o�[�ɂȂ�����
        if(GameManager.gameStatus == GameManager.GameStatus.GameOver)
        {
            //�S�[�X�g��������G�t�F�N�g�𐶐�
            Instantiate(escapeEffect, this.transform.position, Quaternion.Euler(-90,0,0));
            //�S�[�X�g�I�u�W�F�N�g��j��
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        //�S�[�X�g�����e���v���C���[�ɓ������
        if (collider.gameObject.tag == "Bomb" || collider.gameObject.tag == "Player")
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