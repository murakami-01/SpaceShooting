using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// mob��hp�𐧌䂷��N���X
/// </summary>
public class MobHpManager : HpManager
{
    [System.NonSerialized] public GameObject sceneManager;
    private ItemCreator itemCreator;
    public float screenRatio { get; set; }
    public GameObject effect;
    [SerializeField] private EnemyAttack mobAttack;

    public override void Awake()
    {
        hp = maxHp;
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager");
        screenRatio = Camera.main.orthographicSize / 5;
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (this.transform.position.y <= 5.5f * screenRatio)
        {
            if (other.CompareTag("PlayerBullet")|| other.CompareTag("PlayerLaser"))
            {
                //�v���C���[�̍U���ɂ��_���[�W
                var bulletCs = other.GetComponent<BulletManager>();
                Damage(bulletCs.Attack);
            }
            else if (other.CompareTag("Player"))
            {
                //�v���C���[�ƏՓ�
                Damage(maxHp);
            }
        }

        if(other.CompareTag("BossBomb"))
        {
            //boss���S���ɓ����蔻����E���Ď���
            Instantiate(effect, this.transform.position, Quaternion.identity);
            sceneManager.GetComponent<ResultJudgment>().num++;
            Destroy(this.gameObject);
        }
        
    }

    public void OnTriggerStay(Collider other)
    {
        if (this.transform.position.y <= 5f * screenRatio)
        {
            if (other.CompareTag("PlayerLaser"))
            {
                //���[�U�[�ɂ��p���_���[�W
                var bulletCs = other.GetComponent<BulletManager>();
                Damage(bulletCs.Attack);
            }
        }
        
    }

    public override void Damage(float damage)
    {
        if (hp <= damage)
        {
            hpParentTransform.localScale = new Vector3(0, hpParentTransform.localScale.y, hpParentTransform.localScale.z);
            Die();
        }
        else
        {
            hp -= damage;
            hpParentTransform.localScale = new Vector3(hp / maxHp, hpParentTransform.localScale.y, hpParentTransform.localScale.z);

            if (hp / maxHp <= 0.2f && !isRed)
            {
                //hp2���ȉ��ŃQ�[�W��ԐF�ɕω�������
                var hpgauge = hpParentTransform.GetChild(0);
                hpgauge.GetComponent<SpriteRenderer>().color = Color.red;
                isRed = true;
            }

        }
    }

    public override void Die()
    {
        StartCoroutine(DeadEffect());
    }

    /**
     * <summary>
     * ���S���ɍs���x�������i��ɓ����ꂽ���ʉ��ɍ��킹�邽�߁j
     * </summary>
     * */
    private IEnumerator DeadEffect()
    {
        //�����蔻��ƍU�����~
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        mobAttack.StopAllCoroutines();
        Camera.main.GetComponent<AudioSource>().PlayOneShot(clip);

        yield return new WaitForSeconds(0.5f);

        //�A�C�e�����h���b�v���邩�̔���
        itemCreator = sceneManager.GetComponent<ItemCreator>();
        itemCreator.RandomCreate(this.transform.position);

        //�|�������̃J�E���g
        sceneManager.GetComponent<ResultJudgment>().num++;
        Instantiate(effect, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
