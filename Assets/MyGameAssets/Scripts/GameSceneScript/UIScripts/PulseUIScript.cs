using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PulseUIScript : MonoBehaviour
{
    [SerializeField]
    private Slider hpSlider;     //hp�o�[

    private int hp;              //HP

    void Update()
    {
        //�v���C���[��HP���擾
        hp = PlayerScript.playerHP;
        //�o�[��HP�ƍ��킹��
        hpSlider.value = hp;
    }
}
