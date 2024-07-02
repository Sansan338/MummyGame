using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameStatus { Play, GameOver, Result, Next}     //�Q�[���X�e�[�^�X(�v���C��ԁA�Q�[���I�[�o�[��ԁA���U���g���)
    public static GameStatus gameStatus;

    private int waitTime = 3;      //�Q�[���I�[�o�[��Ƀ��U���g��ʂֈȍ~����܂ł̑ҋ@����

    void Start()
    {
        //�Q�[���J�n���̓Q�[���X�e�[�^�X���v���C���ɐݒ�
        gameStatus = GameStatus.Play;
    }

    void Update()
    {
        //�Q�[���I�[�o�[��ԂɂȂ��
        if(gameStatus == GameStatus.GameOver)
        {
            //waitTime�b�҂��Ă��烊�U���g��ʂɈڍs����
            Invoke("TransferResult", waitTime);
        }
    }

    private void TransferResult()
    {
        //�Q�[���X�e�[�^�X�����U���g��Ԃɂ���
        gameStatus = GameStatus.Result;
    }
}
