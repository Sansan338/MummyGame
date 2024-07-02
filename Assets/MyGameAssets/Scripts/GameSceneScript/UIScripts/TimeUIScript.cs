using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUIScript : MonoBehaviour
{
    public static float minutesCount;      //�����̃J�E���g
    private float oldMinutesCount;         //��r�p�����J�E���g
    public static int hoursCount;          //���Ԃ̃J�E���g

    private float oneHours = 60.0f;        //1���Ԃ�60��
    private int oneDay = 24;               //1����24����

    public Text minutesText;
    public Text hoursText;

    // Start is called before the first frame update
    void Start()
    {
        //���Ԃ̃J�E���g��������
        minutesCount = 0.0f;
        oldMinutesCount = 0.0f;
        hoursCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //�J�E���g�A�b�v
        minutesCount += Time.deltaTime;

        //60��(�������E�ł�60�b)�o�߂����
        if (minutesCount >= oneHours)
        {
            //1���Ԍo��
            hoursCount++;

            //���������Z�b�g
            minutesCount -= oneHours;
        }

        //24����(�������E�ł�24��)�o�߂����
        if (hoursCount >= oneDay)
        {
            //���Ԃ����Z�b�g
            hoursCount -= oneDay;
        }
        if ((int)minutesCount != (int)oldMinutesCount)
        {
            //�\��
            minutesText.text = minutesCount.ToString("F0").PadLeft(2,'0');
            hoursText.text = hoursCount.ToString();
        }
        //�����̗]��̕������c���Ă���
        oldMinutesCount = minutesCount;
        
    }
}
