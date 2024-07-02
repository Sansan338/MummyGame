using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultTotalScoreUIScript : MonoBehaviour
{
    [SerializeField]
    Text totalScoreText;

    private int hoursBonus = 1000;     //�����������Ԃɉ��Z����X�R�A�{��
    private int minutesBonus = 10;     //�������������ɉ��Z����X�R�A�{��
    private int timeScore;             //�������Ԃ݂̂̃X�R�A
    private int oneHour = 60;          //1���Ԃ�60�b

    private float additionalMagnification;     //�X�R�A���Z�{��(1���Ԑ����邲��)

    private int hitCount = 0;            //�S�[�X�g�ɓ�����������
    private int minusScore = 0;          //�S�[�X�g�ɐG�ꂽ���Ƃɂ��}�C�i�X�X�R�A
    private int minusMagnification = 50;

    private int killScore = 0;           //�L���X�R�A
    private int escapeRedBonus = 200;
    private int killMobBonus = 50;

    private float totalScore;            //���_���_�������ʂ̃g�[�^���X�R�A
    void Update()
    {
        //�S�[�X�g�ɓ�����������
        hitCount = PlayerScript.redCount + PlayerScript.blueCount + PlayerScript.yellowCount +PlayerScript.whiteCount + PlayerScript.blackCount;

        //�}�C�i�X�X�R�A = �S�[�X�g�ɓ����������� * 50
        minusScore = hitCount * minusMagnification;

        //�g�[�^���X�R�A�{�� = 1 + (������������ / 10)
        additionalMagnification = ResultAdditionalMagnificationScript.magnification;

        //�����X�R�A = (������������ �~ 1000) + (60 �~ ������������ + ������������) �~ 10
        timeScore = ResultSurvivalTimeUIScript.hours * hoursBonus + (oneHour * ResultSurvivalTimeUIScript.hours+(int)TimeUIScript.minutesCount) * minutesBonus;

        //�L���X�R�A = �Ԃ�|������ �~ 200 + �ԈȊO��|������ �~ 50
        killScore = (RedGhostScript.escapeRedCount * escapeRedBonus) + (BombScript.killMobCount * killMobBonus);

        //�g�[�^���X�R�A = �^�C���X�R�A �~ �g�[�^���X�R�A�{��
        totalScore = (timeScore - minusScore + killScore) * additionalMagnification;

        //�g�[�^���X�R�A��\��
        totalScoreText.text = Mathf.Floor(totalScore).ToString();
    }
}
