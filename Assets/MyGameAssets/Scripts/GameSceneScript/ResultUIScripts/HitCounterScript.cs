using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCounterScript : MonoBehaviour
{
    public static int hitBlueCount = 0;        //�S�[�X�g�ɓ��������J�E���g
    public static int hitYelowCount = 0;       //���S�[�X�g�ɓ��������J�E���g
    public static int hitWhiteCount = 0;       //���S�[�X�g�ɓ��������J�E���g
    public static int hitBlackCount = 0;       //���S�[�X�g�ɓ��������J�E���g

    private void OnCollisionEnter(Collision collision)
    {
        //�S�[�X�g�ɓ������
        if(collision.gameObject.tag == "Blue")
        {
            hitBlueCount++;
        }
        //���S�[�X�g�ɓ������
        else if(collision.gameObject.tag == "Yelow")
        {
            hitYelowCount++;
        }
        //���S�[�X�g�ɓ������
        else if(collision.gameObject.tag == "White")
        {
            hitWhiteCount++;
        }
        //���S�[�X�g�ɓ������
        else if(collision.gameObject.tag == "Black")
        {
            hitBlackCount++;
        }
    }
}
