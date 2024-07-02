using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawnScript : MonoBehaviour
{
    [SerializeField]
    private Transform rangeA;     //�S�[�X�g�������G���A�͈̔�
    [SerializeField]
    private Transform rangeB;     //�S�[�X�g�������G���A�͈̔�
    [SerializeField]
    int appearanceHeight = 2;     //�S�[�X�g����������

    public GameObject[] mob;      //�S�[�X�g�̃Q�[���I�u�W�F�N�g
    int mobNumber = 0;

    public float minspawnSpan = 15.0f;        //�S�[�X�g�������X�p���ő�
    public float spawnSpan = 30.0f;           //�S�[�X�g�������X�p��
    public static bool isSpeedUp = false;     �@�@�@�@//�X�|�[���X�s�[�h���㏸������
    public float minusSpawnSpeed = 0.5f;      //�X�|�[���X�s�[�h�A�b�v��
    public float spawnSpeedUpTime = 60.0f;    //�X�|�[���X�s�[�h�A�b�v����܂ł̎���
    float time = 0.0f;                 //�o�ߎ��ԃJ�E���g
    float spawnTime = 0.0f;            //�X�|�[�����ԃJ�E���g

    private void Start()
    {
        //�S�[�X�g�������͈͂������_���ɐݒ�
        float mobSpawnX = Random.Range(rangeA.position.x, rangeB.position.x + 1);
        int mobSpawnY = appearanceHeight;
        float mobSpawnZ = Random.Range(rangeA.position.z, rangeB.position.z + 1);
        //�����S�[�X�g�������_���Ɏw��
        mobNumber = Random.Range(0, mob.Length);
        //�S�[�X�g�𐶐�
        Instantiate(mob[mobNumber], new Vector3(mobSpawnX, mobSpawnY, mobSpawnZ), Quaternion.identity);
    }

    void Update()
    {
        //�Q�[�����v���C���Ȃ�
        if (GameManager.gameStatus == GameManager.GameStatus.Play)
        {
            //�S�[�X�g�������͈͂������_���ɐݒ�
            float mobSpawnX = Random.Range(rangeA.position.x, rangeB.position.x + 1);
            int mobSpawnY = appearanceHeight;
            float mobSpawnZ = Random.Range(rangeA.position.z, rangeB.position.z + 1);

            //���Ԃ��J�E���g
            time += Time.deltaTime;
            spawnTime += Time.deltaTime;

            if(time >= spawnSpeedUpTime)
            {
                //�X�|�[���X�p�����Z���Ȃ�
                spawnSpan -= minusSpawnSpeed;
                isSpeedUp = true;
                //�^�C���J�E���g�����Z�b�g
                time = 0;
            }

            if (spawnTime > spawnSpan)
            {
                //�����S�[�X�g�������_���Ɏw��
                mobNumber = Random.Range(0, mob.Length);
                //�S�[�X�g�𐶐�
                Instantiate(mob[mobNumber], new Vector3(mobSpawnX, mobSpawnY, mobSpawnZ), Quaternion.identity);
                //�^�C���J�E���g�����Z�b�g
                spawnTime = 0;
            }
        }
    }
}
