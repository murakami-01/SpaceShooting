using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// bossのhp管理用クラス
/// </summary>
public class BossHpManager : HpManager
{
    private bool isFirstGauge = true;
    public bool isActive { get; set; }
    [SerializeField] private Transform secondHpGaugeTransform;
    [SerializeField] private BossAttack bossAttack;
    [SerializeField] private GameObject effect1;
    [SerializeField] private GameObject effect2;
    [SerializeField] private GameObject bossBomb;
    [SerializeField] private AudioClip clip2;

    private GameObject sceneManager;
    private ResultJudgment resultJudgment;

    public override void Awake()
    {
        hp = maxHp;
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager");
        resultJudgment = sceneManager.GetComponent<ResultJudgment>();
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //プレイヤーと衝突
            Damage(maxHp);
        }

    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerLaser"))
        {
            //レーザーの継続ダメージ
            var bulletCs = other.GetComponent<BulletManager>();
            Damage(bulletCs.Attack * 0.1f);
        }

    }

    public override void Damage(float damage)
    {
        if (isActive)
        {
            if (hp <= damage)
            {
                hpParentTransform.localScale = new Vector3(0, hpParentTransform.localScale.y, hpParentTransform.localScale.z);
                if (isFirstGauge)
                {
                    //1ゲージ目だったならば２ゲージ目への移行処理
                    isFirstGauge = false;
                    hp = maxHp - 120;
                    hpParentTransform = secondHpGaugeTransform;
                    bossAttack.ChangeAttack();
                    for (int i = 0; i < 2; i++)
                    {
                        Instantiate(effect1, this.transform.position + new Vector3(-0.2f + 0.4f * i, -0.7f, 0), Quaternion.identity);
                        Camera.main.GetComponent<AudioSource>().PlayOneShot(clip);
                    }
                }
                else
                {
                    //2ゲージ目だったならば志望処理
                    Die();
                }
            }
            else
            {
                hp -= damage;
                hpParentTransform.localScale = new Vector3(hp / maxHp, hpParentTransform.localScale.y, hpParentTransform.localScale.z);

            }
        }
        
    }

    public override void Die()
    {
        isActive = false;
        Destroy(bossAttack);
        bossBomb.SetActive(true);//mobを破壊するための当たり判定を有効化
        resultJudgment.num++;
        StartCoroutine(BombEffect());
        StartCoroutine(resultJudgment.PlayerWin());
    }

    /**
     * <summary>
     * boss死亡時のエフェクト
     * </summary>
     * */
    private IEnumerator BombEffect()
    {
        Instantiate(effect2, this.transform.position, Quaternion.identity);
        Camera.main.GetComponent<AudioSource>().PlayOneShot(clip2);
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
