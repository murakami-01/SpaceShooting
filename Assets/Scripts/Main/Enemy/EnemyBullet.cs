using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵の通常弾を制御するクラス
/// </summary>
public class EnemyBullet : BulletManager
{
    Rigidbody rb;
    public override int bulletid { get; set; } = 4;

    public override void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, -1, 0) * bulletDataList.bulletDataList[bulletid].Speed;
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }else if(other.CompareTag("PlayerBullet"))
        {
            //弾丸同士ならダメージ処理
            Damage();
        }
    }
}
