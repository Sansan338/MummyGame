using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManegerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject DeathUI;        //���SUI
    [SerializeField]
    private GameObject TimeUI;         //���ݎ���UI
    [SerializeField]
    private GameObject ScoreUI;        //���U���g�X�R�AUI
    [SerializeField]
    private GameObject HeartUI;        //�S��UI
    [SerializeField]
    private GameObject BonusScoreUI;   //�{�[�i�X�X�R�AUI
    [SerializeField]
    private GameObject FallSpeedUpUI;  //���e�����Ԋu�X�s�[�h�A�b�vUI
    [SerializeField]
    private GameObject SpawnSpeedUI;   //�S�[�X�g�X�|�[���X�s�[�h�A�b�vUI

    [SerializeField]
    Animator fallSpeedUpAnimator;
    [SerializeField]
    Animator spawnSpeedUpAnimator;

    private int deathUIwaitTime = 1;   //���S��Ɏ��SUI��\������܂ł̑ҋ@����
    private int speedUpUIwaitTime = 4; //�X�s�[�h�A�b�v��\���܂ł̎���

    void Start()
    {
        //�Q�[���J�n���͎��SUI�ƃ��U���g�X�R�AUI�͔�\��
        DeathUI.SetActive(false);
        ScoreUI.SetActive(false);
        //�Q�[���J�n���Ɍ��ݎ���UI�ƐS��UI�A�{�[�i�X�X�R�AUI�A�����X�s�[�h�㏸UI�A�X�|�[���X�s�[�h�㏸UI�͗L����
        TimeUI.SetActive(true);
        HeartUI.SetActive(true);
        BonusScoreUI.SetActive(true);
        FallSpeedUpUI.SetActive(true);
        SpawnSpeedUI.SetActive(true);
        //�X�s�[�h�A�b�v�㏸UI�̃A�j���[�^�[�𖳌���Ԃɂ��Ă���
        fallSpeedUpAnimator.SetBool("isSpeedUp", false);
        spawnSpeedUpAnimator.SetBool("isSpeedUp", false);
    }

    void Update()
    {
        //�Q�[���X�e�[�^�X���Q�[���I�[�o�[��ԂɂȂ��
        if(GameManager.gameStatus == GameManager.GameStatus.GameOver)
        {
            //���ݎ���UI�ƐS��UI�A�{�[�i�X�X�R�AUI�𖳌���
            TimeUI.SetActive(false);
            HeartUI.SetActive(false);
            BonusScoreUI.SetActive(false );
            //deathUIwaitTime�b���DeathUI��\��
            Invoke("ShowDeathUI", deathUIwaitTime);
            //�X�s�[�h�A�b�v�㏸UI�𖳌���
            FallSpeedUpUI.SetActive(false);
            SpawnSpeedUI.SetActive(false);
        }
        //�Q�[���X�e�[�^�X�����U���g��ԂɂȂ��
        else if(GameManager.gameStatus == GameManager.GameStatus.Result)
        {
            //���U���g�X�R�AUI��L����
            ScoreUI.SetActive(true);
        }

        //���e�����Ԋu�X�s�[�h�A�b�vUI�\��
        if (BombAppearanceScript.isSpeedUp == true)
        {
            fallSpeedUpAnimator.SetBool("isSpeedUp", true);
            Invoke("HiddenFallSpeedUpUI", speedUpUIwaitTime);
        }
        //�S�[�X�g�X�|�[���Ԋu�X�s�[�h�A�b�vUI�\��
        if (MobSpawnScript.isSpeedUp == true)
        {
            spawnSpeedUpAnimator.SetBool("isSpeedUp", true);
            Invoke("HiddenSpawnSpeedUpUI", speedUpUIwaitTime);
        }
    }

    private void ShowDeathUI()
    {
        //���SUI��L����
        DeathUI.SetActive(true);
    }

    private void HiddenFallSpeedUpUI()
    {
        //�X�s�[�h�A�b�vUI���\��
        fallSpeedUpAnimator.SetBool("isSpeedUp", false);
        BombAppearanceScript.isSpeedUp = false;
    }
    private void HiddenSpawnSpeedUpUI()
    {
        //�X�s�[�h�A�b�vUI���\��
        spawnSpeedUpAnimator.SetBool("isSpeedUp", false);
        MobSpawnScript.isSpeedUp = false;
    }
}
