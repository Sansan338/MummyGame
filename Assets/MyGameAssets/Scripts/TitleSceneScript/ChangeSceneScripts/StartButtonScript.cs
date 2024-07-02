using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonScript : MonoBehaviour
{
    [SerializeField]
    GameObject Image;     //�t�F�[�h�A�E�g�摜�Q�[���I�u�W�F�N�g

    private int fadeTime = 3;  //�t�F�[�h�A�E�g����

    public void Start()
    {
        //�t�F�[�h�A�E�g�摜�𖳌���Ԃ�
        Image.SetActive(false);
    }
    private void StartGameButton()
    {
        //�Q�[���V�[����ǂݍ���
        SceneManager.LoadScene("GameScene");
    }

    public void StartFade()
    {
        //�t�F�[�h�A�E�g�摜��L����
        Image.SetActive(true);
        //Start�{�^����������Ă���fadeTime�b��ɃQ�[���V�[����ǂݍ���
        Invoke("StartGameButton", fadeTime);
    }
}
