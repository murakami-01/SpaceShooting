using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHpManager : HpManager
{
    private bool isFirstGauge = true;
    public bool isActive { get; set; }
    [SerializeField] private Transform secondHpGaugeTransform;
    [SerializeField] private BossAttack bossAttack;
    [SerializeField] private GameObject effect1;
    [SerializeField] private GameObject effect2;
    [SerializeField] private GameObject bossBomb;
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
        if (isActive)
        {
            if (other.tag == "PlayerBullet" || other.tag == "PlayerLaser")
            {
                var bulletCs = other.GetComponent<BulletManager>();
                Damage(bulletCs.Attack);
            }
            else if (other.tag == "Player")
            {
                Damage(maxHp);
            }
        }

    }

    public void OnTriggerStay(Collider other)
    {
        if (isActive)
        {
            if (other.tag == "PlayerLaser")
            {
                var bulletCs = other.GetComponent<BulletManager>();
                Damage(bulletCs.Attack * 0.1f);
            }
        }

    }

    public override void Damage(float damage)
    {
        if (hp <= damage)
        {
            hpParentTransform.localScale = new Vector3(0, hpParentTransform.localScale.y, hpParentTransform.localScale.z);
            if (isFirstGauge)
            {
                isFirstGauge = false;
                hp = maxHp;
                hpParentTransform = secondHpGaugeTransform;
                bossAttack.ChangeAttack();
                for (int i = 0; i < 2; i++)
                {
                    Instantiate(effect1, this.transform.position + new Vector3(-0.2f + 0.4f * i, -0.7f, 0), Quaternion.identity);
                }
            }
            else
            {
                Die();
            }
        }
        else
        {
            hp -= damage;
            hpParentTransform.localScale = new Vector3(hp / maxHp, hpParentTransform.localScale.y, hpParentTransform.localScale.z);

        }
    }

    public override void Die()
    {
        isActive = false;
        Destroy(bossAttack);
        bossBomb.SetActive(true);
        StartCoroutine(BombEffect());
        StartCoroutine(resultJudgment.PlayerWin());
    }

    private IEnumerator BombEffect()
    {
        Instantiate(effect2, this.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
