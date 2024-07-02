using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawnScript : MonoBehaviour
{
    [SerializeField]
    private Transform rangeA;     //ゴーストが沸くエリアの範囲
    [SerializeField]
    private Transform rangeB;     //ゴーストが沸くエリアの範囲
    [SerializeField]
    int appearanceHeight = 2;     //ゴーストが沸く高さ

    public GameObject[] mob;      //ゴーストのゲームオブジェクト
    int mobNumber = 0;

    public float minspawnSpan = 15.0f;        //ゴーストが沸くスパン最速
    public float spawnSpan = 30.0f;           //ゴーストが沸くスパン
    public static bool isSpeedUp = false;     　　　　//スポーンスピードが上昇したか
    public float minusSpawnSpeed = 0.5f;      //スポーンスピードアップ量
    public float spawnSpeedUpTime = 60.0f;    //スポーンスピードアップするまでの時間
    float time = 0.0f;                 //経過時間カウント
    float spawnTime = 0.0f;            //スポーン時間カウント

    private void Start()
    {
        //ゴーストが沸く範囲をランダムに設定
        float mobSpawnX = Random.Range(rangeA.position.x, rangeB.position.x + 1);
        int mobSpawnY = appearanceHeight;
        float mobSpawnZ = Random.Range(rangeA.position.z, rangeB.position.z + 1);
        //沸くゴーストをランダムに指定
        mobNumber = Random.Range(0, mob.Length);
        //ゴーストを生成
        Instantiate(mob[mobNumber], new Vector3(mobSpawnX, mobSpawnY, mobSpawnZ), Quaternion.identity);
    }

    void Update()
    {
        //ゲームがプレイ中なら
        if (GameManager.gameStatus == GameManager.GameStatus.Play)
        {
            //ゴーストが沸く範囲をランダムに設定
            float mobSpawnX = Random.Range(rangeA.position.x, rangeB.position.x + 1);
            int mobSpawnY = appearanceHeight;
            float mobSpawnZ = Random.Range(rangeA.position.z, rangeB.position.z + 1);

            //時間をカウント
            time += Time.deltaTime;
            spawnTime += Time.deltaTime;

            if(time >= spawnSpeedUpTime)
            {
                //スポーンスパンが短くなる
                spawnSpan -= minusSpawnSpeed;
                isSpeedUp = true;
                //タイムカウントをリセット
                time = 0;
            }

            if (spawnTime > spawnSpan)
            {
                //沸くゴーストをランダムに指定
                mobNumber = Random.Range(0, mob.Length);
                //ゴーストを生成
                Instantiate(mob[mobNumber], new Vector3(mobSpawnX, mobSpawnY, mobSpawnZ), Quaternion.identity);
                //タイムカウントをリセット
                spawnTime = 0;
            }
        }
    }
}
