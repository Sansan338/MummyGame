using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    [SerializeField]
    GameObject explosionEffect;       //�����G�t�F�N�g
    [SerializeField]
    AudioClip explosionSound;         //�����T�E���h
    
    public bool isBreak = false;      //��ꂽ�H

    public static int killMobCount = 0;           //���u��|������

    void Update()
    {
        //���e����ꂽ�Ȃ�
        if(isBreak == true )
        {
            //�����T�E���h���Đ�
            AudioSource.PlayClipAtPoint(explosionSound, this.transform.position);
            //�����G�t�F�N�g�𐶐�
            Instantiate(explosionEffect, this.transform.position, Quaternion.identity);
            //���e��j��
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        //�n��or�v���C���[�ɓ������
        if (collider.gameObject.tag == "Ground" || collider.gameObject.tag == "Player" )
        {
            //��ꂽ�H --> �͂�
           isBreak = true;
        }

        //�ԈȊO�̃S�[�X�g��|���������J�E���g
        if(collider.gameObject.tag == "Blue" || collider.gameObject.tag == "Yellow" 
            || collider.gameObject.tag == "White" || collider.gameObject.tag == "Black")
        {
            killMobCount++;
        }
    }
}
