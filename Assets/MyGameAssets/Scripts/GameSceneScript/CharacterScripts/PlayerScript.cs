using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{
    //�v���C���[�̃X�e�[�^�X
    public static int playerHP;�@//�v���C���[�̗̑�
    public float walkSpeed = 8.0f;      //�������x
    public float defaultSpeed = 8.0f;   //�������x�����ɖ߂����߂�
    public float buffSpeed = 11.0f;     //�o�t��ԑ��x
    public float slowSpeed = 5.0f;    �@//�X���E��ԑ��x
    public float rotateSpeed = 10.0f;   //��]���x
    public static bool isDie;    //����ł��邩
    Vector3 moveDirection;       //���W


    public int hitDamage = 20;          //�S�[�X�g�ɓ����������̃_���[�W

    //�o�t�f�o�t���ʎ���
    private float blueTimeCount = 0.0f;     //�S�[�X�g�ɓ����������̃^�C���J�E���g
    private float yellowTimeCount = 0.0f;   //���S�[�X�g�ɓ����������̃^�C���J�E���g
    private float whiteTimeCount = 0.0f;    //���S�[�X�g�ɓ����������̃^�C���J�E���g
    private float blackTimeCount = 0.0f;    //���S�[�X�g�ɓ����������̃^�C���J�E���g
    
    public float slowTime = 5.0f;                  //�S�[�X�g�ɓ����������̃X���E���ʎ���
    public float electricshockTime = 1.5f;         //���S�[�X�g�ɓ����������̒�~����
    public float buffTime = 5.0f;                  //���S�[�X�g�ɓ����������̑��x�A�b�v����
    public float blindTime = 2.5f;                 //���S�[�X�g�ɓ����������̃u���C���h����

    //�Փ˔���
    private bool isBlue = false;    //��
    private bool isYellow = false;   //���F
    private bool isBlack = false;   //��
    private bool isWhite = false;   //��

    //�Փ˃J�E���g
    public static int redCount;      //�Ԃɓ���������
    public static int blueCount;     //�ɉ��񓖂�������
    public static int yellowCount;   //���ɉ��񓖂�������
    public static int whiteCount;    //���ɉ��񓖂�������
    public static int blackCount;    //���ɉ��񓖂�������

    //�v���C���[
    [SerializeField]
    Animator playerAnimator;     //�v���C���[�̃A�j���[�^�[
    [SerializeField]
    Rigidbody playerRididbody;   //�v���C���[�̃��W�b�h�{�f�B

    //�p�[�e�B�N��
    [SerializeField]
    GameObject deathEffect;                  //���S���̃p�[�e�B�N��
    [SerializeField]
    ParticleSystem slowParticle;             //�X���E���ʂ̃p�[�e�B�N��
    [SerializeField]
    ParticleSystem electricshockParticle;    //���d���ʂ̃p�[�e�B�N��
    [SerializeField]
    ParticleSystem buffParticle;             //�ړ����x�A�b�v�̃p�[�e�B�N��

    //�S��UI
    [SerializeField]
    GameObject heart;

    //���C�g
    [SerializeField]
    GameObject spotLight;
    [SerializeField]
    GameObject directionalLight;

    //BGM
    [SerializeField]
    AudioSource InGameAudio;     //�v���C����BGM

    void Start()
    {
        //�v���C���[�͐����Ă��āA�̗͂�100
        isDie = false;
        playerHP = 100;

        //�S�[�X�g�ɓ����������̏�����
        redCount = 0;
        blueCount = 0;
        yellowCount = 0;
        whiteCount = 0;
        blackCount = 0;
    }

    void Update()
    {
        //WASD�������͖��L�[�̓��͏����擾
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        moveDirection = new Vector3(moveX,0,moveZ);
        //���K��
        moveDirection.Normalize();
        //���̌��ʎ��ԈȊO�͏�Ɉړ��\
        if (isYellow == false)
        {
            transform.position += moveDirection * walkSpeed * Time.deltaTime;
        }
        //�v���C���[���ړ�
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
        //�v���C���[���ړ����Ă���Ȃ�
        if (moveZ != 0 || moveX != 0)
        {
            //�����A�j���[�V������L����
            playerAnimator.SetBool("isMoving", true);
        }
        //�v���C���[���ړ����Ă��Ȃ��Ȃ�
        else
        {
            //�����A�j���[�V�����𖳌���
            playerAnimator.SetBool("isMoving", false);
        }

        //HP��0�ɂȂ��
        if (playerHP <= 0)
        {
            //����ł��܂��� --> �͂�
            isDie = true;
        }
        //�v���C���[�����ʂ�
        if (isDie == true)
        {
            //�Q�[���I�[�o�[�ɂȂ�
            GameManager.gameStatus = GameManager.GameStatus.GameOver;
            //���S�G�t�F�N�g�𐶐�
            Instantiate(deathEffect,this.transform.position,Quaternion.Euler(-90,0,0));
            //�v���C���[�̃Q�[���I�u�W�F�N�g��j��
            Destroy(this.gameObject);
            //�Q�[���I�[�o�[�ɂȂ��BGM�̒�~
            InGameAudio.volume = 0;
        }

        //�o�t�f�o�t����
        //��
        if (isBlue == true)
        {
            HitBlueGhost();
        }
        //��
        if (isYellow == true)
        {
            HitYellowGhost();
        }
        //��
        if (isWhite == true)
        {
            HitWhiteGhost();
        }
        //��
        if (isBlack == true)
        {
            HitBlackGhost();
        }
    }

    //�v���C���[�������ɂԂ�����
    private void OnTriggerEnter(Collider collider)
    {
        //�v���C���[�����e�ɓ������
        if(collider.gameObject.tag == "Bomb")
        {
            //����ł��܂��� --> �͂�
            isDie = true;
        }
        //�v���C���[���ԃS�[�X�g�ɓ������
        if (collider.gameObject.tag == "Enemy")
        {
            //����ł��܂��� --> �͂�
            isDie = true;
            //�Ԃɓ��������񐔁@+1
            redCount++;
        }
        //���u�ɓ���������
        if (collider.gameObject.tag == "Blue")
        {
            //�v���C���[�̃X�e�[�^�X�����Z�b�g
            ResetPlayerState();
            //�ɓ�����܂�����
            isBlue = true;

            //�̗̓}�C�i�X
            HitMobDamage();

            //�p�[�e�B�N���𐶐�and�Đ�and�j��
            BuffDebuffParticle(slowParticle, slowTime);

            //�S�[�X�g�ɓ��������񐔃J�E���g
            blueCount++;
        }
        //�����u�ɓ���������
        if (collider.gameObject.tag == "Yelow")
        {
            //�v���C���[�̃X�e�[�^�X�����Z�b�g
            ResetPlayerState();
            //���ɓ�����܂�����
            isYellow = true;

            //�̗̓}�C�i�X
            HitMobDamage();

            //�p�[�e�B�N���𐶐�and�Đ�and�j��
            BuffDebuffParticle(electricshockParticle, electricshockTime);

            //���S�[�X�g�ɓ��������񐔃J�E���g
            yellowCount++;
        }
        //�����u�ɓ���������
        if (collider.gameObject.tag == "White")
        {
            //�v���C���[�̃X�e�[�^�X�����Z�b�g
            ResetPlayerState();
            //���ɓ�����܂�����
            isWhite = true;

            //�p�[�e�B�N���𐶐�and�Đ�and�j��
            BuffDebuffParticle(buffParticle,buffTime);

            //���S�[�X�g�ɓ��������񐔃J�E���g
            whiteCount++;
        }
        //�����u�ɓ���������
        if(collider.gameObject.tag == "Black")
        {
            //���ɓ�����܂�����
            isBlack = true;

            //���ɓ��������񐔃J�E���g
            blackCount++;
        }
    }

    //�v���C���[�̃X�e�[�^�X�����Z�b�g
    private void ResetPlayerState()
    {
        //���x�����̑��x�ɖ߂�
        walkSpeed = defaultSpeed;
        //�Փ˔�������Z�b�g
        isBlue = false;
        isYellow = false;
        isWhite = false;
    }

    //���u�ɏՓ˂������Ƀ_���[�W���󂯂�
    private void HitMobDamage()
    {
        //�̗�-�_���[�W
        playerHP -= hitDamage;
    }

    //�o�t�f�o�t���ʂ̃p�[�e�B�N��
    private void BuffDebuffParticle(ParticleSystem particle,float effectTime)
    {
        //�p�[�e�B�N���𐶐�
        ParticleSystem newParticle = Instantiate(particle);
        newParticle.transform.position = this.gameObject.transform.position;
        newParticle.transform.parent = this.gameObject.transform;
        //�p�[�e�B�N�����Đ�
        newParticle.Play();
        Destroy(newParticle.gameObject, effectTime);
    }

    //�S�[�X�g�ɓ����������̃o�t�f�o�t����
    private void HitBlueGhost()
    {
        //�^�C���J�E���g
        blueTimeCount += Time.deltaTime;

        //�X���E���ʎ��ԓ���
        if (blueTimeCount <= slowTime)
        {
            //�X���E���ʂɂ�茸��
            walkSpeed = slowSpeed;

            //�S���̌ۓ����x���Ȃ�A�j���[�V����
            heart.GetComponent<HeartUIScript>().SlowHeartBeat();
        }
        //�X���E���ʎ��Ԃ��I�������
        else if (blueTimeCount > slowTime)
        {
            //���x�����̑��x�ɖ߂�
            walkSpeed = defaultSpeed;

            //�Փ˔��胊�Z�b�g
            isBlue = false;

            //�^�C���J�E���g�����Z�b�g
            blueTimeCount = 0;

            //�ʏ�̌ۓ��ɖ߂�
            heart.GetComponent<HeartUIScript>().NormalHeartBeat();
        }
    }

    //���S�[�X�g�ɓ����������̃o�t�f�o�t����

    private void HitYellowGhost()
    {
        //�^�C���J�E���g
        yellowTimeCount += Time.deltaTime;

        //��~���ʎ��ԓ���
        if (yellowTimeCount <= electricshockTime)
        {
            //���F�ɂԂ����Ă���
            isYellow = true;

            //�S���̌ۓ����~�܂�A�j���[�V����
            heart.GetComponent<HeartUIScript>().StopHeartBeat();
        }
        //��~���ʎ��Ԃ��I�������
        else if (yellowTimeCount > electricshockTime)
        {
            //���x�����̑��x�ɖ߂�
            walkSpeed = defaultSpeed;

            //�Փ˔��胊�Z�b�g
            isYellow = false;

            //�^�C���J�E���g�����Z�b�g
            yellowTimeCount = 0;

            //�ʏ�̌ۓ��ɖ߂�
            heart.GetComponent<HeartUIScript>().NormalHeartBeat();
        }
    }

    //���S�[�X�g�ɓ����������̃o�t�f�o�t����
    private void HitWhiteGhost()
    {
        //�^�C���J�E���g
        whiteTimeCount += Time.deltaTime;

        //�X�s�[�h�A�b�v���ʎ��ԓ���
        if (whiteTimeCount <= buffTime)
        {
            //�X�s�[�h�A�b�v���ʂɂ�����
            walkSpeed = buffSpeed;

            //�S���̌ۓ��������Ȃ�A�j���[�V����
            heart.GetComponent<HeartUIScript>().HighHeartBeat();
        }
        //�X�s�[�h�A�b�v���ʂ��I�������
        else if (whiteTimeCount > buffTime)
        {
            //���x�����̑��x�ɖ߂�
            walkSpeed = defaultSpeed;

            //�Փ˔��胊�Z�b�g
            isWhite = false;

            //�^�C���J�E���g�����Z�b�g
            whiteTimeCount = 0;

            //�ʏ�̌ۓ��ɖ߂�
            heart.GetComponent<HeartUIScript>().NormalHeartBeat();
        }
    }

    //���S�[�X�g�ɓ����������̃o�t�f�o�t����
    private void HitBlackGhost()
    {
        //�^�C���J�E���g
        blackTimeCount += Time.deltaTime;

        //�u���C���h���ʎ��ԓ��Ȃ�
        if (blackTimeCount <= blindTime)
        {
            //���C�g��������
            spotLight.SetActive(false);
            directionalLight.SetActive(false);
        }
        //�u���C���h���ʎ��Ԃ��I�������
        else if (blackTimeCount > blindTime)
        {
            //���C�g���_��
            spotLight.SetActive(true);
            directionalLight.SetActive(true);

            //�Փ˔��胊�Z�b�g
            isBlack = false;

            //�^�C���J�E���g�����Z�b�g
            blackTimeCount = 0;
        }
    }
}
