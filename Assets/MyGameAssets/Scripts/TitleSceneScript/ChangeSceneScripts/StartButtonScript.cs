using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonScript : MonoBehaviour
{
    [SerializeField]
    GameObject Image;     //フェードアウト画像ゲームオブジェクト

    private int fadeTime = 3;  //フェードアウト時間

    public void Start()
    {
        //フェードアウト画像を無効状態に
        Image.SetActive(false);
    }
    private void StartGameButton()
    {
        //ゲームシーンを読み込む
        SceneManager.LoadScene("GameScene");
    }

    public void StartFade()
    {
        //フェードアウト画像を有効化
        Image.SetActive(true);
        //Startボタンを押されてからfadeTime秒後にゲームシーンを読み込む
        Invoke("StartGameButton", fadeTime);
    }
}
