using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// mobのhpを制御するクラス
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
                //プレイヤーの攻撃によるダメージ
                var bulletCs = other.GetComponent<BulletManager>();
                Damage(bulletCs.Attack);
            }
            else if (other.CompareTag("Player"))
            {
                //プレイヤーと衝突
                Damage(maxHp);
            }
        }

        if(other.CompareTag("BossBomb"))
        {
            //boss死亡時に当たり判定を拾って死ぬ
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
                //レーザーによる継続ダメージ
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
                //hp2割以下でゲージを赤色に変化させる
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
     * 死亡時に行う遅延処理（手に入れられた効果音に合わせるため）
     * </summary>
     * */
    private IEnumerator DeadEffect()
    {
        //当たり判定と攻撃を停止
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        mobAttack.StopAllCoroutines();
        Camera.main.GetComponent<AudioSource>().PlayOneShot(clip);

        yield return new WaitForSeconds(0.5f);

        //アイテムがドロップするかの判定
        itemCreator = sceneManager.GetComponent<ItemCreator>();
        itemCreator.RandomCreate(this.transform.position);

        //倒した数のカウント
        sceneManager.GetComponent<ResultJudgment>().num++;
        Instantiate(effect, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
