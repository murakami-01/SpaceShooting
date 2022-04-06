using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの通常弾を制御するクラス
/// </summary>
public class PlayerBullet : BulletManager
{
    Rigidbody rb;
    public override int bulletid { get; set; } = 0;
    private float limitSpeed;

    public override void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        limitSpeed = bulletDataList.bulletDataList[bulletid].Speed;
        rb.velocity = new Vector3(0, 1, 0) * limitSpeed;
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.gameObject.TryGetComponent<HpManager>(out HpManager hpManager))
            {
                hpManager.Damage(this.Attack);
            }
            Destroy(this.gameObject);
        }else if (other.CompareTag("EnemyBullet"))
        {
            Damage();
        }
    }
}
