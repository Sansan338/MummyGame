using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPScript : MonoBehaviour
{
    [SerializeField]
    private Text hpText;       //HPText

    int hp;                    //HP
    
    void Update()
    {
        //�v���C���[��HP���擾
        hp = PlayerScript.playerHP;
        //HP��\��
        hpText.text = hp.ToString();
    }
}
