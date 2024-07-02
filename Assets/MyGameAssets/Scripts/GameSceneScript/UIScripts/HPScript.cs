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
        //プレイヤーのHPを取得
        hp = PlayerScript.playerHP;
        //HPを表示
        hpText.text = hp.ToString();
    }
}
