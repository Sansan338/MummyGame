using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrictedAreaScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        //�v���C���[���G���A�ɐN��������
        if (collision.gameObject.tag == "Player")
        {
            //����ł��܂��� --> �͂�
            PlayerScript.isDie = true;

            //�Q�[���I�[�o�[�ɂȂ�
            GameManager.gameStatus = GameManager.GameStatus.GameOver;
        }
    }
}
