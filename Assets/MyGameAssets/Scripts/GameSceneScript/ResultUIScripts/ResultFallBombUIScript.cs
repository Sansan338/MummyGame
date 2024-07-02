using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultFallBombUIScript : MonoBehaviour
{
    private int bombCount = 0;

    [SerializeField]
    Text bombCountText;

    void Update()
    {
        bombCount = BombAppearanceScript.bombCount;
        bombCountText.text = bombCount.ToString();
    }
}
