using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUIScript : MonoBehaviour
{
    [SerializeField]
    Animator heartAnimator;                     //�S���̃A�j���[�^�[

    private float maxplayerHP = 100.0f;         //�v���C���[�̍�HP
    private float deffaultHeartSpeed = 1.0f;    //�f�t�H���g�̐S�����x

    //���i�̐S���ɐݒ�
    public void NormalHeartBeat()
    {
        heartAnimator.SetBool("isNormal", true);
        heartAnimator.SetBool("isSlow", false);
        heartAnimator.SetBool("isHigh", false);
        heartAnimator.SetBool("isStop", false);
    }

    //�S�[�X�g�ɓ����������̐S���ɐݒ�
    public void SlowHeartBeat()
    {
        heartAnimator.SetBool("isSlow", true);
        heartAnimator.SetBool("isNormal", false);
        heartAnimator.SetBool("isHigh", false);
        heartAnimator.SetBool("isStop", false);
    }

    //���S�[�X�g�ɓ����������̐S���ɐݒ�
    public void StopHeartBeat()
    {
        heartAnimator.SetBool("isStop", true);
        heartAnimator.SetBool("isNormal", false);
        heartAnimator.SetBool("isSlow", false);
        heartAnimator.SetBool("isHigh", false);
    }

    //���S�[�X�g�ɓ����������̐S���ɐݒ�
    public void HighHeartBeat()
    {
        heartAnimator.SetBool("isHigh", true);
        heartAnimator.SetBool("isNormal", false);
        heartAnimator.SetBool("isSlow", false);
        heartAnimator.SetBool("isStop", false);
    }

    public void HeartSpeed()
    {
        //�v���C���[��HP����������ƐS���������Ȃ�
        heartAnimator.SetFloat("Speed",deffaultHeartSpeed + (maxplayerHP - PlayerScript.playerHP) / 100);
    }
}
