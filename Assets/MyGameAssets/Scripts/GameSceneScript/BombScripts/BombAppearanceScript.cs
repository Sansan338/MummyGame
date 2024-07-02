using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAppearanceScript : MonoBehaviour
{
    [SerializeField]
    int AppearanceWidth = 12;        //���e�����G���A�̕�
    [SerializeField]
    int appearanceHeight = 20;       //���e�����ʒu�̍���
    [SerializeField]
    GameObject bomb;                 //���e�̃Q�[���I�u�W�F�N�g

    public static int bombCount = 0;            //�������ꂽ���e�̌�
    public static bool isSpeedUp = false;       //�����X�s�[�h���㏸������
    private float appearanceTime = 0.0f;        //���e��������
    private float time = 0.0f;                  //�o�ߎ���
    public float appearanceSpan = 3f;           //���e�����̃X�p��
    public float minAppearanceSpan = 0.4f;      //�Œᔚ�e�����X�p��
    public float minusSpanSpeed = 0.2f;         //���e�����X�p���̃X�s�[�h�A�b�v��
    public float spanSpeedUpTime = 30.0f;       //���e�����X�p���̃X�s�[�h�A�b�v����܂ł̎���

    void Update()
    {
        //�Q�[�����v���C���Ȃ�
        if (GameManager.gameStatus == GameManager.GameStatus.Play)
        {
            //���e�𐶐�������W�������_����
            int bombAppearanceX = Random.Range(-AppearanceWidth, AppearanceWidth + 1);
            int bombAppearanceY = appearanceHeight;
            int bombAppearanceZ = Random.Range(-AppearanceWidth, AppearanceWidth + 1);

            //���Ԃ��J�E���g
            time += Time.deltaTime;
            appearanceTime += Time.deltaTime;

            if(appearanceSpan >= minAppearanceSpan && time >= spanSpeedUpTime)
            {
                //���e�����X�p����Z������
                appearanceSpan -= minusSpanSpeed;
                isSpeedUp = true;

                //���ԃJ�E���g�����Z�b�g
                time = 0;
            }

            if (appearanceTime >= appearanceSpan)
            {
                //���e�𐶐�
                Instantiate(bomb, new Vector3(bombAppearanceX, bombAppearanceY, bombAppearanceZ), Quaternion.identity);
                bombCount++;

                //���ԃJ�E���g�����Z�b�g
                appearanceTime = 0;
            }
        }
    }
}
