using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PulseUIScript : MonoBehaviour
{
    [SerializeField]
    private Slider hpSlider;     //hpバー

    private int hp;              //HP

    void Update()
    {
        //プレイヤーのHPを取得
        hp = PlayerScript.playerHP;
        //バーをHPと合わせる
        hpSlider.value = hp;
    }
}
