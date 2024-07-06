using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{
    public PlayerMoveScript playerMoveScript;

    public static int playerHP;�@//�v���C���[�̗̑�
    float p_walkSpeed;
    float p_defaultSpeed;
    float p_buffSpeed;
    float p_slowSpeed;
    float p_rotateSpeed;

    public static bool isDie;    //����ł��邩

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
    public bool IsBlue { get; set; } = false;    //��
    public bool IsYellow { get; set; } = false;   //���F
    public bool IsBlack { get; set; } = false;   //��
    public bool IsWhite { get; set; } = false;   //��

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
        //�v���C���[�̃X�s�[�h���Q��
        p_walkSpeed = playerMoveScript.WalkSpeed;
        p_defaultSpeed = playerMoveScript.DefaultSpeed;

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
        if (IsBlue == true)
        {
            HitBlueGhost();
        }
        //��
        if (IsYellow == true)
        {
            HitYellowGhost();
        }
        //��
        if (IsWhite == true)
        {
            HitWhiteGhost();
        }
        //��
        if (IsBlack == true)
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
            IsBlue = true;

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
            IsYellow = true;

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
            IsWhite = true;

            //�p�[�e�B�N���𐶐�and�Đ�and�j��
            BuffDebuffParticle(buffParticle,buffTime);

            //���S�[�X�g�ɓ��������񐔃J�E���g
            whiteCount++;
        }
        //�����u�ɓ���������
        if(collider.gameObject.tag == "Black")
        {
            //���ɓ�����܂�����
            IsBlack = true;

            //���ɓ��������񐔃J�E���g
            blackCount++;
        }
    }

    //�v���C���[�̃X�e�[�^�X�����Z�b�g
    private void ResetPlayerState()
    {
        //���x�����̑��x�ɖ߂�
        p_walkSpeed = p_defaultSpeed;
        //�Փ˔�������Z�b�g
        IsBlue = false;
        IsYellow = false;
        IsWhite = false;
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
            p_walkSpeed = p_slowSpeed;

            //�S���̌ۓ����x���Ȃ�A�j���[�V����
            heart.GetComponent<HeartUIScript>().SlowHeartBeat();
        }
        //�X���E���ʎ��Ԃ��I�������
        else if (blueTimeCount > slowTime)
        {
            //���x�����̑��x�ɖ߂�
            p_walkSpeed = p_defaultSpeed;

            //�Փ˔��胊�Z�b�g
            IsBlue = false;

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
            IsYellow = true;

            //�S���̌ۓ����~�܂�A�j���[�V����
            heart.GetComponent<HeartUIScript>().StopHeartBeat();
        }
        //��~���ʎ��Ԃ��I�������
        else if (yellowTimeCount > electricshockTime)
        {
            //���x�����̑��x�ɖ߂�
            p_walkSpeed = p_defaultSpeed;

            //�Փ˔��胊�Z�b�g
            IsYellow = false;

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
            p_walkSpeed = p_buffSpeed;

            //�S���̌ۓ��������Ȃ�A�j���[�V����
            heart.GetComponent<HeartUIScript>().HighHeartBeat();
        }
        //�X�s�[�h�A�b�v���ʂ��I�������
        else if (whiteTimeCount > buffTime)
        {
            //���x�����̑��x�ɖ߂�
            p_walkSpeed = p_defaultSpeed;

            //�Փ˔��胊�Z�b�g
            IsWhite = false;

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
            IsBlack = false;

            //�^�C���J�E���g�����Z�b�g
            blackTimeCount = 0;
        }
    }
}
