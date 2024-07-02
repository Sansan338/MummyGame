using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCounterScript : MonoBehaviour
{
    public static int hitBlueCount = 0;        //青ゴーストに当たったカウント
    public static int hitYelowCount = 0;       //黄ゴーストに当たったカウント
    public static int hitWhiteCount = 0;       //白ゴーストに当たったカウント
    public static int hitBlackCount = 0;       //黒ゴーストに当たったカウント

    private void OnCollisionEnter(Collision collision)
    {
        //青ゴーストに当たると
        if(collision.gameObject.tag == "Blue")
        {
            hitBlueCount++;
        }
        //黄ゴーストに当たると
        else if(collision.gameObject.tag == "Yelow")
        {
            hitYelowCount++;
        }
        //白ゴーストに当たると
        else if(collision.gameObject.tag == "White")
        {
            hitWhiteCount++;
        }
        //黒ゴーストに当たると
        else if(collision.gameObject.tag == "Black")
        {
            hitBlackCount++;
        }
    }
}
