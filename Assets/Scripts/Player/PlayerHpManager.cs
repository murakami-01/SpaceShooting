using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpManager : HpManager
{
    [SerializeField] private GameObject sceneManager;
    [SerializeField] private GameObject effect;
    private ResultJudgment resultJudgment;
    public bool isActive { get; set; } = true;

    private void Start()
    {
        resultJudgment = sceneManager.GetComponent<ResultJudgment>();
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (isActive)
        {
            if (other.tag == "EnemyBullet")
            {
                var bulletCs = other.GetComponent<BulletManager>();
                Damage(bulletCs.Attack);
            }
            else if (other.tag == "Enemy")
            {
                Damage(maxHp / 2);
            }
        }
    }

    public void Recovery()
    {
        if (maxHp / hp < 2) Damage(hp - maxHp);
        else Damage(-maxHp / 2);
    }

    public override void Die()
    {
        Instantiate(effect, this.transform.position, Quaternion.identity);
        resultJudgment.PlayerLose();
        Destroy(this.gameObject);
    }
}
