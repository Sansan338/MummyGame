using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultAdditionalMagnificationScript : MonoBehaviour
{
    [SerializeField]
    Text magnificationText;

    private int deffaultMagnification = 1;       //�f�t�H���g�X�R�A�{��
    private int divideNumber = 10;               //�X�R�A�{�����v�Z���邽�߂Ɋ��鐔
    public static float magnification;           //�X�R�A���Z�{��(1���Ԑ����邲��)

    void Update()
    {
        //�g�[�^���X�R�A�{�� = 1 + (������������ / 10)
        magnification = deffaultMagnification + (float)ResultSurvivalTimeUIScript.hours / divideNumber;
        magnificationText.text = magnification.ToString("n1");
    }
}
